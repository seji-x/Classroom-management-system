using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLL.BLL;
using QLL.DTO;

namespace QuanLyLop.Pages
{
    public class IndexModel : PageModel
    {
        private TaiKhoanHSBLL busTkHs;
        private TaiKhoanGVBLL busTkGv;
        private TaiKhoanAdminBLL busTkAd;
        [BindProperty]
        public string Selected { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string Msg { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            string res = "";

            switch (Selected)
            {
                case "HocSinh":
                    busTkHs = new TaiKhoanHSBLL();
                    res = busTkHs.login(Username, Password);
                    break;
                case "GiaoVien":
                    busTkGv = new TaiKhoanGVBLL();
                    res = busTkGv.login(Username, Password);
                    break;
                case "Admin":
                    busTkAd = new TaiKhoanAdminBLL();
                    res = busTkAd.login(Username, Password);
                    break;
            }

            if (res != "")
            {
                HttpContext.Session.SetString("user_id", res);
                return RedirectToPage("Home");
            }
            else
            {
                Msg = "Sai tên đăng nhập hoặc mật khẩu";
                return Page();
            }
        }
    }
}