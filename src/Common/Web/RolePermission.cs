using Common.Attributes;
using IdentityModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web
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