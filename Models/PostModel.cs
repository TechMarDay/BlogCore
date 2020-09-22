using System;

namespace BlogCore.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public string Image { get; set; }

        public string Summary { get; set; }

        public CategoryModel Category { get; set; }

        public DateTime LastModificationTime { get; set; }
    }
}
