using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineShop.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="عنوان گروه",Prompt = "عنوان گروه")]
        [Required(ErrorMessage ="عنوان گروه نمیتواند خالی باشد")]
        [MaxLength(15,ErrorMessage ="عنوان گروه نمیتواند بیش از 15 کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name ="تصویر گروه")]
        [AllowNull]
        public string? Img { get; set; }

        [Display(Name="عدم نمایش")]
        public bool NotShow { get; set; }//=false;


        public virtual ICollection<Product>? Products { get; set; }

    }
}
