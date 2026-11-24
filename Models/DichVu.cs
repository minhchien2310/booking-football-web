using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class DichVu
    {
        public DichVu()
        {
            ChiTietDichVus = new HashSet<ChiTietDichVu>();
        }

        public int MaDichVu { get; set; }
        public string TenDichVu { get; set; } = null!;
        public string DonViTinh { get; set; } = null!;
        public decimal DonGia { get; set; }
        public int SoLuongTon { get; set; }
        public bool? DangKinhDoanh { get; set; }

        public virtual ICollection<ChiTietDichVu> ChiTietDichVus { get; set; }
    }
}
