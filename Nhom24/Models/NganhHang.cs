using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom24.Models
{
    public class NganhHang
    {
        [Key]
        public string NganhHangID { get; set; }
        public string NganhHangName { get; set; }
        public string DanhMucSanPhamID { get; set; }
        [ForeignKey("DanhMucSanPhamID")]
        public DanhMucSanPham? DanhMucSanPham { get; set; }

    }
}
