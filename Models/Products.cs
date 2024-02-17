using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class Products
    {
        [Key]
        public int MaSP { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là trường bắt buộc.")]
        [StringLength(20, ErrorMessage = "Tên sản phẩm không được vượt quá 20 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9\sàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđĐ]*$",
    ErrorMessage = "Tên sản phẩm chỉ được nhập số, chữ và ký tự tiếng Việt.")]
        public string? TenSP { get; set; }

        [StringLength(20, ErrorMessage = "Kích thước không được vượt quá 20 ký tự.")]
        [RegularExpression(@"^[0-9*]+$", ErrorMessage = "Kích thước chỉ được nhập số và dấu (*).")]
        public string? KichThuoc { get; set; }

        [StringLength(15, ErrorMessage = "Chất liệu không được vượt quá 15 ký tự.")]
        [RegularExpression(@"^[a-zA-Z\sàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđĐ]*$",
    ErrorMessage = "Chất liệu chỉ được chữ và ký tự tiếng Việt.")]
        public string? ChatLieu { get; set; }

        [StringLength(25, ErrorMessage = "Màu sắc không được vượt quá 25 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9\sàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđĐ]*$",
    ErrorMessage = "Màu sắc chỉ được số, chữ và ký tự tiếng Việt.")]
        public string? MauSac { get; set; }

        [StringLength(30, ErrorMessage = "Chất liệu không được vượt quá 30 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9\sàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđĐ]*$",
    ErrorMessage = "Kiểu dáng chỉ được số, chữ và ký tự tiếng Việt.")]
        public string? KieuDang { get; set; }

        [StringLength(30, ErrorMessage = "Chất liệu không được vượt quá 30 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9]*$",
    ErrorMessage = "Chất liệu chỉ được số và chữ và không hỗ trợ kí tự tiếng Việt.")]
        public string? ThuongHieu { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm là trường bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm không được nhập số âm.")]
        public double GiaCa { get; set; }

        [Required(ErrorMessage = "Số lượng tổng là trường bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tổng không được nhập số âm.")]
        public int SoLuongTong { get; set; }

        public ICollection<Order>? Orders { get; set; } // Danh sách các đơn hàng liên kết với sản phẩm
    }

    public class ProductModel
    {
        public List<Products>? ProductList { get; set; }
    }
}
