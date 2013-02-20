using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTheBoks.Models
{
    public class Catalog : ModelBase
    {
        public long Count { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public long User_Id { get; set; }

        public Privacy Visibility { get; set; }
    }
}