using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using QLL.BLL;
using QLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace QuanLyLop2_ASP.NETCore.Pages
{
    public class GiaoVienModel : PageModel
    {
        public GiaoVienBLL busGV;
        public HocSinhBLL busHS;
        public AdminBLL busAd;
        public List<GiaoVienDTO> lstGV;
        public List<GiaoVienDTO> lstGV1;
        public int TotalPage;

        [BindProperty]
        [DisplayName("Mã Giáo viên")]
        public string maGV { get; set; }

        [BindProperty]
        [DisplayName("Họ tên")]
        public string tenGV { get; set; }
        [BindProperty]
        [DisplayName("Trình độ chuyên môn")]
        public string tdcm { get; set; }
        [BindProperty]
        [DisplayName("Chuyên ngành")]
        public string cn { get; set; }
        public GiaoVienModel()
        {
            busHS = new HocSinhBLL();
            busGV = new GiaoVienBLL();
            busAd = new AdminBLL();
            busGV = new GiaoVienBLL();
        }
        public void OnGet()
        {
            int size = 10;
            lstGV1 = busGV.GetAll().Take(size).ToList();
            lstGV = busGV.GetAll().ToList();
            var totalRecord = busGV.GetAll().Count();
            TotalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)(totalRecord / size + 1);
        }
        public void OnPost()
        {
            lstGV = busGV.GetAll().ToList();
            TotalPage = 1;
            var temp = new List<GiaoVienDTO>();
            int flat = 0;
            if(maGV != null && maGV != "")
            {
                temp = lstGV.Where(x => x.MaGv == maGV).ToList();
                lstGV1 = temp;
                flat = 1;
            }
            if (tenGV != null && tenGV != "")
            {
                if(flat == 1)
                {
                    temp = lstGV1.Where(x => x.TenGv.ToLower().Contains(tenGV.ToLower())).ToList();
                }
                else
                {
                    temp = lstGV.Where(x => x.TenGv.ToLower().Contains(tenGV.ToLower())).ToList();
                }
                lstGV1 = temp;
                flat = 1;
            }
            if (tdcm != null && tdcm != "")
            {
                if (flat == 1)
                {
                    temp = lstGV1.Where(x => x.TrinhDoChuyenMon.ToLower() == tdcm.ToLower()).ToList();
                }
                else
                {
                    temp = lstGV.Where(x => x.TrinhDoChuyenMon.ToLower() == tdcm.ToLower()).ToList();
                }

                    lstGV1 = temp;
                    flat = 1;
                }
                if (cn != null && cn != "")
                {
                    if (flat == 1)
                    {
                        temp = lstGV1.Where(x => x.ChuyenNganh.ToLower() == cn.ToLower()).ToList();
                    }
                    else
                    {
                        temp = lstGV.Where(x => x.ChuyenNganh.ToLower() == cn.ToLower()).ToList();

                    }
                    lstGV1 = temp;
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
            var Data = busGV.GetGVByPage(obj.Page, obj.Size);
            return new ObjectResult(new { success = true, data = Data }) { StatusCode = 200 };
        }
        public IActionResult OnPostUpdate(String gv)
        {
            var obj = JsonSerializer.Deserialize<GiaoVienDTO>(gv);
            var res = busGV.Update(obj);
            if (res)
                return new ObjectResult(new { success = true, gv = obj }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostDelete(String maGV)
        {
            var id = maGV;
            var res = busGV.Delete(id);
            if (res)
                return new ObjectResult(new { success = true }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false }) { StatusCode = 500 };
        }
        public IActionResult OnPostAdd(String gv)
        {
            var obj = JsonSerializer.Deserialize<GiaoVienDTO>(gv);
            var res = busGV.Add(obj);
            if (res != null)
                return new ObjectResult(new { success = true, gv = res }) { StatusCode = 200 };
            else
                return new ObjectResult(new { success = false, }) { StatusCode = 500 };
        }
    }
}
