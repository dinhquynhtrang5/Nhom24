using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom24.Models
{
    public class GioHang
    {
        [Key]
        public string NguoiDungID { get; set; }
        [ForeignKey("NguoiDungID")]
        public NguoiDung? NguoiDung { get; set; }

        public string SanPhamID { get; set; }
        [ForeignKey("SanPhamID")]
        public SanPham? SanPham { get; set; }
        public int SoLuongSanPham { get; set; }

    }
}
