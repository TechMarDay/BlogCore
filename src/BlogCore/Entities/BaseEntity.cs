using System;
namespace BlogCore.Entities
{
    public class BaseEntity
    {
        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
