using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QLL.BLL;
using QLL.DTO;

namespace QuanLyLop.Pages
{
    public class InFo_LopModel : PageModel
    {
        private LopBLL busLop;
        public HocSinhBLL busHS;
        public GiaoVienBLL busGV;
        public AdminBLL busAd;
        private TKBCTBLL busTKB;
        public List<HocSinhDTO> lstHS;
        public LopDTO lop;
        public List<List_Mh> lstMH;

        [BindProperty(SupportsGet = true)]
        public string maLop { get; set; }

        [BindProperty]
        [DisplayName("Mã học sinh")]
        public string maHS { get; set; }

        [BindProperty]
        [DisplayName("Họ tên")]
        public string tenHS { get; set; }
        public InFo_LopModel()
        {
            busGV = new GiaoVienBLL();
            busAd = new AdminBLL();
            busHS = new HocSinhBLL();
            busTKB = new TKBCTBLL();
            busLop = new LopBLL();
            busHS = new HocSinhBLL();
        }
        public void OnGet()
        {
            if(maLop != null)
            {
                int id = int.Parse(maLop);
                lop = busLop.GetLopById(id);
                lstHS = busHS.GetHSByIDLop(id).ToList();
                lstMH = busTKB.GetMonHocByIdGv(HttpContext.Session.GetString("user_id"), id).ToList();
            }           
        }
        
        public void OnPost()
        {
            int id = int.Parse(maLop);
            lop = busLop.GetLopById(id);
            lstHS = busHS.GetHSByIDLop(id).ToList();
            lstMH = busTKB.GetMonHocByIdGv(HttpContext.Session.GetString("user_id"), id).ToList();
            lstHS = busHS.GetHSByIDLop(id).ToList();
            int flat = 0;
            var temp1 = new List<HocSinhDTO>();
            if (tenHS != null && tenHS != "")
            {
                temp1 = (from hs in lstHS
                         where hs.TenHs.ToLower().Contains(tenHS.ToLower())
                         select hs).ToList();
                lstHS = temp1;
            }
            if (maHS != null && maHS != "")
            {
                temp1 = (from hs in lstHS
                         where hs.MaHs == maHS
                         select hs).ToList();
                lstHS = temp1;
            }
            if(flat == 1)
            {
                OnGet();
            }
        }
    }
}
