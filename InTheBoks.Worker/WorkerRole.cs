namespace InTheBoks.Worker
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Diagnostics;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Microsoft.WindowsAzure.StorageClient;
    using NLog;
    using Autofac;
    using InTheBoks.Data.Repositories;
    using System.Threading.Tasks;
    using InTheBoks.Data.Infrastructure;
    using Facebook;

    public class WorkerRole : RoleEntryPoint
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public WorkerRole()
        {
            Bootstrapper.Run();
        }

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("InTheBoks.Worker entry point called", "Information");
            _log.Info("InTheBoks.Worker entry point called");

            while (true)
            {
                var userRepository = IoC.Current.Resolve<IUserRepository>();

                foreach (var user in userRepository.GetAll())
                {
                    if ((user.FriendsLastChecked - DateTime.UtcNow) < TimeSpan.FromDays(1))
                    {
                        var friendRepository = IoC.Current.Resolve<IFriendRepository>();
                        var unitOfWork = IoC.Current.Resolve<IUnitOfWork>();
                        var friends = friendRepository.Query().SingleOrDefault(f => f.User_Id == user.Id);

                        var fb = new FacebookClient(user.Token);
                        dynamic json = null;

                        try
                        {
                            json = fb.Get("me/friends");
                        }
                        catch (FacebookOAuthException fbEx)
                        {
                            if (fbEx.ErrorCode == 190)
                            {
                                //user.Token = null;
                                //user.TokenExpire = new DateTime(1970, 1, 1);
                                //userRepository.Update(user);
                                //unitOfWork.Commit();
                            }

                            _log.ErrorException(fbEx.Message, fbEx);
                        }

                        if (json == null)
                        {
                            continue;
                        }

                        List<long> ids = new List<long>();

                        foreach (var friend in json.data)
                        {
                            var id = long.Parse(friend.id);
                            var name = friend.name;

                            ids.Add(id);
                        }

                        var friendIds = string.Join(";", ids);


                        var existingFriends = userRepository.Query().Where(u => ids.Contains(u.FacebookId));

                        List<long> existingFriendsIds = new List<long>();
                        foreach (var friend in existingFriends)
                        {
                            existingFriendsIds.Add(friend.Id);
                        }

                        if (friends == null)
                        {
                            friends = new Models.Friend();
                            friends.User_Id = user.Id;
                            friends.FacebookFriendIds = friendIds;
                            friends.FriendIds = string.Join(";", existingFriendsIds);
                            friendRepository.Add(friends);
                        }
                        else
                        {
                            friends.FacebookFriendIds = friendIds;
                            friends.FriendIds = string.Join(";", existingFriendsIds);
                            friendRepository.Update(friends);
                        }

                        unitOfWork.Commit();
                    }
                }


                Thread.Sleep(10000);
                Trace.WriteLine("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
