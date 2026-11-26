using DatSanBanh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DatSanBanh.Controllers
{
    public class DatSanController : Controller
    {
        QuanLyDatSanBanhContext db = new QuanLyDatSanBanhContext();

        private NguoiDung? LayNguoiDungHienTai()
        {
            var session = HttpContext.Session.GetString("NguoiDung");
            if (string.IsNullOrEmpty(session)) return null;
            return JsonConvert.DeserializeObject<NguoiDung>(session);
        }

        // 1. TRANG ĐẶT LỊCH SÂN LẺ (GET)
        [HttpGet]
        public IActionResult DatLich(int? maSan)
        {
            var nd = LayNguoiDungHienTai();
            if (nd == null)
            {
                TempData["Error"] = "Vui lòng đăng nhập để đặt sân!";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            ViewBag.DanhSachSan = db.SanBongs.Where(s => s.TrangThai == "HoatDong").ToList();
            ViewBag.BangGia = db.BangGia.OrderBy(b => b.LoaiSan).ThenBy(b => b.LaCuoiTuan).ThenBy(b => b.GioBatDau).ToList();

            // Sân đóng cửa lúc 22:00 -> giờ bắt đầu muộn nhất là 21:00
            int gioMacDinh = DateTime.Now.Hour + 1;
            DateTime ngayMacDinh = DateTime.Today;
            if (gioMacDinh > 21)
            {
                ngayMacDinh = DateTime.Today.AddDays(1);
                gioMacDinh = 17;
            }

            var model = new DatSan
            {
                MaSan = maSan,
                NgayDat = ngayMacDinh,
                GioBatDau = new TimeSpan(gioMacDinh, 0, 0),
                GioKetThuc = new TimeSpan(gioMacDinh + 1, 0, 0)
            };
            return View(model);
        }

        // 2. XỬ LÝ LƯU ĐƠN ĐẶT SÂN (POST)
        [HttpPost]
        public IActionResult DatLich(DatSan model)
        {
            var nd = LayNguoiDungHienTai();
            if (nd == null)
                return RedirectToAction("DangNhap", "TaiKhoan");

            ViewBag.DanhSachSan = db.SanBongs.Where(s => s.TrangThai == "HoatDong").ToList();
            ViewBag.BangGia = db.BangGia.OrderBy(b => b.LoaiSan).ThenBy(b => b.LaCuoiTuan).ThenBy(b => b.GioBatDau).ToList();

            if (!model.MaSan.HasValue || !model.NgayDat.HasValue || !model.GioBatDau.HasValue || !model.GioKetThuc.HasValue)
            {
                ViewBag.Loi = "Vui lòng chọn đầy đủ sân, ngày đá và khung giờ!";
                return View(model);
            }

            if (model.NgayDat.Value.Date < DateTime.Today)
            {
                ViewBag.Loi = "Không thể đặt sân cho ngày trong quá khứ!";
                return View(model);
            }

            DateTime thoiDiemBatDau = model.NgayDat.Value.Date.Add(model.GioBatDau.Value);
            if (thoiDiemBatDau <= DateTime.Now)
            {
                ViewBag.Loi = $"Khung giờ {model.GioBatDau.Value:hh\\:mm} hôm nay đã qua (hiện tại là {DateTime.Now:HH:mm}). Vui lòng chọn giờ muộn hơn!";
                return View(model);
            }

            if (model.GioKetThuc <= model.GioBatDau)
            {
                ViewBag.Loi = "Giờ kết thúc phải lớn hơn giờ bắt đầu!";
                return View(model);
            }

            // Chặn đặt ngoài giờ hoạt động (06:00 - 22:00)
            if (model.GioBatDau < new TimeSpan(6, 0, 0) || model.GioKetThuc > new TimeSpan(22, 0, 0))
            {
                ViewBag.Loi = "Sân chỉ hoạt động từ 06:00 đến 22:00. Vui lòng chọn giờ kết thúc tối đa 22:00!";
                return View(model);
            }

            bool biTrungLich = db.DatSans.Any(d =>
                d.MaSan == model.MaSan &&
                d.NgayDat.HasValue && d.NgayDat.Value.Date == model.NgayDat.Value.Date &&
                d.TrangThai != "DaHuy" &&
                model.GioBatDau < d.GioKetThuc &&
                model.GioKetThuc > d.GioBatDau);

            if (biTrungLich)
            {
                ViewBag.Loi = "Khung giờ này sân đã có người đặt! Vui lòng chọn giờ hoặc sân khác.";
                return View(model);
            }

            var san = db.SanBongs.Find(model.MaSan.Value);
            if (san == null)
            {
                ViewBag.Loi = "Sân bóng không tồn tại!";
                return View(model);
            }

            // Tính tiền chuẩn từng giờ một (nếu đá vắt qua 17:00 sẽ tự cộng đúng giá thường + giá cao điểm)
            bool laCuoiTuan = model.NgayDat.Value.DayOfWeek == DayOfWeek.Saturday ||
                              model.NgayDat.Value.DayOfWeek == DayOfWeek.Sunday;

            var dsGiaSan = db.BangGia
                .Where(g => g.LoaiSan == san.LoaiSan && g.LaCuoiTuan == laCuoiTuan)
                .ToList();

            decimal tongTienSan = 0;
            for (int h = model.GioBatDau.Value.Hours; h < model.GioKetThuc.Value.Hours; h++)
            {
                var gioHienTai = new TimeSpan(h, 0, 0);
                var giaKhung = dsGiaSan.FirstOrDefault(g => gioHienTai >= g.GioBatDau && gioHienTai < g.GioKetThuc);
                tongTienSan += giaKhung != null ? giaKhung.GiaMoiGio : (san.LoaiSan == 7 ? 500000 : 250000);
            }

            model.MaNguoiDung = nd.MaNguoiDung;
            model.GiaSan = Math.Round(tongTienSan, 0);
            model.TienGiam = 0;
            model.TongTien = model.GiaSan - model.TienGiam;
            model.TienCoc = Math.Round(model.TongTien * 0.3m, 0);
            model.TrangThai = "ChoThanhToan";
            model.HanGiuCho = DateTime.Now.AddMinutes(30);
            model.NgayTao = DateTime.Now;

            db.DatSans.Add(model);
            db.SaveChanges();

            TempData["Success"] = $"Đặt sân thành công! Tổng tiền: {model.TongTien:N0}đ (Cọc 30%: {model.TienCoc:N0}đ).";
            return RedirectToAction("LichSu");
        }

        // 3. LỊCH SỬ ĐẶT SÂN CỦA KHÁCH HÀNG
        public IActionResult LichSu()
        {
            var nd = LayNguoiDungHienTai();
            if (nd == null)
            {
                TempData["Error"] = "Vui lòng đăng nhập để xem lịch sử đặt sân!";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            var dsDon = db.DatSans
                .Include(d => d.MaSanNavigation)
                .Where(d => d.MaNguoiDung == nd.MaNguoiDung)
                .OrderByDescending(d => d.NgayTao)
                .ToList();

            return View(dsDon);
        }

        // 4. KHÁCH HÀNG HỦY ĐƠN KHI ĐANG CHỜ THANH TOÁN CỌC
        [HttpPost]
        public IActionResult HuyDon(int id)
        {
            var nd = LayNguoiDungHienTai();
            if (nd == null) return RedirectToAction("DangNhap", "TaiKhoan");

            var don = db.DatSans.FirstOrDefault(d => d.MaDatSan == id && d.MaNguoiDung == nd.MaNguoiDung);
            if (don != null && don.TrangThai == "ChoThanhToan")
            {
                don.TrangThai = "DaHuy";
                db.SaveChanges();
                TempData["Success"] = $"Đã hủy phiếu đặt sân #{don.MaDatSan}.";
            }
            return RedirectToAction("LichSu");
        }
    }
}