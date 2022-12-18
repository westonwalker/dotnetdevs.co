using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnetdevs.Models
{

    public class Company : EntityBase
    {
        public int ID { get; set; }

		[Required]
		public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100)]
        public string website { get; set; }

        public string Avatar { get; set; }

        [Required]
        [StringLength(50)]
        public string PersonalName { get; set; }

        [Required]
        [StringLength(50)]
        public string JobTitle { get; set; }

        [Required]
        public string Bio { get; set; }

        public bool IsSubscribed { get; set; }
		public string? PaymentGuid { get; set; }
	}
}
