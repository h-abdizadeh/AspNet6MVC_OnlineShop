using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "گروه")]
        [Required(ErrorMessage = "گروه محصول را انتخاب کنید")]
        public int GroupId { get; set; }

        [Display(Name = "نام محصول", Prompt = "نام محصول")]
        [Required(ErrorMessage = "نام محصول الزامیست")]
        [MaxLength(25, ErrorMessage = "نام محصول نمی توانید بیش از 25 کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "درج قیمت الزامیست")]
        public uint Price { get; set; }

        [Display(Name = "درصد تخفیف")]
        public byte SellOff { get; set; } = 0;

        [Display(Name = "تصویر محصول")]
        //[Required(ErrorMessage = "ثبت تصویر محصول الزامیست")]
        public string? Img { get; set; }

        [Display(Name = "درباره محصول", Prompt = "درباره محصول")]
        [MaxLength(512, ErrorMessage = "توضیحات نمی تواند بیش از 512 کاراکتر باشد")]
        public string? Des { get; set; }

        [Display(Name = "موجودی", Prompt = "موجودی")]
        [Required(ErrorMessage = "تعداد موجودی کالا الزامیست")]
        public uint Inventory { get; set; }=0;

        [Display(Name = "بازدید")]
        public uint Seen { get; set; } = 0;

        [Display(Name = "عدم نمایش")]
        public bool NotShow { get; set; }


        [ForeignKey("GroupId")]
        public virtual Group? Group { get; set; }
    }
}
