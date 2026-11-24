using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class ThanhToan
    {
        public int MaThanhToan { get; set; }
        public int? MaDatSan { get; set; }
        public int? MaDatSanCoDinh { get; set; }
        public decimal SoTien { get; set; }
        public string PhuongThuc { get; set; } = null!;
        public string LoaiThanhToan { get; set; } = null!;
        public string? MaGiaoDich { get; set; }
        public string TrangThai { get; set; } = null!;
        public DateTime ThoiGianThanhToan { get; set; }

        public virtual DatSanCoDinh? MaDatSanCoDinhNavigation { get; set; }
        public virtual DatSan? MaDatSanNavigation { get; set; }
    }
}
