using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using QLL.DAL.Models;
using QLL.DTO;

namespace QLL.DAL
{
    public class KhoaHocDAL
    {
        private QuanLyLopContext db;
        public KhoaHocDAL()
        {
            db = new QuanLyLopContext();
        }
        public IList<KhoaHocDTO> GetAll()
        {
            List<KhoaHocDTO> res = new List<KhoaHocDTO>();
            try
            {
                var ls = db.KhoaHocDbs.ToList();
                foreach (var l in ls)
                {
                    KhoaHocDTO dto = new KhoaHocDTO();
                    dto.MaKh = l.MaKh;
                    dto.TenKh = l.TenKh;
                    dto.NgayBatDau = l.NgayBatDau;
                    dto.NgayKetThuc = l.NgayKetThuc;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;

        }
        public KhoaHocDTO GetById(int maKh)
        {
            KhoaHocDTO res = new KhoaHocDTO();
            var l = db.KhoaHocDbs.FirstOrDefault(x=>x.MaKh == maKh);
            if(l != null)
            {
                res.MaKh = l.MaKh;
                res.TenKh = l.TenKh;
                res.NgayBatDau = l.NgayBatDau;
                res.NgayKetThuc = l.NgayKetThuc;
                
            }
            else
            {
                res = null;
            }
            return res;

        }
        public bool Update(KhoaHocDTO kh)
        {
            bool res = false;
            var c = db.KhoaHocDbs.FirstOrDefault(x => x.MaKh == kh.MaKh);
            if (c.TenKh != kh.TenKh)
                c.TenKh = kh.TenKh;
            if (c.NgayBatDau != kh.NgayBatDau)
                c.NgayBatDau = kh.NgayBatDau;
            if (c.NgayKetThuc != kh.NgayKetThuc)
                c.NgayKetThuc = kh.NgayKetThuc;
            try
            {
                db.KhoaHocDbs.Update(c);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public bool Delete(int maKh)
        {
            bool res = false;
            var kh = db.KhoaHocDbs.FirstOrDefault(x => x.MaKh == maKh);
            try
            {
                db.KhoaHocDbs.Remove(kh);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public KhoaHocDTO Add(KhoaHocDTO kh)
        {
            KhoaHocDTO res = new KhoaHocDTO();
            var c = new KhoaHocDb();
            c.TenKh = kh.TenKh;
            c.NgayBatDau = kh.NgayBatDau;
            c.NgayKetThuc = kh.NgayKetThuc;
            try
            {
                db.KhoaHocDbs.Add(c);
                db.SaveChanges();
                res.MaKh = c.MaKh;
                res.TenKh = c.TenKh;
                res.NgayBatDau = c.NgayBatDau;
                res.NgayKetThuc = c.NgayKetThuc;
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public IList<MonHocDTO> GetAllMH(int maLop)
        {
            List<MonHocDTO> res = new List<MonHocDTO>();
            try
            {
                var ls = db.KhoaHocDbs.Join(db.Tkbdbs,
                                            kh => kh.MaKh,
                                            tkb => tkb.MaKh,
                                            (kh, tkb) => new { tkb.MaTkb }).Join(db.Tkbctdbs,
                                            tkb => tkb.MaTkb,
                                            tkbct => tkbct.MaTkb,
                                            (tkb, tkbct) => new
                                            {
                                                tkbct.Malop,
                                                tkbct.MaMh,
                                            }).Where(p=>p.Malop == maLop).ToList();
                foreach(var i in ls.Distinct())
                {
                    
                    MonHocDAL dal = new MonHocDAL();
                    res.Add(dal.GetById(i.MaMh));
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
