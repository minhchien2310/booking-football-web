using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class DanhGia
    {
        public int MaDanhGia { get; set; }
        public int MaDatSan { get; set; }
        public int MaNguoiDung { get; set; }
        public int MaSan { get; set; }
        public int SoSao { get; set; }
        public string? BinhLuan { get; set; }
        public DateTime NgayDanhGia { get; set; }

        public virtual DatSan MaDatSanNavigation { get; set; } = null!;
        public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
        public virtual SanBong MaSanNavigation { get; set; } = null!;
    }
}
