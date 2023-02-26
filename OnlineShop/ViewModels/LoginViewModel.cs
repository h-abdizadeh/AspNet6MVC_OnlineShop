using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModels
{
    public class LoginViewModel
    {
        [Display(Prompt ="شماره موبایل")]
        [Required(ErrorMessage = "ثبت شماره موبایل الزامیست")]
        [MinLength(11, ErrorMessage = "شماره موبایل 11 رقمی")]
        [MaxLength(11, ErrorMessage = "شماره موبایل 11 رقمی")]
        public string Mobile { get; set; }

        [Display(Prompt = "رمز عبور")]
        [Required(ErrorMessage = "ثبت رمز عبور الزامیست")]
        [MinLength(4, ErrorMessage = "رمز عبور حداقل 8 کاراکتر")]
        [MaxLength(15, ErrorMessage = "رمز عبور حداکثر 15 کاراکتر")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
