using DatSanBanh.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DatSanBanh.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        QuanLyDatSanBanhContext db = new QuanLyDatSanBanhContext();

        private bool KiemTraQuyenAdmin()
        {
            var session = HttpContext.Session.GetString("NguoiDung");
            if (session == null) return false;
            var nd = JsonConvert.DeserializeObject<NguoiDung>(session);
            return nd != null && (nd.VaiTro == "QuanTri" || nd.VaiTro == "NhanVien");
        }

        public IActionResult Index()
        {
            if (!KiemTraQuyenAdmin())
                return RedirectToAction("DangNhap", "TaiKhoan", new { area = "" });

            ViewBag.TongSan = db.SanBongs.Count();
            ViewBag.SanHoatDong = db.SanBongs.Count(s => s.TrangThai == "HoatDong");
            ViewBag.TongNguoiDung = db.NguoiDungs.Count();
            ViewBag.TongDonDat = db.DatSans.Count();
            return View();
        }
    }
}