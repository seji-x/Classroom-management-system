using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using QLL.DTO;
using QLL.BLL;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace QuanLyLop2_ASP.NETCore.Pages
{
    public class TaiKhoanAdminModel : PageModel
    {
        private TaiKhoanAdminBLL bus;
        public AdminBLL busAd;
        public List<TaiKhoanAdminDTO> lstTKA;
        [BindProperty]
        [DisplayName("Mã tài khoản")]
        public int maTk { get; set; }
        [BindProperty]
        [DisplayName("Tên đăng nhâp")]
        public string tdn { get; set; }

        public TaiKhoanAdminModel()
        {
            busAd = new AdminBLL();
            bus = new TaiKhoanAdminBLL();
        } 
        public void OnGet()
        {
            lstTKA = bus.GetAll().ToList();
        }
        public void OnPost()
        {
            int flat = 0;
            if(maTk != 0 )
            {
                lstTKA = bus.GetAll().Where(x => x.MaTk == maTk).ToList();
                flat = 1;
            }
            if (tdn != null && tdn !="" )
            {
                if(flat == 1)
                {
                    lstTKA = bus.GetAll().Where(x => x.TenDangNhap == tdn && x.MaTk == maTk).ToList();

                }
                else
                {
                    lstTKA = bus.GetAll().Where(x => x.TenDangNhap == tdn).ToList();

                }
                flat = 1;
            }
            if(flat == 0)
            {
                OnGet();
            }    
        }    
        public IActionResult OnGetTest()
        {
            return new ObjectResult(new { Id = 123, name = "hero" }) { StatusCode = 200 };
        }
        public IActionResult OnPostUpdate(String tka)
        {
            var obj = JsonSerializer.Deserialize<TaiKhoanAdminDTO>(tka);
            var res = bus.Update(obj);
            if (res)
                return new ObjectResult(new { success = true, tka = obj }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostDelete(String maTKA)
        {
            var id = int.Parse(maTKA);
            var res = bus.Delete(id);
            if (res)
                return new ObjectResult(new { success = true }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostAdd(String tka)
        {
            var obj = JsonSerializer.Deserialize<TaiKhoanAdminDTO>(tka);
            var res = bus.Add(obj);
            if (res != null)
                return new ObjectResult(new { success = true, tka = res }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
    }
}
