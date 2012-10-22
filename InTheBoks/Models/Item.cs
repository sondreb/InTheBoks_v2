using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTheBoks.Models
{
    public class Item
    {
        [MaxLength(200)]
        public string ASIN { get; set; }

        public Catalog Catalog { get; set; }

        [ForeignKey("Catalog")]
        public long Catalog_Id { get; set; }

        public DateTime Created { get; set; }

        [Key]
        public long Id { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? Modified { get; set; }

        [MaxLength(1000)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Url { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public long User_Id { get; set; }
    }
}