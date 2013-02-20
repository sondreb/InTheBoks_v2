namespace InTheBoks.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Friend : ModelBase
    {
        public string FacebookFriendIds { get; set; }

        public string FriendIds { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public long User_Id { get; set; }
    }
}