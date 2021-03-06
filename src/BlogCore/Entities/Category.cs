﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BlogCore.Models.CategoryTypeEnum;

namespace BlogCore.Entities
{
    public class Category: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public Type CategoryType { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
