using DatSanBanh.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DatSanBanh.Controllers
{
    public class TaiKhoanController : Controller
    {
        QuanLyDatSanBanhContext db = new QuanLyDatSanBanhContext();

        // ================= 1. ĐĂNG KÝ =================
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(NguoiDung nd, string XacNhanMatKhau)
        {
            if (ModelState.IsValid)
            {
                if (nd.MatKhau != XacNhanMatKhau)
                {
                    ModelState.AddModelError("", "Mật khẩu nhập lại không khớp!");
                    return View(nd);
                }

                NguoiDung? ktEmail = db.NguoiDungs.FirstOrDefault(x => x.Email == nd.Email);
                if (ktEmail != null)
                {
                    ModelState.AddModelError("", "Email này đã được sử dụng!");
                    return View(nd);
                }

                NguoiDung? ktSDT = db.NguoiDungs.FirstOrDefault(x => x.SoDienThoai == nd.SoDienThoai);
                if (ktSDT != null)
                {
                    ModelState.AddModelError("", "Số điện thoại này đã được đăng ký!");
                    return View(nd);
                }

                nd.VaiTro = "KhachHang";
                nd.TrangThaiHoatDong = true;
                nd.NgayTao = DateTime.Now;

                db.NguoiDungs.Add(nd);
                db.SaveChanges();


                // Đăng ký xong chuyển qua trang Đăng nhập kèm thông báo
                TempData["DangKyThanhCong"] = "Đăng ký tài khoản thành công! Vui lòng đăng nhập.";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            return View(nd);
        }

        // ================= 2. ĐĂNG NHẬP =================
        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangNhap(string Email, string MatKhau)
        {
            // Tìm người dùng theo Email và Mật khẩu trong SQL Server
            NguoiDung? nd = db.NguoiDungs.FirstOrDefault(x => x.Email == Email && x.MatKhau == MatKhau);

            if (nd == null)
            {
                ViewBag.LoiDangNhap = "Bạn nhập Email hoặc Mật khẩu bị sai!";
                return View();
            }

            if (nd.TrangThaiHoatDong == false)
            {
                ViewBag.LoiDangNhap = "Tài khoản của bạn đã bị khóa!";
                return View();
            }

            // Lưu thông tin đối tượng NguoiDung vào Session dưới dạng chuỗi JSON (giống Bài 6)
            string json = JsonConvert.SerializeObject(nd);
            HttpContext.Session.SetString("NguoiDung", json);

            // Phân luồng sau khi đăng nhập dựa vào VaiTro
            if (nd.VaiTro == "QuanTri" || nd.VaiTro == "NhanVien")
            {
                // Sau này tạo QuanTriController thì đổi thành RedirectToAction("Index", "QuanTri")
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        // ================= 3. ĐĂNG XUẤT =================
        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("NguoiDung");
            return RedirectToAction("Index", "Home");
        }
    }
}