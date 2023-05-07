    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QLL.BLL;
using QLL.DTO;

namespace QuanLyLop.Pages
{
    public class HomeModel : PageModel
    {
        private TaiKhoanAdminBLL busTKA;
        private TaiKhoanGVBLL busTKGV;
        private TaiKhoanHSBLL busTKHS;
        public GiaoVienBLL busGV;
        public HocSinhBLL busHS;
        public AdminBLL busAd;
        public LopBLL busLop;
        public KhoaHocBLL busKH;
        public MonHocBLL busMH;
        private TKBCTBLL busTKBCT;
        public List<LopDTO> lstLop;
        public List<LopDTO> lstLop_GV;
        public List<KhoaHocDTO> lstKH;
        public List<MonHocDTO> lstMH;
        public int totalL, totalHS, totalGV, totalTK, totalMH;
        public double totalDTB;   

        [BindProperty]
        public string text { get; set; }
        public HomeModel()
        {
            busLop = new LopBLL();
            busKH = new KhoaHocBLL();
            busTKA = new TaiKhoanAdminBLL();
            busTKGV = new TaiKhoanGVBLL();
            busTKHS = new TaiKhoanHSBLL();
            busAd = new AdminBLL();
            busLop = new LopBLL();
            busGV = new GiaoVienBLL();
            busHS = new HocSinhBLL();
            busTKBCT = new TKBCTBLL();
        }
        public void OnGet()
        {
            string user_id = HttpContext.Session.GetString("user_id");
            lstLop = busLop.GetAll().ToList();
            if(user_id.Substring(0,2) == "ad")
            {

                totalGV = busGV.GetAll().Count;
                totalHS = busHS.GetAll().Count;
                totalL = busLop.GetAll().Count;
                totalTK = busTKA.GetAll().Count + busTKGV.GetAll().Count + busTKHS.GetAll().Count;
            }    
            if (user_id.Substring(0,2) == "gv")
            {    
                lstLop_GV = busLop.GetLopByIdGV(user_id).ToList();
            }  
            if(user_id.Substring(0,2) == "hs")
            {
                lstKH = busLop.GetAllKH(busHS.GetHSByID(user_id).MaLop).ToList();
                lstMH = busHS.GetMHById(user_id).ToList();
                totalHS = busHS.GetHSByIDLop(busHS.GetHSByID(user_id).MaLop).Count;
                totalMH = busKH.GetAllMH(busHS.GetHSByID(user_id).MaLop).Count;
                totalDTB = Math.Round(busHS.DiemTB(user_id),2);
            }
        }
    }
}
