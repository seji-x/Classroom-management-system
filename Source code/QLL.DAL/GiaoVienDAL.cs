using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DTO;
using QLL.DAL.Models;

namespace QLL.DAL
{
    public class GiaoVienDAL
    {
        private QuanLyLopContext db;
        public GiaoVienDAL()
        {
            db = new QuanLyLopContext();
        }
        public List<string> GetID()
        {
            List<string> ma;
            var ls = (from ad in db.GiaoVienDbs select ad.MaGv).
                Except(from tk in db.TaiKhoanGvdbs select tk.MaGv);
            ma = ls.ToList();
            return ma;

        }
        public object GetGVByPage(int page, int size)
        {
            List<GiaoVienDTO> data = new List<GiaoVienDTO>();
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
                var ls = db.GiaoVienDbs.ToList();
                var offset = (page - 1) * size;
                var totalRecord = ls.Count();
                var tottalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)(totalRecord / size + 1);
                var lst = ls.Skip(offset).Take(size);
                foreach (var gv in lst)
                {

                    GiaoVienDTO dto = new GiaoVienDTO();
                    dto.MaGv= gv.MaGv;
                    dto.TenGv = gv.TenGv;
                    dto.GioiTinh = gv.GioiTinh;
                    dto.NgaySinh = gv.NgaySinh;
                    dto.DiaChi = gv.DiaChi;
                    dto.Sdt = gv.Sdt;
                    dto.Email = gv.Email;
                    dto.ChuyenNganh = gv.ChuyenNganh;
                    dto.TrinhDoChuyenMon = gv.TrinhDoChuyenMon;
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
        public IList<GiaoVienDTO> GetAll()
        {
            List<GiaoVienDTO> res = new List<GiaoVienDTO>();
            try
            {
                var ls = db.GiaoVienDbs.ToList();
                foreach (var gv in ls)
                {
                    GiaoVienDTO dto = new GiaoVienDTO();
                    dto.MaGv = gv.MaGv;
                    dto.TenGv = gv.TenGv;
                    dto.GioiTinh = gv.GioiTinh;
                    dto.NgaySinh = gv.NgaySinh;
                    dto.DiaChi = gv.DiaChi;
                    dto.Sdt = gv.Sdt;
                    dto.Email = gv.Email;
                    dto.ChuyenNganh = gv.ChuyenNganh;
                    dto.TrinhDoChuyenMon = gv.TrinhDoChuyenMon;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public GiaoVienDTO GetByID(string maGv)
        {
            GiaoVienDTO res = new GiaoVienDTO();
            var gv = db.GiaoVienDbs.FirstOrDefault(gv => gv.MaGv == maGv);
            if(gv != null)
            {
                res.MaGv = gv.MaGv;
                res.TenGv = gv.TenGv;
                res.NgaySinh = gv.NgaySinh;
                res.GioiTinh = gv.GioiTinh;
                res.ChuyenNganh = gv.ChuyenNganh;
                res.Sdt = gv.Sdt;
                res.DiaChi = gv.DiaChi;
                res.Email = gv.Email;
                res.TrinhDoChuyenMon = gv.TrinhDoChuyenMon;
            }
            else
            {
                res = null;
            }
            return res;
        }
        public bool Update(GiaoVienDTO gv)
        {
            bool res = false;
            var c = db.GiaoVienDbs.FirstOrDefault(x => x.MaGv == gv.MaGv);
            if (c.TenGv != gv.TenGv)
                c.TenGv = gv.TenGv;
            if (c.GioiTinh != gv.GioiTinh)
                c.GioiTinh = gv.GioiTinh;
            if (c.NgaySinh != gv.NgaySinh)
                c.NgaySinh = gv.NgaySinh;
            if (c.DiaChi != gv.DiaChi)
                c.DiaChi = gv.DiaChi;
            if (c.Sdt != gv.Sdt)
                c.Sdt = gv.Sdt;
            if (c.Email != gv.Email)
                c.Email = gv.Email;
            if (c.ChuyenNganh != gv.ChuyenNganh)
                c.ChuyenNganh = gv.ChuyenNganh;
            if (c.TrinhDoChuyenMon != gv.TrinhDoChuyenMon)
                c.TrinhDoChuyenMon = gv.TrinhDoChuyenMon;
            try
            {
                db.GiaoVienDbs.Update(c);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public bool Delete(string maGV)
        {
            bool res = false;
            var hs = db.GiaoVienDbs.FirstOrDefault(x => x.MaGv == maGV);
            try
            {
                TaiKhoanGVDAL gvDal = new TaiKhoanGVDAL();
                if (db.TaiKhoanGvdbs.FirstOrDefault(x => x.MaGv == maGV) != null)
                {
                    gvDal.Delete(db.TaiKhoanGvdbs.FirstOrDefault(x => x.MaGv == maGV).MaTk);
                }
                
                db.GiaoVienDbs.Remove(hs);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public GiaoVienDTO Add(GiaoVienDTO gv)
        {
            var t = db.GiaoVienDbs.FirstOrDefault(x=>x.MaGv==gv.MaGv);
            if (db.GiaoVienDbs.FirstOrDefault(x => x.MaGv == gv.MaGv) == null)
            {
                GiaoVienDTO res = new GiaoVienDTO();
                var c = new GiaoVienDb();
                c.MaGv = gv.MaGv;
                c.TenGv = gv.TenGv;
                c.GioiTinh = gv.GioiTinh;
                c.NgaySinh = gv.NgaySinh;
                c.Sdt = gv.Sdt;
                c.DiaChi = gv.DiaChi;
                c.Email = gv.Email;
                c.ChuyenNganh = gv.ChuyenNganh;
                c.TrinhDoChuyenMon = gv.TrinhDoChuyenMon;
                try
                {
                    db.GiaoVienDbs.Add(c);
                    db.SaveChanges();
                    res.MaGv = c.MaGv;
                    res.TenGv = c.TenGv;
                    res.GioiTinh = c.GioiTinh;
                    res.NgaySinh = c.NgaySinh;
                    res.Sdt = c.Sdt;
                    res.DiaChi = c.DiaChi;
                    res.Email = c.Email;
                    res.ChuyenNganh = c.ChuyenNganh;
                    res.TrinhDoChuyenMon = c.TrinhDoChuyenMon;
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
