using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products? Product { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là trường bắt buộc.")]
        public string? TenKhachHang { get; set; }

        [Required(ErrorMessage = "Số lượng đặt hàng là trường bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng đặt hàng phải lớn hơn 0.")]
        public int SoLuongDatHang { get; set; }

        public double TongTien { get; set; }
    }
}
