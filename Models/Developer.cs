using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.Models
{
    public class Developer : EntityBase
    {
        public int ID { get; set; }

		[Required]
		public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name is to long (50 character limit)")]
        public string FullName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Hero is to long (100 character limit)")]
        public string Hero { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "City is to long (50 character limit)")]
        public string City { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "State is to long (50 character limit)")]
        public string State { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Country is to long (50 character limit)")]
        public string Country { get; set; }

        [Required(ErrorMessage = "An avatar image is required. Only jpg and png images are allowed.")]
        public string? Avatar { get; set; }

        [Required]
        public string Bio { get; set; }

		[Required]
		public int SearchStatusID { get; set; }

        public virtual SearchStatus SearchStatus { get; set; }

		[Required]
		public int ExperienceLevelID { get; set; }

        public virtual ExperienceLevel ExperienceLevel { get; set; }

        public DateTime StartDate { get; set; }

        public string? Website { get; set; }

        public string? Github { get; set; }

        public string? Twitter { get; set; }

        public string? Linkedin { get; set; }

        public string? StackOverflow { get; set; }
    }
}
