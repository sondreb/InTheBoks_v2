namespace InTheBoks.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueAttribute : ValidationAttribute
    {
        public override Boolean IsValid(Object value)
        {
            return true;
        }
    }
}
