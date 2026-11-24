using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            DanhGia = new HashSet<DanhGia>();
            DatSanCoDinhs = new HashSet<DatSanCoDinh>();
            DatSans = new HashSet<DatSan>();
            KhoiPhucMatKhaus = new HashSet<KhoiPhucMatKhau>();
        }

        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string SoDienThoai { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string VaiTro { get; set; } = null!;
        public bool? TrangThaiHoatDong { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual ICollection<DanhGia> DanhGia { get; set; }
        public virtual ICollection<DatSanCoDinh> DatSanCoDinhs { get; set; }
        public virtual ICollection<DatSan> DatSans { get; set; }
        public virtual ICollection<KhoiPhucMatKhau> KhoiPhucMatKhaus { get; set; }
    }
}
