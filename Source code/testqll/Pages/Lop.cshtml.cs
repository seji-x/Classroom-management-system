using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QLL.DTO;
using QLL.BLL;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace QuanLyLop2_ASP.NETCore.Pages
{
    
    public class LopModel : PageModel
    {
        public HocSinhBLL busHS;
        public GiaoVienBLL busGV;
        public AdminBLL busAd;
        public MonHocBLL busMh;
        public LopBLL bus;
        public List<LopDTO> lstLop;
        public List<MonHocDTO> lstMh;

        [BindProperty]
        [DisplayName("Mã lớp")]
        public int maLop { get; set; }
        [BindProperty]
        [DisplayName("Phòng học")]
        public string phong { get; set; }
        [BindProperty]
        [DisplayName("Trạng thái")]
        public string tt { get; set; }
        public LopModel()
        {
            busGV = new GiaoVienBLL();
            busAd = new AdminBLL();
            busHS = new HocSinhBLL();
            busMh = new MonHocBLL();
            bus = new LopBLL();

        }
        public void OnGet()
        {
            lstLop = bus.GetAll().ToList();
            if(HttpContext.Session.GetString("user_id").Substring(0,2) == "hs")
            {
                lstMh = busHS.GetMHById(HttpContext.Session.GetString("user_id")).ToList();
            }
        }
        public void OnPost()
        {
            int flat = 0;
            List<LopDTO> lst = new List<LopDTO>();
            if(maLop != 0)
            {
                lstLop = bus.GetAll().Where(x=>x.MaLop == maLop).ToList();
                lst = lstLop;
                flat = 1;
            }
            if (phong != null)
            {
                if(flat == 1)
                {
                    lstLop = lst.Where(x => x.PhongHoc == phong).ToList();
                    lst = lstLop;
                }
                else
                {
                    lstLop = bus.GetAll().Where(x => x.PhongHoc == phong).ToList();
                    lst = lstLop;
                }
                flat = 1;
            }
            if (tt != null)
            {
                if (flat == 1)
                {
                    lstLop = lst.Where(x => x.TrangThai.ToLower() == tt.ToLower()).ToList();
                    lst = lstLop;
                }
                else
                {
                    lstLop = bus.GetAll().Where(x => x.TrangThai.ToLower() == tt.ToLower()).ToList();
                    lst = lstLop;
                }
                flat = 1;
            }
            if(flat == 0)
            {
                OnGet();
            }
        }
        public IActionResult OnPostUpdate(String lop)
        {
            var obj = JsonSerializer.Deserialize<LopDTO>(lop);
            var res = bus.UpdateLop(obj);
            if (res)
                return new ObjectResult(new { success = true, lop = obj }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostDelete(String maLop)
        {
            var id = int.Parse(maLop);
            var res = bus.Delete(id);
            if (res)
                return new ObjectResult(new { success = true}) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostAdd(String lop)
        {
            var obj = JsonSerializer.Deserialize<LopDTO>(lop);
            var res = bus.Add(obj);
            if (res != null)
                return new ObjectResult(new { success = true, lop = res }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
    }
}
