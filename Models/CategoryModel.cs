using System;
using System.Collections.Generic;

namespace BlogCore.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime LastModificationTime { get; set; }

        public List<PostModel> Posts { get; set; }
    }
}
