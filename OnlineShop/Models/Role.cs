using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="کد نقش")]
        public Guid Id { get; set; }

        [Display(Name ="نام نقش")]
        public string RoleName { get; set; }//عنوان انگلیسی نقش

        [Display(Name ="عنوان نقش")]
        public string RoleTitle { get; set;  }//عنوان فارسی نقش

        public virtual ICollection<User> Users { get; set; }
    }
}
