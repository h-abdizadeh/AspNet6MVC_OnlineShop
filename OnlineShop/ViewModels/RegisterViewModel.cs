﻿using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModels;

public class RegisterViewModel
{
    [Display(Prompt ="شماره موبایل")]
    [Required(ErrorMessage ="ثبت شماره موبایل الزامیست")]
    [MinLength(11,ErrorMessage ="شماره موبایل 11 رقمی")]
    [MaxLength(11, ErrorMessage = "شماره موبایل 11 رقمی")]
    public string Mobile { get; set; }

    [Display(Prompt = "رمز عبور")]
    [Required(ErrorMessage = "ثبت رمز عبور الزامیست")]
    [MinLength(8,ErrorMessage ="رمز عبور حداقل 8 کاراکتر")]
    [MaxLength(15,ErrorMessage ="رمز عبور حداکثر 15 کاراکتر")]
    public string Password { get; set; }

    [Display(Prompt = "تکرار رمز عبور")]
    [Required(ErrorMessage = "تکرار رمز عبور الزامیست")]
    [MinLength(8, ErrorMessage = "رمز عبور حداقل 8 کاراکتر")]
    [MaxLength(15, ErrorMessage = "رمز عبور حداکثر 15 کاراکتر")]
    [Compare("Password",ErrorMessage ="رمز عبور همخوانی ندارد")]
    public string ConfirmPassword { get; set; }
}
