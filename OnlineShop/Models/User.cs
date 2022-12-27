using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="کد کاربر")]
        public Guid Id { get; set; }

        [Display(Name ="کد نقش کابر")]
        public Guid RoleId { get; set; }

        [Display(Name ="شماره تماس")]
        [Required]
        [MaxLength(11)]
        [MinLength(11)]
        public string Mobile { get; set; }//userName

        [Display(Name ="رمز عبور")]
        [Required]
        public string Password { get; set; }

        [Display(Name ="کد پیامکی")]
        public int Code { get; set; }


        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }  
    }
}
