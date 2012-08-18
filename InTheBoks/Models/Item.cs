using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
