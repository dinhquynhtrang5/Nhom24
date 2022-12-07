using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Nhom24.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public string DonHangID { get; set; }
        [ForeignKey("DonHangID")]
        public DonHang? DonHang { get; set; }

        public string SanPhamID { get; set; }
        [ForeignKey("SanPhamID")]
        public SanPham? SanPham { get; set; }
        public int SoLuongSanPham { get; set; }
    }
}
