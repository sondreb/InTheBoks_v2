namespace InTheBoks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;

    public class Friend
    {
        [Key]
        public long Id { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public long User_Id { get; set; }

        public string FriendIds { get; set; }

        public string FacebookFriendIds { get; set; }
    }
}