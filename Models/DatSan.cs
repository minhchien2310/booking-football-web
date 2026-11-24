using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatSanBanh.Models
{
    public partial class DatSan
    {
        [DisplayName("Mã phiếu")]
        public int MaDatSan { get; set; }

        [DisplayName("Khách hàng")]
        public int MaNguoiDung { get; set; }

        [DisplayName("Sân bóng")]
        [Required(ErrorMessage = "Vui lòng chọn sân bóng!")]
        public int? MaSan { get; set; }

        public int? MaDatSanCoDinh { get; set; }
        public int? MaVoucher { get; set; }

        [DisplayName("Ngày đá")]
        [Required(ErrorMessage = "Ngày đặt sân không được để trống!")]
        public DateTime? NgayDat { get; set; }

        [DisplayName("Giờ bắt đầu")]
        [Required(ErrorMessage = "Vui lòng chọn khung giờ bắt đầu!")]
        public TimeSpan? GioBatDau { get; set; }

        [DisplayName("Giờ kết thúc")]
        [Required(ErrorMessage = "Vui lòng chọn khung giờ kết thúc!")]
        public TimeSpan? GioKetThuc { get; set; }

        [DisplayName("Tiền sân")]
        public decimal GiaSan { get; set; }

        [DisplayName("Tiền giảm")]
        public decimal TienGiam { get; set; }

        [DisplayName("Tổng tiền")]
        public decimal TongTien { get; set; }

        [DisplayName("Tiền cọc (30%)")]
        public decimal TienCoc { get; set; }

        [DisplayName("Trạng thái")]
        public string? TrangThai { get; set; }

        public DateTime? HanGiuCho { get; set; }

        [DisplayName("Ghi chú")]
        public string? GhiChu { get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime? NgayTao { get; set; }

        [DisplayName("Khung giờ")]
        public string KhungGioText => (GioBatDau.HasValue && GioKetThuc.HasValue)
            ? $"{GioBatDau.Value:hh\\:mm} – {GioKetThuc.Value:hh\\:mm}"
            : "";

        public virtual ICollection<ChiTietDichVu> ChiTietDichVus { get; set; } = new List<ChiTietDichVu>();
        public virtual DanhGia? DanhGia { get; set; }
        public virtual DatSanCoDinh? MaDatSanCoDinhNavigation { get; set; }
        public virtual NguoiDung? MaNguoiDungNavigation { get; set; }
        public virtual SanBong? MaSanNavigation { get; set; }
        public virtual MaGiamGia? MaVoucherNavigation { get; set; }
        public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
    }
}