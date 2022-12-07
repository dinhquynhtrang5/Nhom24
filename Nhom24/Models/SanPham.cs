using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom24.Models
{
    public class SanPham
    {
        [Key]
        public string SanPhamID { get; set; }
        public string SanPhamName { get; set; }
        public string AnhSanPham { get; set; }
        public double GiaSanPham { get; set; }
        public int SoLuongSanPham { get; set; }
        public string MoTaSanPham { get; set; }
        public string NganhHangID { get; set; }
        [ForeignKey("NganhHangID")]
        public NganhHang? NganhHang { get; set; }
    }
}
