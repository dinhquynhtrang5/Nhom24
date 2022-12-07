using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nhom24.Models;

namespace Nhom24.Data
{
    public class Nhom24Context : DbContext
    {
        public Nhom24Context (DbContextOptions<Nhom24Context> options)
            : base(options)
        {
        }

        public DbSet<Nhom24.Models.DanhMucSanPham> DanhMucSanPham { get; set; } = default!;

        public DbSet<Nhom24.Models.NganhHang> NganhHang { get; set; }

        public DbSet<Nhom24.Models.SanPham> SanPham { get; set; }

        public DbSet<Nhom24.Models.NguoiDung> NguoiDung { get; set; }

        public DbSet<Nhom24.Models.GioHang> GioHang { get; set; }

        public DbSet<Nhom24.Models.DonHang> DonHang { get; set; }

        public DbSet<Nhom24.Models.ChiTietDonHang> ChiTietDonHang { get; set; }
    }
}
