using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using QLL.DTO;
using QLL.BLL;
using System.Collections.Generic;
using System.Linq;
using System;

namespace QuanLyLop2_ASP.NETCore.Pages
{
    public class MonHocModel : PageModel
    {
        private MonHocBLL bus;
        public AdminBLL busAd;
        public HocSinhBLL busHS;
        public GiaoVienBLL busGV;
        public List<MonHocDTO> lstMH;
        public List<MonHocDTO> lstMH1;
        public int TotalPage;
        public MonHocModel()
        {

            bus = new MonHocBLL();
        }
        public void OnGet()
        {
            int size = 10;
            busAd = new AdminBLL();
            busGV = new GiaoVienBLL();
            busHS = new HocSinhBLL();
            lstMH = bus.GetAll().ToList();
            lstMH1 = bus.GetAll().Take(size).ToList();
            var totalRecord = busHS.GetAll().Count();
            TotalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)(totalRecord / size + 1);
            
        }
        public IActionResult OnPostList(string filter)
        {
            var obj = JsonSerializer.Deserialize<Filter>(filter);
            var Data = bus.GetMHByPage(obj.Page, obj.Size);
            return new ObjectResult(new { success = true, data = Data }) { StatusCode = 200 };
        }
        public IActionResult OnPostUpdate(String mh)
        {
            var obj = JsonSerializer.Deserialize<MonHocDTO>(mh);
            var res = bus.Update(obj);
            if (res)
                return new ObjectResult(new { success = true, mh = obj }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostDelete(String maMH)
        {
            var id = int.Parse(maMH);
            var res = bus.Delete(id);
            if (res)
                return new ObjectResult(new { success = true }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostAdd(String mh)
        {
            var obj = JsonSerializer.Deserialize<MonHocDTO>(mh);
            var res = bus.Add(obj);
            if (res != null)
                return new ObjectResult(new { success = true, mh = res }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
    }
}
