using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatSanBanh.Models
{
    public partial class NguoiDung
    {
        public int MaNguoiDung { get; set; }

        [DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Họ tên không được để trống!")]
        public string HoTen { get; set; } = null!;

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
        public string Email { get; set; } = null!;

        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Số điện thoại phải từ 10 đến 15 số!")]
        public string SoDienThoai { get; set; } = null!;

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải nhập tối thiểu 6 ký tự!")]
        public string MatKhau { get; set; } = null!;

        public string VaiTro { get; set; } = "KhachHang";
        public bool TrangThaiHoatDong { get; set; } = true;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        public virtual ICollection<DanhGia>? DanhGia { get; set; }
        public virtual ICollection<DatSanCoDinh>? DatSanCoDinhs { get; set; }
        public virtual ICollection<DatSan>? DatSans { get; set; }
        public virtual ICollection<KhoiPhucMatKhau>? KhoiPhucMatKhaus { get; set; }
    }
}
