using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace InTheBoks.Models
{
    public class Item
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(200)]
        public string ASIN { get; set; }

        [MaxLength(1000)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public long User_Id { get; set; }

        public Catalog Catalog { get; set; }

        [ForeignKey("Catalog")]
        public long Catalog_Id { get; set; }
    }
}
