using Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.UI.Models
{
    [Table("icc_user_account_role")]
    public class UserAccountRole
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("user_id")]
        public int UserId { set; get; }

        [Column("role_id")]
        public int RoleId { set; get; }
    }
}