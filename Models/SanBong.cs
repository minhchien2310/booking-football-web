using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class SanBong
    {
        public SanBong()
        {
            DanhGia = new HashSet<DanhGia>();
            DatSanCoDinhs = new HashSet<DatSanCoDinh>();
            DatSans = new HashSet<DatSan>();
            MaSanChas = new HashSet<SanBong>();
            MaSanCons = new HashSet<SanBong>();
        }

        public int MaSan { get; set; }
        public string TenSan { get; set; } = null!;
        public int LoaiSan { get; set; }
        public string? MoTa { get; set; }
        public string? HinhAnh { get; set; }
        public bool LaSanGop { get; set; }
        public string TrangThai { get; set; } = null!;

        public virtual ICollection<DanhGia> DanhGia { get; set; }
        public virtual ICollection<DatSanCoDinh> DatSanCoDinhs { get; set; }
        public virtual ICollection<DatSan> DatSans { get; set; }

        public virtual ICollection<SanBong> MaSanChas { get; set; }
        public virtual ICollection<SanBong> MaSanCons { get; set; }
    }
}
