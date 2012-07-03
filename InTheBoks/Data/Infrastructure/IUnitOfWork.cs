namespace InTheBoks.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IUnitOfWork
    {
        void Commit();
    }
}
