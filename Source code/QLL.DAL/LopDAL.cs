using QLL.DAL.Models;
using QLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLL.DAL
{
    public class LopDAL
    {
        private QuanLyLopContext db;
        public LopDAL()
        {
            db = new QuanLyLopContext();
        }
        public IList<LopDTO> GetAll()
        {
            List<LopDTO> res = new List<LopDTO>();
            try
            {
                var ls = db.LopDbs.ToList();
                foreach (var l in ls)
                {
                    LopDTO dto = new LopDTO();
                    dto.MaLop = l.MaLop;
                    dto.TenLop = l.TenLop;
                    dto.PhongHoc = l.PhongHoc;
                    dto.MoTa = l.MoTa;
                    dto.TrangThai = l.TrangThai;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;

        }
        public LopDTO GetLopById(int id)
        {
            LopDTO res = new LopDTO();
            try
            {
                var ls = db.LopDbs.ToList();
                foreach (var l in ls)
                {
                    if(id == l.MaLop)
                    {
                        res.MaLop = l.MaLop;
                        res.TenLop = l.TenLop;
                        res.PhongHoc = l.PhongHoc;
                        res.MoTa = l.MoTa;
                        res.TrangThai = l.TrangThai;
                        return res;
                    }         
                }
                res = null;
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public IList<LopDTO> GetLopByIdGV(string maGv)
        {
            IList<LopDTO> res = new List<LopDTO>();
            try
            {
                var ls = db.Tkbctdbs.Where(tkb => tkb.MaGv == maGv).Join(
                    db.GiaoVienDbs,
                    tkb => tkb.MaGv,
                    gv => gv.MaGv,
                    (tkb, gv) => new
                    {   
                        tkb.Malop
                    }
                    ).ToList();
                foreach (var l in ls.Distinct())
                {             
                        LopDTO dto = new LopDTO();
                        dto = GetLopById(l.Malop) ;
                        res.Add(dto);               
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public bool Update(LopDTO lop)
        {
            bool res = false;
            var c = db.LopDbs.FirstOrDefault(x => x.MaLop == lop.MaLop);
            if (c.TenLop != lop.TenLop)
                c.TenLop = lop.TenLop;
            if (c.PhongHoc != lop.PhongHoc)
                c.PhongHoc = lop.PhongHoc;
            if (c.MoTa != lop.MoTa)
                c.MoTa = lop.MoTa;
            if (c.TrangThai != lop.TrangThai)
                c.TrangThai = lop.TrangThai;
            try
            {
                db.LopDbs.Update(c);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
               res = false;
            }
            return res;
        }
        public bool Delete(int maLop)
        {
            bool res = false;
            var lop = db.LopDbs.FirstOrDefault(x=>x.MaLop==maLop);
            try
            {
                db.LopDbs.Remove(lop);
                db.SaveChanges();
                res = true;
            }
            catch(Exception ex)
            {
                res = false;
            }
            return res;
        }
        public LopDTO Add(LopDTO lop)
        {
            LopDTO res = new LopDTO();
            var c = new LopDb();
            c.TenLop = lop.TenLop;
            c.PhongHoc = lop.PhongHoc;
            c.MoTa = lop.MoTa;
            c.TrangThai = lop.TrangThai;
            try
            {
                db.LopDbs.Add(c);
                db.SaveChanges();
                res.MaLop = c.MaLop;
                res.TenLop = c.TenLop;
                res.PhongHoc = c.PhongHoc;
                res.MoTa = c.MoTa;  
                res.TrangThai = c.TrangThai;
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public IList<KhoaHocDTO> GetAllKH(int maLop)
        {
            List<KhoaHocDTO> res = new List<KhoaHocDTO>();
            try
            {
                var ls = db.LopDbs.Join(db.Hocs,
                                        l => l.MaLop,
                                        h => h.MaLop,
                                        (l, h) => new { h.MaKh }).Join(db.KhoaHocDbs,
                                        h => h.MaKh,
                                        kh => kh.MaKh,
                                        (h, kh) => new { kh.MaKh, kh.NgayBatDau, 
                                            kh.NgayKetThuc, kh.TenKh,}).ToList();
                foreach(var i in ls.Distinct())
                {
                    KhoaHocDTO dto = new KhoaHocDTO();
                    dto.MaKh = i.MaKh;
                    dto.TenKh = i.TenKh;
                    dto.NgayKetThuc = i.NgayKetThuc;
                    dto.NgayBatDau = i.NgayBatDau;
                    res.Add(dto);
                }    
            }
            catch(Exception ex1)
            {
                res = null;
            }
            return res;
        }
        
    }
}
