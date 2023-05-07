using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QLL.BLL;
using QLL.DTO;

namespace QuanLyLop.Pages
{
    public class PhoDiemModel : PageModel
    {
        public AdminBLL busAd;
        public DiemBLL busD;
        private KhoaHocBLL busK;
        private LopBLL busL;
        private MonHocBLL busM;
        public List<PhoDiem> lstd;
        public string msg_err;
        public string title;
        [BindProperty]
        [DisplayName("Mã môn học")]
        public string maMh { get; set; }
        [BindProperty]
        [DisplayName("Mã khoá học")]
        public string maKh { get; set; }
        [BindProperty]
        [DisplayName("Mã Lớp")]
        public string maLop { get; set; }

        public PhoDiemModel()
        {
            busAd = new AdminBLL();
            busD = new DiemBLL();
        }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            int idKh = 0, idLop = 0, idMon = 0;
            title = "Phổ điểm";
            if(maKh != null)
            {
                idKh = int.Parse(maKh);
                lstd = busD.GetAllGroupBy(idKh, idLop,idMon).ToList();
            }
            if (maLop != null)
            {
                idLop = int.Parse(maLop);
                lstd = busD.GetAllGroupBy(idKh, idLop, idMon).ToList();
            }
            if (maMh != null)
            {
                idMon = int.Parse(maMh);
                lstd = busD.GetAllGroupBy(idKh, idLop, idMon).ToList();
            }
            if(idMon == 0 && idLop == 0 && idKh == 0 || lstd.Count == 0)
            {
                msg_err = "Không tìm thấy dữ liệu!!! vui lòng kiểm tra lại";

            } 
        }
    }
}
