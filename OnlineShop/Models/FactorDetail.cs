
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class FactorDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        public Guid FactorId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Display(Name = "تعداد")]
        public int DetailCount { get; set; }

        [Display(Name ="قیمت نهایی")]
        public int FinalPrice { get; set; }

        [Display(Name ="توضیحات سفارش")]
        public string? Des { get; set; }


        [ForeignKey("FactorId")]
        public virtual Factor Factor { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
