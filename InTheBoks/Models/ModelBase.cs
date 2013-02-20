namespace InTheBoks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public abstract class ModelBase
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
