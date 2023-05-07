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
    public class TaiKhoanHSModel : PageModel
    {
        private TaiKhoanHSBLL bus;
        public AdminBLL busAd;
        public GiaoVienBLL busGV;
        public HocSinhBLL busHS;
        public List<TaiKhoanHSDTO> lstTKHS;
        [BindProperty]
        [DisplayName("Mã tài khoản")]
        public int maTk { get; set; }
        [BindProperty]
        [DisplayName("Tên đăng nhâp")]
        public string tdn { get; set; }
        public TaiKhoanHSModel()
        {
            busAd = new AdminBLL();
            busGV = new GiaoVienBLL();
            busHS = new HocSinhBLL();
            bus = new TaiKhoanHSBLL();
        }
        public void OnGet()
        {
            lstTKHS = bus.GetAll().ToList();
        }
        public void OnPost()
        {
            int flat = 0;
            if (maTk != 0)
            {
                lstTKHS = bus.GetAll().Where(x => x.MaTk == maTk).ToList();
                flat = 1;
            }
            if (tdn != null && tdn != "")
            {
                if(flat == 1)
                {
                    lstTKHS = bus.GetAll().Where(x => x.TenDangNhap == tdn && x.MaTk == maTk).ToList();
                }    
                else
                {
                    lstTKHS = bus.GetAll().Where(x => x.TenDangNhap == tdn).ToList();
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
        public IActionResult OnPostUpdate(String tkhs)
        {
            var obj = JsonSerializer.Deserialize<TaiKhoanHSDTO>(tkhs);
            var res = bus.Update(obj);
            if (res)
                return new ObjectResult(new { success = true, tkhs = obj }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostDelete(String maTKHS)
        {
            var id = int.Parse(maTKHS);
            var res = bus.Delete(id);
            if (res)
                return new ObjectResult(new { success = true }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostAdd(String tkhs)
        {
            var obj = JsonSerializer.Deserialize<TaiKhoanHSDTO>(tkhs);
            var res = bus.Add(obj);
            if (res != null)
                return new ObjectResult(new { success = true, tkhs = res }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
    }
}
