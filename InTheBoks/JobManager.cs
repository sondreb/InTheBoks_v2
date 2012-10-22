using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InTheBoks
{
    public interface IJobManager
    {
        string Description { get; set; }

        string Operation { get; set; }

        void Progress(long current, long total);

        void Progress(long current);

        void Start();

        void Stop();
    }

    public class JobManager : IJobManager
    {
        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Operation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Progress(long current, long total)
        {
            throw new NotImplementedException();
        }

        public void Progress(long current)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}