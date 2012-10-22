namespace InTheBoks.Data.Infrastructure
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueAttribute : ValidationAttribute
    {
        public override Boolean IsValid(Object value)
        {
            return true;
        }
    }
}