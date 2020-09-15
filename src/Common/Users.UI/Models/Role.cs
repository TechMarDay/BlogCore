using Common.Attributes;
using Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.UI.Models
{
    [Table("icc_role")]
    public class Role : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("is_external_role")]
        public bool IsExternalRole { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}