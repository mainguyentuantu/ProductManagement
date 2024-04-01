using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models
{
    public class Order
    {
        [Key]
        public int MaDH { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là trường bắt buộc.")]
        public string TenKhachHang { get; set; }

        [ForeignKey("MaSP")]
        public Products? Product { get; set; }

        public int MaSP { get; set; }

        [Required(ErrorMessage = "Số lượng đặt hàng là trường bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng đặt hàng không được nhập số âm.")]
        public double SoLuongDH { get; set; }

        [Required(ErrorMessage = "Thành tiền là trường bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Thành tiền không được nhập số âm.")]
        public double ThanhTien { get; set; }

        [Required(ErrorMessage = "Thời gian tạo đơn hàng là trường bắt buộc.")]
        public DateTime ThoiGianTao { get; set; }

  

    }
}

