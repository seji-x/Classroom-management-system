using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using QLL.BLL;
using QLL.DTO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace QuanLyLop2_ASP.NETCore.Pages
{
    public class HocSinhModel : PageModel
    {
        public HocSinhBLL busHS;
        public GiaoVienBLL busGV;
        public AdminBLL busAd;
        private LopBLL busLop;
        public List<HocSinhDTO> lstHS;
        public List<HocSinhDTO> lstHS1;
        public List<LopDTO> lstLop;
        public int TotalPage;
        [BindProperty]
        [DisplayName("Mã học sinh")]
        public string maHS { get; set; }

        [BindProperty]
        [DisplayName("Họ tên")]
        public string tenHS { get; set; }
        [BindProperty]
        [DisplayName("Mã lớp")]
        public int maLop { get; set; }
        public HocSinhModel()
        {
            busGV = new GiaoVienBLL();
            busAd = new AdminBLL();
            busHS = new HocSinhBLL();
            busLop = new LopBLL(); 
        }
        public void OnGet()
        {
            int size = 10;
            lstHS = busHS.GetAll().ToList();
            lstHS1 = busHS.GetAll().Take(size).ToList();
            lstLop = busLop.GetAll().ToList();
            var totalRecord = busHS.GetAll().Count();
            TotalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)(totalRecord / size + 1);
        }
        public void OnPost()
        {
            lstHS = busHS.GetAll().ToList();
            lstLop = busLop.GetAll().ToList();
            TotalPage = 1;
            int flat = 0;
            var temp = new List<HocSinhDTO>();
            if(maHS != null && maHS != "")
            {
                temp = lstHS.Where(x => x.MaHs == maHS).ToList();
                lstHS1 = temp;
                flat = 1;
            }
            if (maLop != 0 )
            {
                if(flat == 1)
                {
                    temp = lstHS1.Where(x => x.MaLop == maLop).ToList();
                }    
                else 
                { 
                    temp = lstHS.Where(x => x.MaLop == maLop).ToList();
                }
                lstHS1 = temp;
                flat = 1;
            }
            if (tenHS != null && tenHS != "")
            {
                if (flat == 1)
                {
                    temp = lstHS1.Where(x => x.TenHs.ToLower().Contains(tenHS.ToLower())).ToList();
                }
                else 
                {  
                    temp = lstHS.Where(x => x.TenHs.ToLower().Contains(tenHS.ToLower())).ToList();
                }
               
                lstHS1 = temp;
                flat = 1;
            }
            if(flat == 0)
            {
                OnGet();
            }    
        }
    
        public IActionResult OnPostList(string filter)
        {
            var obj = JsonSerializer.Deserialize<Filter>(filter);
            var Data = busHS.GetHSByPage(obj.Page, obj.Size);
            return new ObjectResult(new { success = true, data = Data }) { StatusCode = 200 };
        }
        public IActionResult OnPostUpdate(String hs)
        {
            var obj = JsonSerializer.Deserialize<HocSinhDTO>(hs);
            var res = busHS.Update(obj);
            if (res)
                return new ObjectResult(new { success = true, hs = obj }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostDelete(String maHS)
        {
            var id = maHS;
            var res = busHS.Delete(id);
            if (res)
                return new ObjectResult(new { success = true }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostAdd(String hs)
        {
            var obj = JsonSerializer.Deserialize<HocSinhDTO>(hs);
            var res = busHS.Add(obj);
            if (res != null)
                return new ObjectResult(new { success = true, hs = res }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false, }) { StatusCode = 500 };
        }
    }
}
