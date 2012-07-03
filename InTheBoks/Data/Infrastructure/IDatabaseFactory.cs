namespace InTheBoks.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InTheBoks.Data;

    public interface IDatabaseFactory : IDisposable
    {
        DataContext Get();
    }
}
