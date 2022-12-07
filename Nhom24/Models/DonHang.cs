using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom24.Models
{
    public class DonHang
    {
        [Key]
        public string DonHangID { get; set; }
        public string DiaChiDonHang { get; set; }
        public double ThanhTien { get; set; }
        public string NguoiDungID { get; set; }
        [ForeignKey("NguoiDungID")]
        public NguoiDung? NguoiDung { get; set; }
    }
}
