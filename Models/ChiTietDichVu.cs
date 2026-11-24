using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class ChiTietDichVu
    {
        public int MaDatSan { get; set; }
        public int MaDichVu { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGiaLucDat { get; set; }

        public virtual DatSan MaDatSanNavigation { get; set; } = null!;
        public virtual DichVu MaDichVuNavigation { get; set; } = null!;
    }
}
