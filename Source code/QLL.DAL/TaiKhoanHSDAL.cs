using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL.Models;
using QLL.DTO;

namespace QLL.DAL
{
    public class TaiKhoanHSDAL
    {
        private QuanLyLopContext db;
        public TaiKhoanHSDAL()
        {
            db = new QuanLyLopContext();

        }
        public IList<TaiKhoanHSDTO> GetAll()
        {
            List<TaiKhoanHSDTO> res = new List<TaiKhoanHSDTO>();
            try
            {
                var ls = db.TaiKhoanHsdbs.ToList();
                foreach (var tk in ls)
                {
                    TaiKhoanHSDTO dto = new TaiKhoanHSDTO();
                    dto.MaTk = tk.MaTk;
                    dto.TenDangNhap = tk.TenDangNhap;
                    dto.MatKhau = tk.MatKhau;
                    dto.MaHs = tk.MaHs;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;

        }
        public bool Update(TaiKhoanHSDTO tk)
        {
            bool res = false;
            var c = db.TaiKhoanHsdbs.FirstOrDefault(x => x.MaTk == tk.MaTk);
            if (c.MatKhau != tk.MatKhau)
                c.MatKhau = tk.MatKhau;
            try
            {
                db.TaiKhoanHsdbs.Update(c);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public bool DeleteById(string  maHs)
        {
            bool res = false;
            var tk = db.TaiKhoanHsdbs.FirstOrDefault(x => x.MaHs == maHs);
            try
            {
                if (tk != null)
                {
                    db.TaiKhoanHsdbs.Remove(tk);
                    db.SaveChanges();
                    res = true;
                }
            }
            catch (Exception ex1)
            {
                res = false;
            } 
            return res;
        }
        public bool Delete(int maTk)
        {
            bool res = false;
            var tk = db.TaiKhoanHsdbs.FirstOrDefault(x => x.MaTk == maTk);
            try
            {
                db.TaiKhoanHsdbs.Remove(tk);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public TaiKhoanHSDTO Add(TaiKhoanHSDTO tk)
        {
            TaiKhoanHSDTO res = new TaiKhoanHSDTO();
            var c = new TaiKhoanHsdb();
            c.TenDangNhap = tk.TenDangNhap;
            c.MatKhau = tk.MatKhau;
            c.MaHs = tk.MaHs;
            try
            {
                db.TaiKhoanHsdbs.Add(c);
                db.SaveChanges();
                res.MaTk = c.MaTk;
                res.TenDangNhap = c.TenDangNhap;
                res.MatKhau = c.MatKhau;
                res.MaHs = c.MaHs;
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
            var tk = db.TaiKhoanHsdbs.FirstOrDefault(x => x.TenDangNhap == username && x.MatKhau == password);
            if (tk != null)
            {
                userId = tk.MaHs;
                return userId;
            }

            return userId;
        }
    }
}