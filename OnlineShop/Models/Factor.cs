using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Factor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Display(Name ="شماره فاکتور")]
        public int FactorNumber { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string? OpenDate { get; set; }

        [Display(Name = "تاریخ پرداخت")]
        public string? CloseDate { get; set; }

        [Display(Name = "وضعیت پرداخت")]
        public bool IsPay { get; set; } = false;

        [Display(Name = "وضعیت سفارش")]
        public string? State { get; set; }

        [Display(Name = "توضیحات")]
        public string? Des { get; set; }

        [Display(Name = "مبلغ فاکتور")]
        public int TotalPrice { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<FactorDetail>? FactorDetails { get; set; }

    }
}
