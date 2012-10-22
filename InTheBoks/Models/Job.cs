namespace InTheBoks.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Job
    {
        public DateTime Completed { get; set; }

        public string Description { get; set; }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string JobType { get; set; }

        public long Progress { get; set; }

        public DateTime Started { get; set; }

        [Required]
        public short StatusCode { get; set; }

        [StringLength(200)]
        public string StatusText { get; set; }

        public long Total { get; set; }
    }
}