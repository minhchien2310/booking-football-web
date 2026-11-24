using Microsoft.AspNetCore.Mvc;
using DatSanBanh.Models;

namespace DatSanBanh.Controllers
{
    public class HomeController : Controller
    {
        QuanLyDatSanBanhContext db = new QuanLyDatSanBanhContext();

        public IActionResult Index()
        {
            var homNay = DateTime.Today;

            ViewBag.ThongBao = db.ThongBaos
                .Where(t => t.HienThi == true && (t.TuNgay == null || t.TuNgay <= homNay) && (t.DenNgay == null || t.DenNgay >= homNay))
                .OrderByDescending(t => t.NgayTao)
                .ToList();

            ViewBag.ds = db.SanBongs
                .Where(s => s.TrangThai == "HoatDong")
                .ToList();

            ViewBag.BangGia = db.BangGia
                .OrderBy(b => b.LoaiSan)
                .ThenBy(b => b.LaCuoiTuan)
                .ThenBy(b => b.GioBatDau)
                .ToList();

            return View();
        }
    }
}