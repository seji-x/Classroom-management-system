using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using QLL.BLL;
using QLL.DTO;
using System.ComponentModel;

namespace QuanLyLop.Pages
{
    public class ChiTietTKBModel : PageModel
    {
        private TKBCTBLL bus;
        public AdminBLL busAd;
        public List<TKBCTDTO> lst1;
        public List<TKBCTDTO> lst;
        [BindProperty]
        [DisplayName("Thứ")]
        public string thu { get; set; }
        [BindProperty]
        [DisplayName("Tiết")]
        public string tiet { get; set; }
        [BindProperty]
        [DisplayName("Mã môn")]
        public string maMh { get; set; }
        [BindProperty]
        [DisplayName("Mã lớp")]
        public string maLop { get; set; }

        [BindProperty(SupportsGet = true)]
        public int maTKB { get; set; }
        public int TotalPage;
        public ChiTietTKBModel()
        {
            bus = new TKBCTBLL();
            busAd = new AdminBLL();
        }
        public void OnGet()
        {
            int size = 10;
            lst = bus.GetById(maTKB).ToList();
            lst1 = bus.GetById(maTKB).Take(size).ToList();
            var totalRecord = bus.GetAll().Count();
            TotalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)(totalRecord / size + 1);
        }
        public void OnPost()
        {
            lst = bus.GetById(maTKB).ToList();
            TotalPage = 1;
            var temp = new List<TKBCTDTO>();
            int flat = 0;
            if (thu != null && thu != "")
            {
                temp = lst.Where(x => x.Thu == int.Parse(thu)).ToList();
                lst1 = temp;
                flat = 1;
            }
            if (tiet != null && tiet != "")
            {
                if(flat == 1)
                {
                    temp = lst1.Where(x => x.Tiet == int.Parse(tiet)).ToList();
                }
                else
                {
                    temp = lst.Where(x => x.Tiet == int.Parse(tiet)).ToList();
                }
                lst1 = temp;
                flat = 1;
            }
            if (maLop != null && maLop != "")
            {
                if (flat == 1)
                {
                    temp = lst1.Where(x => x.Malop == int.Parse(maLop)).ToList();
                }
                else
                {
                    temp = lst.Where(x => x.Malop == int.Parse(maLop)).ToList();
                }
                lst1 = temp;
                flat = 1;
            }
            if (maMh != null && maMh != "")
            {
                if (flat == 1)
                {
                    temp = lst1.Where(x => x.MaMh == int.Parse(maMh)).ToList();
                }
                else
                {
                    temp = lst.Where(x => x.MaMh == int.Parse(maMh)).ToList();
                }
                lst1 = temp;
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
            var Data = bus.GetTKBByPage(obj.Page, obj.Size);
            return new ObjectResult(new { success = true, data = Data }) { StatusCode = 200 };
        }
        public IActionResult OnPostUpdate(String tkbct)
        {
            var obj = JsonSerializer.Deserialize<TKBCTDTO>(tkbct);
            var res = bus.Update(obj);
            if (res)
                return new ObjectResult(new { success = true, tkbct = obj }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostDelete(String maLop, string thu, string tiet)
        {
            var idLop = int.Parse(maLop);
            var prThu = int.Parse(thu);
            var prTiet = int.Parse(tiet);
            var res = bus.Delete(idLop, prThu, prTiet);
            if (res)
                return new ObjectResult(new { success = true }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostAdd(String tkbct)
        {
            var obj = JsonSerializer.Deserialize<TKBCTDTO>(tkbct);
            var res = bus.Add(obj);
            if (res != null)
                return new ObjectResult(new { success = true, tkbct = res }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
    }
}
