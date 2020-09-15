using Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.UI.Models
{
    [Table("icc_role_permission")]
    public class RolePermission
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("feature_id")]
        public int FeatureId { set; get; }

        [Column("role_id")]
        public int RoleId { set; get; }

        [Column("permission_id")]
        public int PermissionId { set; get; }

        [Column("is_enabled")]
        public bool IsEnabled { set; get; }
    }
}