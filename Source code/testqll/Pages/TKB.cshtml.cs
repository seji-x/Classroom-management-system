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
    public class TKBModel : PageModel
    {
        private TKBBLL busTKB;
        private TKBCTBLL busTKBCT;
        private KhoaHocBLL busKH;
        public LopBLL busLop;
        public MonHocBLL busMH;
        public GiaoVienBLL busGV;
        public HocSinhBLL busHS;
        public AdminBLL busAd;
        private List<KhoaHocDTO> lstKH;
        public List<TKBDTO> lstTKB;
        public List<TKBCTDTO> lstTKBCT;
        [BindProperty]
        [DisplayName("Mã Khoá học")]
        public int maKh { get; set; }
        [BindProperty]
        [DisplayName("Mã thời khoá biểu")]
        public int maTkb { get; set; }
        public List<KhoaHocDTO> LstKH { get => lstKH; set => lstKH = value; }

        public TKBModel()
        {
            busLop = new LopBLL();
            busMH = new MonHocBLL();
            busKH = new KhoaHocBLL();
            busTKB = new TKBBLL();
            busTKBCT = new TKBCTBLL();
            busLop = new LopBLL();
            busHS = new HocSinhBLL();
            busGV = new GiaoVienBLL();
            busAd = new AdminBLL();
        }
        public void OnGet()
        {
            string user_id = HttpContext.Session.GetString("user_id");
            if(user_id.Substring(0, 2) == "ad")
            {
                lstTKB = busTKB.GetAll().ToList();
            }    
            else if(user_id.Substring(0, 2) == "gv")
            {

                lstTKBCT = busTKBCT.getTbkByGv(user_id).ToList();
            }   
            else
            {
                int idLop = busHS.GetHSByID(user_id).MaLop;
                lstTKBCT = busTKBCT.getTkbByLop(idLop).ToList();
            }    
            LstKH = busKH.GetAll().ToList();
            lstTKB = busTKB.GetAll().ToList();
        }
        public void OnPost()
        {
            int flat = 0;
            LstKH = busKH.GetAll().ToList();
            List<TKBDTO> lst = new List<TKBDTO>();
            if (maKh != 0)
            {
                lstTKB = busTKB.GetAll().Where(x => x.MaKh == maKh).ToList();
                lst = lstTKB;
                flat = 1;
            }
            if (maTkb != 0)
            {
                if(flat == 1)
                {
                    lstTKB = lst.Where(x => x.MaTkb == maTkb && x.MaTkb == maTkb).ToList();
                }
                else
                {
                    lstTKB = busTKB.GetAll().Where(x => x.MaTkb == maTkb).ToList();
                }    
                flat = 1;
            }
            if(flat == 0)
            {
                OnGet();
            }    
        }
        public IActionResult OnPostUpdate(String tkb)
        {
            var obj = JsonSerializer.Deserialize<TKBDTO>(tkb);
            var res = busTKB.Update(obj);
            if (res)
                return new ObjectResult(new { success = true, tkb = obj }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostDelete(String maTKB)
        {
            var id = int.Parse(maTKB);
            var res = busTKB.Delete(id);
            if (res)
                return new ObjectResult(new { success = true }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostAdd(String tkb)
        {
            var obj = JsonSerializer.Deserialize<TKBDTO>(tkb);
            var res = busTKB.Add(obj);
            if (res != null)
                return new ObjectResult(new { success = true, tkb = res }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
    }
}
