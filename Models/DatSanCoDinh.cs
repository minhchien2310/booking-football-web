using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class DatSanCoDinh
    {
        public DatSanCoDinh()
        {
            DatSans = new HashSet<DatSan>();
            ThanhToans = new HashSet<ThanhToan>();
        }

        public int MaDatSanCoDinh { get; set; }
        public int MaNguoiDung { get; set; }
        public int MaSan { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string CacThuTrongTuan { get; set; } = null!;
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public int TongSoBuoi { get; set; }
        public int PhanTramGiam { get; set; }
        public decimal TongTien { get; set; }
        public decimal TienCoc { get; set; }
        public string TrangThai { get; set; } = null!;
        public DateTime NgayTao { get; set; }

        public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
        public virtual SanBong MaSanNavigation { get; set; } = null!;
        public virtual ICollection<DatSan> DatSans { get; set; }
        public virtual ICollection<ThanhToan> ThanhToans { get; set; }
    }
}
