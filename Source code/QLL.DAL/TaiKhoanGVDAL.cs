using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL.Models;
using QLL.DTO;

namespace QLL.DAL
{
    public class TaiKhoanGVDAL
    {
        private QuanLyLopContext db;
        public TaiKhoanGVDAL()
        {
            db = new QuanLyLopContext();

        }
        public IList<TaiKhoanGVDTO> GetAll()
        {
            List<TaiKhoanGVDTO> res = new List<TaiKhoanGVDTO>();
            try
            {
                var ls = db.TaiKhoanGvdbs.ToList();
                foreach (var tk in ls)
                {
                    TaiKhoanGVDTO dto = new TaiKhoanGVDTO();
                    dto.MaTk = tk.MaTk;
                    dto.TenDangNhap = tk.TenDangNhap;
                    dto.MatKhau = tk.MatKhau;
                    dto.MaGv = tk.MaGv;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;

        }
        public bool Update(TaiKhoanGVDTO tk)
        {
            bool res = false;
            var c = db.TaiKhoanGvdbs.FirstOrDefault(x => x.MaTk == tk.MaTk);
            if (c.MatKhau != tk.MatKhau)
                c.MatKhau = tk.MatKhau;
            try
            {
                db.TaiKhoanGvdbs.Update(c);
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
            var tk = db.TaiKhoanGvdbs.FirstOrDefault(x => x.MaTk == maTk);
            try
            {
                db.TaiKhoanGvdbs.Remove(tk);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public TaiKhoanGVDTO Add(TaiKhoanGVDTO tk)
        {
            TaiKhoanGVDTO res = new TaiKhoanGVDTO();
            var c = new TaiKhoanGvdb();
            c.TenDangNhap = tk.TenDangNhap;
            c.MatKhau = tk.MatKhau;
            c.MaGv = tk.MaGv;
            try
            {
                db.TaiKhoanGvdbs.Add(c);
                db.SaveChanges();
                res.MaTk = c.MaTk;
                res.TenDangNhap = c.TenDangNhap;
                res.MatKhau = c.MatKhau;
                res.MaGv = c.MaGv;
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
            var tk = db.TaiKhoanGvdbs.FirstOrDefault(x => x.TenDangNhap == username && x.MatKhau == password);
            if (tk != null)
            {
                userId = tk.MaGv;
                return userId;
            }

            return userId;
        }
    }
}