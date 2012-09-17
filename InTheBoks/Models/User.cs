using InTheBoks.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace InTheBoks.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Unique]
        public long FacebookId { get; set; }

        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Link { get; set; }

        [MaxLength(500)]
        public string Token { get; set; }

        [MaxLength(10)]
        public string Language { get; set; }

        public DateTime? FriendsLastChecked { get; set; }

        public DateTime? TokenExpire { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public bool ShareActivity { get; set; }

        public bool ShareFacebook { get; set; }

        [MaxLength(100)]
        [DefaultValue("CET")]
        public string TimeZone { get; set; }
    }
}
