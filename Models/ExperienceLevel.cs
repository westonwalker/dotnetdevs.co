using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.Models
{
    public class ExperienceLevel : EntityBase
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }
}
