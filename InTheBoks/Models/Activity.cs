namespace InTheBoks.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Activity
    {
        public DateTime Created { get; set; }

        [Key]
        public long Id { get; set; }

        public Item Item { get; set; }

        [ForeignKey("Item")]
        public long Item_Id { get; set; }

        [MaxLength(400)]
        public string StatusText { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public long User_Id { get; set; }
    }
}