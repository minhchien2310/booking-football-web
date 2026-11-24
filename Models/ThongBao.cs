using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class ThongBao
    {
        public int MaThongBao { get; set; }
        public string TieuDe { get; set; } = null!;
        public string NoiDung { get; set; } = null!;
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public bool? HienThi { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
