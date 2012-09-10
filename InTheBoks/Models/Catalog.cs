using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace InTheBoks.Models
{
    public class Catalog
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        public long Count { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public long User_Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public Privacy Visibility { get; set; }
    }
}
