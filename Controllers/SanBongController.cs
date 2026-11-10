using DatSanBanh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatSanBanh.Controllers
{
    public class SanBongController : Controller
    {
        private readonly DatSanBanhDbContext db;
        public SanBongController(DatSanBanhDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            ViewBag.sb = db.SanBongs;
            return View();
        }
        [HttpGet]
        public ActionResult them()
        {
            return View();
        }
        [HttpPost]
        public ActionResult them(SanBong n)
        {
            if (ModelState.IsValid)
            {
                if (db.SanBongs.Find(n.Id) != null)
                {
                    ModelState.AddModelError("", "Mã phiếu thuê bị trùng!");
                    return View(n);
                }
                else
                {
                    db.SanBongs.Add(n);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(n);
            }
        }
        [HttpGet]
        public ActionResult sua(int id)
        {
            SanBong sb = db.SanBongs.Find(id);
            if (sb == null) return NotFound();
            ViewBag.TrangThaiList = new List<SelectListItem>
    {
        new SelectListItem("Còn trống", "true", sb.TrangThai == true),
        new SelectListItem("Đã đặt", "false", sb.TrangThai == false)
    };
            return View(sb);
        }
        [HttpPost]
        public ActionResult sua(SanBong n)
        {
            SanBong sb = db.SanBongs.Find(n.Id);
            sb.TenSan = n.TenSan;
            sb.LoaiSan = n.LoaiSan;
            sb.GiaThue = n.GiaThue;
            sb.TrangThai = n.TrangThai;
            db.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult xoa(int id)
        {
            SanBong n = db.SanBongs.Find(id);
            return View(n);
        }
        [HttpPost, ActionName("xoa")]
        public ActionResult xoa_Post(int id)
        {
            SanBong n = db.SanBongs.Find(id);
            if (n != null)
            {
                db.SanBongs.Remove(n);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}
