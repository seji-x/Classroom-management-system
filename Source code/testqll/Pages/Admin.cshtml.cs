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
    public class AdminModel : PageModel
    {
        public AdminBLL busAd;
        public List<AdminDTO> lstAd;
        public List<AdminDTO> lstAd1;
        public int TotalPage;
        [BindProperty]
        [DisplayName("Mã Admin")]
        public string maAd { get; set; }

        [BindProperty]
        [DisplayName("Họ tên")]
        public string tenAd { get; set; }
        public AdminModel()
        {
            busAd = new AdminBLL();
        }
        public void OnGet()
        {
            int size = 10;
            lstAd = busAd.GetAll().ToList();
            lstAd1 = busAd.GetAll().Take(size).ToList();
            var totalRecord = busAd.GetAll().Count();
            TotalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)(totalRecord / size + 1);
        }
        public void OnPost()
        {
            lstAd = busAd.GetAll().ToList();
            TotalPage = 1;
            var temp1 = new List<AdminDTO>();
            int flat = 0;
            if (tenAd != null && tenAd != "")
            {
                temp1 = (from hs in lstAd
                         where hs.TenAdmin.ToLower().Contains(tenAd.ToLower())
                         select hs).ToList();
                lstAd1 = temp1;
                flat = 1;
            }
            if (maAd != null && maAd != "")
            {
                if(flat == 1)
                {
                    temp1 = lstAd1.Where(x=>x.MaAdmin == maAd).ToList();
                }  
                else
                {    
                    temp1 = (from hs in lstAd
                             where hs.MaAdmin== maAd
                             select hs).ToList();
                    lstAd1 = temp1;

                }
                flat = 1;
            }
            if (flat == 0)
            {
                OnGet();
            }

        }
        public IActionResult OnPostList(string filter)
        {
            var obj = JsonSerializer.Deserialize<Filter>(filter);
            var Data = busAd.GetAdByPage(obj.Page, obj.Size);
            return new ObjectResult(new { success = true, data = Data }) { StatusCode = 200 };
        }
        public IActionResult OnPostUpdate(String ad)
        {
            var obj = JsonSerializer.Deserialize<AdminDTO>(ad);
            var res = busAd.Update(obj);
            if (res)
                return new ObjectResult(new { success = true, ad = obj }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostDelete(String maAd)
        {
            var id = maAd;
            var res = busAd.Delete(id);
            if (res)
                return new ObjectResult(new { success = true }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostAdd(String ad)
        {
            var obj = JsonSerializer.Deserialize<AdminDTO>(ad);
            var res = busAd.Add(obj);
            if (res != null)
                return new ObjectResult(new { success = true, ad = res }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false, }) { StatusCode = 500 };
        }
    }
}
