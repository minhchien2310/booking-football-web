using System;
using System.Collections.Generic;

namespace DatSanBanh.Models
{
    public partial class SanBong
    {
        public int Id { get; set; }
        public string TenSan { get; set; } = null!;
        public string LoaiSan { get; set; } = null!;
        public decimal GiaThue { get; set; }
        public bool? TrangThai { get; set; }
    }
}
