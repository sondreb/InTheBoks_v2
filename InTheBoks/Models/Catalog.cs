using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTheBoks.Models
{
    public class Catalog
    {
        public long Count { get; set; }

        public DateTime Created { get; set; }

        [Key]
        public long Id { get; set; }

        public DateTime? Modified { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public long User_Id { get; set; }

        public Privacy Visibility { get; set; }
    }
}