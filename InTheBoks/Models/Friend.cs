namespace InTheBoks.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Friend
    {
        public string FacebookFriendIds { get; set; }

        public string FriendIds { get; set; }

        [Key]
        public long Id { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public long User_Id { get; set; }
    }
}