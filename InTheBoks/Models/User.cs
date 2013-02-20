namespace InTheBoks.Models
{
    using InTheBoks.Data.Infrastructure;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class User : ModelBase
    {
        [MaxLength(250)]
        public string Email { get; set; }

        [Unique]
        public long FacebookId { get; set; }

        public DateTime? FriendsLastChecked { get; set; }

        [MaxLength(10)]
        public string Language { get; set; }

        [MaxLength(250)]
        public string Link { get; set; }

        public bool ShareActivity { get; set; }

        public bool ShareFacebook { get; set; }

        [MaxLength(100)]
        [DefaultValue("CET")]
        public string TimeZone { get; set; }

        [MaxLength(500)]
        public string Token { get; set; }

        public DateTime? TokenExpire { get; set; }
    }
}