using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class BaseModel
    {
        [JsonIgnore]
        [Column("is_deleted")]
        public bool IsDeleted { set; get; }

        [Column("created_date")]
        public DateTime CreatedDate { set; get; }

        [JsonIgnore]
        [Column("created_by")]
        public int CreatedBy { set; get; }

        [Column("modified_date")]
        public DateTime? ModifiedDate { set; get; }

        [Column("modified_by")]
        public int? ModifiedBy { set; get; }
    }
}