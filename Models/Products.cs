using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class Products
    {
        [Key]
        public int MaSP { get; set; } 
        public string? TenSP {  get; set; }
        public string? KichThuoc { get; set; }
        public string? ChatLieu { get; set; }
        public string? MauSac {  get; set; }
        public string? KieuDang { get; set; }
        public string? ThuongHieu { get; set; } 
        public int GiaCa { get; set; }
        
      
    }
}
