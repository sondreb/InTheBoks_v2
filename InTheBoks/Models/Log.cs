using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace InTheBoks.Models
{
    public class Log
    {
        [Key]
        public long Id { get; set; }

        //public DateTime Created { get; set; }

        public DateTime Timestamp { get; set; }

        public string Message { get; set; }

        [MaxLength(10)]
        public string Level { get; set; }

        [MaxLength(128)]
        public string Logger { get; set; }

        [MaxLength(128)]
        public string MachineName { get; set; }

        [MaxLength(128)]
        public string ThreadId { get; set; }

        public string Exception { get; set; }

        public string Stacktrace { get; set; }
    }
}
