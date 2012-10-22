using System;
using System.ComponentModel.DataAnnotations;

namespace InTheBoks.Models
{
    public class Log
    {
        public string Exception { get; set; }

        [Key]
        public long Id { get; set; }

        //public DateTime Created { get; set; }

        [MaxLength(10)]
        public string Level { get; set; }

        [MaxLength(128)]
        public string Logger { get; set; }

        [MaxLength(128)]
        public string MachineName { get; set; }

        public string Message { get; set; }

        public string Stacktrace { get; set; }

        [MaxLength(128)]
        public string ThreadId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}