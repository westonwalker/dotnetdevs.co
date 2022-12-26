using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetdevs.Models
{
    public class Post : EntityBase
    {
        public int ID { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Markdown { get; set; }
    }
}
