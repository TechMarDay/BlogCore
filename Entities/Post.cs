
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCore.Entities
{
    public class Post: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Url { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Summary { get; set; }

        public string Image { get; set; }

        public virtual Category Category { get; set; }
    }
}
