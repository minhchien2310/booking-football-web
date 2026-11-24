using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class MaGiamGia
    {
        public MaGiamGia()
        {
            DatSans = new HashSet<DatSan>();
        }

        public int MaVoucher { get; set; }
        public string MaCode { get; set; } = null!;
        public int PhanTramGiam { get; set; }
        public decimal? GiamToiDa { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public int GioiHanLuotDung { get; set; }
        public int DaSuDung { get; set; }

        public virtual ICollection<DatSan> DatSans { get; set; }
    }
}
