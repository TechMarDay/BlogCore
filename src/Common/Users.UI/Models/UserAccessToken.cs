using Common.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.UI.Models
{
    [Table("icc_user_accesstoken")]
    public class UserAccessToken
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("user_id")]
        public int UserId { set; get; }

        [Column("accesstoken")]
        public string AccessToken { set; get; }

        [Column("login_date")]
        public DateTime LoginDate { set; get; }

        [Column("expired_date")]
        public DateTime ExpiredDate { set; get; }
    }
}