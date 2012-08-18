namespace InTheBoks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    public class Job
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string JobType { get; set; }

        [Required]
        public short StatusCode { get; set; }

        [StringLength(200)]
        public string StatusText { get; set; }

        public string Description { get; set; }

        public DateTime Started { get; set; }

        public DateTime Completed { get; set; }

        public long Progress { get; set; }

        public long Total { get; set; }
    }
}
