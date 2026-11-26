using DatSanBanh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DatSanBanh.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DatSanController : Controller
    {
        QuanLyDatSanBanhContext db = new QuanLyDatSanBanhContext();

        private bool LaAdmin()
        {
            var session = HttpContext.Session.GetString("NguoiDung");
            if (string.IsNullOrEmpty(session)) return false;
            var nd = JsonConvert.DeserializeObject<NguoiDung>(session);
            return nd != null && (nd.VaiTro == "Admin" || nd.VaiTro == "QuanTri" || nd.VaiTro == "QuanTriVien" || nd.VaiTro == "NhanVien");
        }

        // 1. DANH SÁCH TẤT CẢ PHIẾU ĐẶT SÂN
        public IActionResult Index(string? trangThai)
        {
            if (!LaAdmin())
                return RedirectToAction("DangNhap", "TaiKhoan", new { area = "" });

            var query = db.DatSans
                .Include(d => d.MaNguoiDungNavigation)
                .Include(d => d.MaSanNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(trangThai))
            {
                query = query.Where(d => d.TrangThai == trangThai);
            }

            ViewBag.TrangThaiHienTai = trangThai;
            var dsDon = query.OrderByDescending(d => d.NgayTao).ToList();
            return View(dsDon);
        }

        // 2. DUYỆT / CHUYỂN TRẠNG THÁI ĐƠN NHANH
        [HttpPost]
        public IActionResult DoiTrangThai(int id, string trangThaiMoi)
        {
            if (!LaAdmin())
                return RedirectToAction("DangNhap", "TaiKhoan", new { area = "" });

            var hopLe = new[] { "ChoThanhToan", "DaCoc", "DaNhanSan", "HoanThanh", "DaHuy" };
            var don = db.DatSans.Find(id);

            if (don != null && hopLe.Contains(trangThaiMoi))
            {
                don.TrangThai = trangThaiMoi;
                db.SaveChanges();
                TempData["Success"] = $"Đã cập nhật phiếu #{don.MaDatSan} sang trạng thái mới.";
            }

            return RedirectToAction("Index");
        }
    }
}