using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class KhoiPhucMatKhau
    {
        public int MaKhoiPhuc { get; set; }
        public int MaNguoiDung { get; set; }
        public string MaOtp { get; set; } = null!;
        public DateTime ThoiGianHetHan { get; set; }
        public bool DaSuDung { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
    }
}
