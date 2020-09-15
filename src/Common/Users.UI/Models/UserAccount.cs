using Common.Attributes;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.UI.Models
{
    [Table("icc_user_account")]
    public class UserAccount : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [JsonIgnore]
        [Column("password")]
        public string Password { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("display_name")]
        public string DisplayName { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("security_password")]
        public Guid SecurityPassword { get; set; }

        [Column("password_resetcode")]
        public string PasswordResetCode { get; set; }

        [Column("is_external_user")]
        public bool IsExternalUser { get; set; }

        [Column("is_superadmin")]
        public bool IsSuperAdmin { get; set; }

        [Column("is_actived")]
        public bool IsActived { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }
    }
}