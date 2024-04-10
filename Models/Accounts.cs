using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class Accounts
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là trường bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự.")]
        public string UserName { get; set; }

		[Required(ErrorMessage = "Email là trường bắt buộc.")]
		[EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
		[StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Mật khẩu là trường bắt buộc.")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải chứa ít nhất {2} ký tự và không quá {1} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
