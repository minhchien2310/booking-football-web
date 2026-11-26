using DatSanBanh.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// 1. Đăng ký dịch vụ lưu cache và Session (giống Bài 6 giáo trình STU)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(cfg =>
{
    cfg.Cookie.Name = "QuanLyDatSanBanh";
    cfg.IdleTimeout = new TimeSpan(0, 60, 0); // Tồn tại 60 phút
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

// 2. Kích hoạt Session (bắt buộc đặt trước MapControllerRoute)
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

