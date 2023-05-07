using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using QLL.BLL;
using QLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace QuanLyLop2_ASP.NETCore.Pages
{
    public class TaiKhoanGVModel : PageModel
    {
        private TaiKhoanGVBLL bus;
        public AdminBLL busAd;
        public GiaoVienBLL busGV;
        public HocSinhBLL busHS;
        public List<TaiKhoanGVDTO> lstTKGV;
        [BindProperty]
        [DisplayName("Mã tài khoản")]
        public int maTk { get; set; }
        [BindProperty]
        [DisplayName("Tên đăng nhâp")]
        public string tdn { get; set; }
        
        public TaiKhoanGVModel()
        {
            busAd = new AdminBLL();
            busGV = new GiaoVienBLL();
            busHS = new HocSinhBLL();
            bus = new TaiKhoanGVBLL();
        }
        public void OnGet()
        {
            lstTKGV = bus.GetAll().ToList();
        }
        public IActionResult OnGetTest()
        {
            return new ObjectResult(new { Id = 123, name = "hero" }) { StatusCode = 200 };
        }
        public void OnPost()
        {
            int flat = 0;
            if (maTk != 0)
            {
                lstTKGV = bus.GetAll().Where(x => x.MaTk == maTk).ToList();
                flat = 1;
            }
            if (tdn != null && tdn != "")
            {
                if(flat == 1)
                {
                    lstTKGV = bus.GetAll().Where(x => x.TenDangNhap == tdn && x.MaTk == maTk).ToList();
                }    
                else
                {
                    lstTKGV = bus.GetAll().Where(x => x.TenDangNhap == tdn).ToList();
                }
                flat = 1;
            }
            if(flat == 0)
            {
                OnGet();
            }    
        }
        public IActionResult OnPostUpdate(String tkgv)
        {
            var obj = JsonSerializer.Deserialize<TaiKhoanGVDTO>(tkgv);
            var res = bus.Update(obj);
            if (res)
                return new ObjectResult(new { success = true, tkgv = obj }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostDelete(String maTKGV)
        {
            var id = int.Parse(maTKGV);
            var res = bus.Delete(id);
            if (res)
                return new ObjectResult(new { success = true }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostAdd(String tkgv)
        {
            var obj = JsonSerializer.Deserialize<TaiKhoanGVDTO>(tkgv);
            var res = bus.Add(obj);
            if (res != null)
                return new ObjectResult(new { success = true, tkgv = res }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
    }
}
