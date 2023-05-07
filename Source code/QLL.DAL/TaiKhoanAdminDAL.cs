using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL.Models;
using QLL.DTO;

namespace QLL.DAL
{
    public class TaiKhoanAdminDAL
    {
        private QuanLyLopContext db;
        public TaiKhoanAdminDAL()
        {
            db = new QuanLyLopContext();

        }
        public IList<TaiKhoanAdminDTO> GetAll()
        {
            List<TaiKhoanAdminDTO> res = new List<TaiKhoanAdminDTO>();
            try
            {
                var ls = db.TaiKhoanAdDbs.ToList();
                foreach (var tk in ls)
                {
                    TaiKhoanAdminDTO dto = new TaiKhoanAdminDTO();
                    dto.MaTk = tk.MaTk;
                    dto.TenDangNhap = tk.TenDangNhap;
                    dto.MatKhau = tk.MatKhau;
                    dto.MaAdmin = tk.MaAdmin;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;

        }
        public bool Update(TaiKhoanAdminDTO tk)
        {
            bool res = false;
            var c = db.TaiKhoanAdDbs.FirstOrDefault(x => x.MaTk == tk.MaTk);
            if (c.MatKhau != tk.MatKhau)
                c.MatKhau = tk.MatKhau;
            try
            {
                db.TaiKhoanAdDbs.Update(c);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public bool Delete(int maTk)
        {
            bool res = false;
            var tk = db.TaiKhoanAdDbs.FirstOrDefault(x => x.MaTk == maTk);
            try
            {
                db.TaiKhoanAdDbs.Remove(tk);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public TaiKhoanAdminDTO Add(TaiKhoanAdminDTO tk)
        {
            TaiKhoanAdminDTO res = new TaiKhoanAdminDTO();
            var c = new TaiKhoanAdDb();
            c.TenDangNhap = tk.TenDangNhap;
            c.MatKhau = tk.MatKhau;
            c.MaAdmin = tk.MaAdmin;
            try
            {
                db.TaiKhoanAdDbs.Add(c);
                db.SaveChanges();
                res.MaTk = c.MaTk;
                res.TenDangNhap = c.TenDangNhap;
                res.MatKhau = c.MatKhau;
                res.MaAdmin = c.MaAdmin;
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }

        public string login(string username, string password)
        {
            string userId = "";
            var tk = db.TaiKhoanAdDbs.FirstOrDefault(x => x.TenDangNhap == username && x.MatKhau == password);
            if (tk != null)
            {
                userId = tk.MaAdmin;
                return userId;
            }

            return userId;
        }

    }
}