using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class BangGia
    {
        public int MaBangGia { get; set; }
        public int LoaiSan { get; set; }
        public bool LaCuoiTuan { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public decimal GiaMoiGio { get; set; }
    }
}
