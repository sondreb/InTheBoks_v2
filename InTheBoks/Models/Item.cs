namespace InTheBoks.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Item : ModelBase
    {
        [MaxLength(200)]
        public string ASIN { get; set; }

        public Catalog Catalog { get; set; }

        [ForeignKey("Catalog")]
        public long Catalog_Id { get; set; }

        public string ImageUrl { get; set; }

        [MaxLength(1000)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Url { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public long User_Id { get; set; }
    }
}