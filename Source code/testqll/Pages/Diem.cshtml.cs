using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using QLL.BLL;
using QLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace QuanLyLop2_ASP.NETCore.Pages
{
    public class DiemModel : PageModel
    {
        public DiemBLL busd;
        public AdminBLL busAd;
        public HocSinhBLL busHS;
        public GiaoVienBLL busGV;
        public KhoaHocBLL busKH;
        public MonHocBLL busMH;
        public LopBLL busLop;
        public List<DiemDTO> lstd;
        public List<DiemDTO> lstd1;
        public List<KhoaHocDTO> lstKH;
        public int TotalPage;
        [BindProperty(SupportsGet = true)]
        public string maLop { get; set; }
        [BindProperty]
        [DisplayName("Mã khoá học")]
        public string maKh { get; set; }
        [BindProperty]
        [DisplayName("Mã môn học")]
        public string maMh { get; set; }
        [BindProperty]
        [DisplayName("Mã học sinh")]
        public string maHs { get; set; }
        [BindProperty]
        [DisplayName("Điểm")]
        public string diem { get; set; }
        public DiemModel()
        {
            busKH = new KhoaHocBLL();
            busMH = new MonHocBLL();
            busAd = new AdminBLL();
            busGV = new GiaoVienBLL();
            busHS = new HocSinhBLL();
            busd = new DiemBLL();
            busLop = new LopBLL();
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("user_id").Substring(0, 2) == "hs")
            {
                lstKH = busLop.GetAllKH(busHS.GetHSByID(HttpContext.Session.GetString("user_id"))
                    .MaLop).ToList();
            }
            if(HttpContext.Session.GetString("user_id").Substring(0, 2) == "ad")
            {
            int size = 10;
            lstd = busd.GetAll().ToList();
            lstd1 = busd.GetAll().Take(size).ToList();
            var totalRecord = busHS.GetAll().Count();
            TotalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)(totalRecord / size + 1);
            }
            if (HttpContext.Session.GetString("user_id").Substring(0, 2) == "gv")
            {
                lstd1 = busd.GetByMonGV(int.Parse(maLop), HttpContext.Session.GetString("user_id")).ToList();
                TotalPage = 1;
            }
        }
        public void OnPost()
        {
            lstd = busd.GetAll().ToList();
            if (HttpContext.Session.GetString("user_id").Substring(0, 2) == "gv")
            {
                lstd = busd.GetByMonGV(int.Parse(maLop), HttpContext.Session.GetString("user_id")).ToList(); 
            }
            TotalPage = 1;
            int flat = 0;
            var temp = new List<DiemDTO>();
            if(maHs != null & maHs != "")
            {
                if(flat == 0)
                {
                    temp = lstd.Where(x => x.MaHs == maHs).ToList();
                }
                else
                {
                    temp = lstd1.Where(x => x.MaHs == maHs).ToList();
                }
                lstd1 = temp;
                flat = 1;
            }
            if (maKh != null)
            {
                if (flat == 0)
                {
                    temp = lstd.Where(x => x.MaKh == int.Parse(maKh)).ToList();
                }
                else
                {
                    temp = lstd1.Where(x => x.MaKh == int.Parse(maKh)).ToList();
                }
                lstd1 = temp;
                flat = 1;
            }
            if (maMh != null)
            {
                if (flat == 0)
                {
                    temp = lstd.Where(x => x.MaMh == int.Parse(maMh)).ToList();
                }
                else
                {
                    temp = lstd1.Where(x => x.MaMh == int.Parse(maMh)).ToList();
                }
                lstd1 = temp;
                flat = 1;
            }
            if (diem != null)
            {
                if (flat == 0)
                {
                    temp = lstd.Where(x => x.Diem == int.Parse(diem)).ToList();
                }
                else
                {
                    temp = lstd1.Where(x => x.Diem == int.Parse(diem)).ToList();
                }
                lstd1 = temp;
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
            var Data = busd.GetDiemByPage(obj.Page, obj.Size);
            return new ObjectResult(new { success = true, data = Data }) { StatusCode = 200 };
        }
        public IActionResult OnPostUpdate(String d)
        {
            var obj = JsonSerializer.Deserialize<DiemDTO>(d);
            var res = busd.Update(obj);
            if (res)
                return new ObjectResult(new { success = true, d = obj }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostDelete(String maMH, string maHS)
        {
            var idMH = int.Parse(maMH);
            var idHS = maHS;
            var res = busd.Delete(idMH, idHS);
            if (res)
                return new ObjectResult(new { success = true }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostAdd(String d)
        {
            var obj = JsonSerializer.Deserialize<DiemDTO>(d);
            var res = busd.Add(obj);
            if (res != null)
                return new ObjectResult(new { success = true, d = res }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
    }
}
