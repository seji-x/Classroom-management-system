using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL.Models;
using QLL.DTO;

namespace QLL.DAL
{
    public class AdminDAL
    {
        private QuanLyLopContext db;
        public AdminDAL()
        {
            db = new QuanLyLopContext();
        }
        public object GetAdByPage(int page, int size)
        {

            List<AdminDTO> data = new List<AdminDTO>();
            var res = new
            {
                Data = data,
                TotalRecord = 0,
                TotalPage = 0,
                Page = page,
                Size = size
            };
            try
            {
                var ls = db.AdminDbs.ToList();
                var offset = (page - 1) * size;
                var totalRecord = ls.Count();
                var tottalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)(totalRecord / size + 1);
                var lst = ls.Skip(offset).Take(size);
                foreach (var hs in lst)
                {
                    AdminDTO dto = new AdminDTO();
                    dto.MaAdmin = hs.MaAdmin;
                    dto.TenAdmin = hs.TenAdmin;
                    dto.GioiTinh = hs.GioiTinh;
                    dto.NgaySinh = hs.NgaySinh;
                    dto.DiaChi = hs.DiaChi;
                    dto.Sdt = hs.Sdt;
                    dto.Email = hs.Email;
                    data.Add(dto);
                }
                res = new
                {
                    Data = data,
                    TotalRecord = totalRecord,
                    TotalPage = tottalPage,
                    Page = page,
                    Size = size
                };
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public IList<AdminDTO> GetAll()
        {

            List<AdminDTO> res = new List<AdminDTO>();
            try
            {
                var ls = db.AdminDbs.ToList();
                foreach (var ad in ls)
                {
                    AdminDTO dto = new AdminDTO();
                    dto.MaAdmin = ad.MaAdmin;
                    dto.TenAdmin = ad.TenAdmin;
                    dto.GioiTinh = ad.GioiTinh;
                    dto.NgaySinh = ad.NgaySinh;
                    dto.DiaChi = ad.DiaChi;
                    dto.Sdt = ad.Sdt;
                    dto.Email = ad.Email;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public List<string> GetID()
        {
            List<string> ma;
            var ls = (from ad in db.AdminDbs select ad.MaAdmin).
                Except(from tk in db.TaiKhoanAdDbs select tk.MaAdmin);
            ma = ls.ToList();
            return ma;
        }
        public bool Update(AdminDTO ad)
        {
            bool res = false;
            var c = db.AdminDbs.FirstOrDefault(x => x.MaAdmin == ad.MaAdmin);
            if (c.TenAdmin != ad.TenAdmin)
                c.TenAdmin = ad.TenAdmin;
            if (c.GioiTinh != ad.GioiTinh)
                c.GioiTinh = ad.GioiTinh;
            if (c.NgaySinh != ad.NgaySinh)
                c.NgaySinh = ad.NgaySinh;
            if (c.DiaChi != ad.DiaChi)
                c.DiaChi = ad.DiaChi;
            if (c.Sdt != ad.Sdt)
                c.Sdt = ad.Sdt;
            if (c.Email != ad.Email)
                c.Email = ad.Email;
            try
            {
                db.AdminDbs.Update(c);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public AdminDTO GetById(string maAd)
        {
            AdminDTO res = new AdminDTO();
            var ad = db.AdminDbs.FirstOrDefault(ad => ad.MaAdmin == maAd);
            if(ad != null)
            {
                res.MaAdmin = ad.MaAdmin;
                res.TenAdmin = ad.TenAdmin;
                res.NgaySinh = ad.NgaySinh;
                res.GioiTinh = ad.GioiTinh;
                res.Sdt = ad.Sdt;
                res.DiaChi = ad.DiaChi;
                res.Email = ad.Email;
            }
            return res;
        }
        public bool Delete(string maAd)
        {
            bool res = false;
            var ad = db.AdminDbs.FirstOrDefault(x => x.MaAdmin == maAd);
            try
            {
                TaiKhoanAdminDAL tkdal = new TaiKhoanAdminDAL();
                if(db.TaiKhoanAdDbs.FirstOrDefault(x=>x.MaAdmin == maAd) != null)
                {
                    tkdal.Delete(db.TaiKhoanAdDbs.FirstOrDefault(x => x.MaAdmin == maAd).MaTk);
                }
                db.AdminDbs.Remove(ad);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public AdminDTO Add(AdminDTO ad)
        {
            if (db.AdminDbs.FirstOrDefault(x => x.MaAdmin == ad.MaAdmin) == null)
            {
                AdminDTO res = new AdminDTO();
                var c = new AdminDb();
                c.MaAdmin = ad.MaAdmin;
                c.TenAdmin = ad.TenAdmin;
                c.GioiTinh = ad.GioiTinh;
                c.NgaySinh = ad.NgaySinh;
                c.Sdt = ad.Sdt;
                c.DiaChi = ad.DiaChi;
                c.Email = ad.Email;
                try
                {
                    db.AdminDbs.Add(c);
                    db.SaveChanges();
                    res.MaAdmin = c.MaAdmin;
                    res.TenAdmin = c.TenAdmin;
                    res.GioiTinh = c.GioiTinh;
                    res.NgaySinh = c.NgaySinh;
                    res.Sdt = c.Sdt;
                    res.DiaChi = c.DiaChi;
                    res.Email = c.Email;
                }
                catch (Exception ex)
                {
                    res = null;
                }
                return res;
            }
            return null;
        }
    }
}
