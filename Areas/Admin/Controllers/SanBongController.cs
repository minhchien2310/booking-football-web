using DatSanBanh.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DatSanBanh.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanBongController : Controller
    {
        QuanLyDatSanBanhContext db = new QuanLyDatSanBanhContext();

        private bool KiemTraQuyenAdmin()
        {
            var session = HttpContext.Session.GetString("NguoiDung");
            if (session == null) return false;
            var nd = JsonConvert.DeserializeObject<NguoiDung>(session);
            return nd != null && (nd.VaiTro == "QuanTri" || nd.VaiTro == "NhanVien");
        }

        // 1. READ: Danh sách sân bóng
        public IActionResult Index()
        {
            if (!KiemTraQuyenAdmin())
                return RedirectToAction("DangNhap", "TaiKhoan", new { area = "" });

            var dsSan = db.SanBongs.ToList();
            return View(dsSan);
        }

        // 2. CREATE: Thêm sân mới
        [HttpGet]
        public IActionResult Them()
        {
            if (!KiemTraQuyenAdmin())
                return RedirectToAction("DangNhap", "TaiKhoan", new { area = "" });
            return View();
        }

        [HttpPost]
        public IActionResult Them(SanBong model)
        {
            if (!KiemTraQuyenAdmin())
                return RedirectToAction("DangNhap", "TaiKhoan", new { area = "" });

            if (string.IsNullOrWhiteSpace(model.TenSan))
            {
                ViewBag.Loi = "Vui lòng nhập tên sân.";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.TrangThai))
                model.TrangThai = "HoatDong";

            db.SanBongs.Add(model);
            db.SaveChanges();
            TempData["Success"] = "Thêm sân bóng mới thành công!";
            return RedirectToAction("Index");
        }

        // 3. UPDATE: Sửa thông tin sân
        [HttpGet]
        public IActionResult Sua(int id)
        {
            if (!KiemTraQuyenAdmin())
                return RedirectToAction("DangNhap", "TaiKhoan", new { area = "" });

            var san = db.SanBongs.Find(id);
            if (san == null) return NotFound();
            return View(san);
        }

        [HttpPost]
        public IActionResult Sua(SanBong model)
        {
            if (!KiemTraQuyenAdmin())
                return RedirectToAction("DangNhap", "TaiKhoan", new { area = "" });

            var san = db.SanBongs.Find(model.MaSan);
            if (san == null) return NotFound();

            san.TenSan = model.TenSan;
            san.LoaiSan = model.LoaiSan;
            san.MoTa = model.MoTa;
            san.TrangThai = model.TrangThai;

            db.SaveChanges();
            TempData["Success"] = "Cập nhật thông tin sân thành công!";
            return RedirectToAction("Index");
        }

        // 4. DELETE: Xóa sân
        [HttpPost]
        public IActionResult Xoa(int id)
        {
            if (!KiemTraQuyenAdmin())
                return RedirectToAction("DangNhap", "TaiKhoan", new { area = "" });

            var san = db.SanBongs.Find(id);
            if (san != null)
            {
                bool daCoDon = db.DatSans.Any(d => d.MaSan == id) || db.DatSanCoDinhs.Any(c => c.MaSan == id);
                if (daCoDon)
                {
                    TempData["Error"] = "Không thể xóa sân đã có đơn đặt! Hãy chuyển sang trạng thái Bảo trì.";
                    return RedirectToAction("Index");
                }

                db.SanBongs.Remove(san);
                db.SaveChanges();
                TempData["Success"] = "Đã xóa sân bóng!";
            }
            return RedirectToAction("Index");
        }
    }
}