using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL.Models;
using QLL.DTO;

namespace QLL.DAL
{
    public class TKBCTDAL
    {
        private QuanLyLopContext db;
        public TKBCTDAL()
        {
            db = new QuanLyLopContext();
        }
        public IList<TKBCTDTO> GetAll()
        {
            List<TKBCTDTO> res = new List<TKBCTDTO>();
            try
            {
                var ls = db.Tkbctdbs.ToList();
                foreach (var tkb in ls)
                {
                    TKBCTDTO dto = new TKBCTDTO();
                    dto.MaTkb = tkb.MaTkb;
                    dto.Thu = tkb.Thu;
                    dto.Tiet = tkb.Tiet;
                    dto.Malop = tkb.Malop;
                    dto.MaGv = tkb.MaGv;
                    dto.MaMh = tkb.MaMh;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;

        }

        public IList<TKBCTDTO> GetById(int maTKB)
        {
            List<TKBCTDTO> res = new List<TKBCTDTO>();
            try
            {
                var ls = db.Tkbctdbs.Where(x=>x.MaTkb == maTKB).ToList();
                foreach (var tkb in ls)
                {
                    TKBCTDTO dto = new TKBCTDTO();
                    dto.MaTkb = tkb.MaTkb;
                    dto.Thu = tkb.Thu;
                    dto.Tiet = tkb.Tiet;
                    dto.Malop = tkb.Malop;
                    dto.MaGv = tkb.MaGv;
                    dto.MaMh = tkb.MaMh;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;

        }
        public bool Update(TKBCTDTO tkb)
        {
            bool res = false;
            var c = db.Tkbctdbs.FirstOrDefault(x => x.Malop == tkb.Malop && x.Thu == tkb.Thu && x.Tiet == tkb.Tiet);
            if (c.MaTkb != tkb.MaTkb)
                c.MaTkb = tkb.MaTkb;
            if (c.MaMh != tkb.MaMh)
                c.MaMh = tkb.MaMh;
            if (c.MaGv != tkb.MaGv)
                c.MaGv = tkb.MaGv;
            try
            {
                db.Tkbctdbs.Update(c);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public bool Delete(int maLop, int thu, int tiet)
        {
            bool res = false;
            var tkb = db.Tkbctdbs.FirstOrDefault(x => x.Malop == maLop && x.Thu == thu && x.Tiet == tiet);
            try
            {
                db.Tkbctdbs.Remove(tkb);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public bool DeleteByIdTKB(int maTKB)
        {
            bool res = false;
            try
            {
                var ls = db.Tkbctdbs.Where(x => x.MaTkb == maTKB).ToList();
                foreach(var tkb in ls)
                {
                    db.Tkbctdbs.Remove(tkb);
                }
                db.SaveChanges();
                res = true;
            }
            catch(Exception ex1)
            {
                res = false;
            }
            return res;
        }
        public TKBCTDTO Add(TKBCTDTO tkb)
        {
            TKBCTDTO res = new TKBCTDTO();
            var c = new Tkbctdb();
            c.MaTkb = tkb.MaTkb;
            c.Thu = tkb.Thu;
            c.Tiet = tkb.Tiet;
            c.MaMh = tkb.MaMh;
            c.Malop = tkb.Malop;
            c.MaGv = tkb.MaGv;
            try
            {
                db.Tkbctdbs.Add(c);
                db.SaveChanges();
                res.MaTkb = c.MaTkb;
                res.Thu = c.Thu;
                res.Tiet = c.Tiet;
                res.MaMh = c.MaMh;
                res.Malop = c.Malop;
                res.MaGv = c.MaGv;
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public IList<List_Mh> GetMonHocByIdGv(string maGv, int maLop)
        {
            List<List_Mh> res = new List<List_Mh>();
            try
            {
                var ls = db.Tkbctdbs.Where(
                        tkb =>tkb.MaGv == maGv && tkb.Malop == maLop
                    ).Select(tkb =>new {
                        tkb.MaGv, tkb.Malop, tkb.MaMh
                    }).ToList();
                foreach (var tkb in ls.Distinct())
                {
                    List_Mh dto = new List_Mh();
                    MonHocDAL dal = new MonHocDAL();
                    dto.maGV = tkb.MaGv;
                    dto.maMH = tkb.MaMh;
                    dto.maLop = tkb.Malop;
                    dto.tenMH = dal.GetMHById(tkb.MaMh).TenMh;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public object GetTKBByPage(int page, int size)
        {

            List<TKBCTDTO> data = new List<TKBCTDTO>();
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
                var ls = db.Tkbctdbs.ToList();
                var offset = (page - 1) * size;
                var totalRecord = ls.Count();
                var tottalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)(totalRecord / size + 1);
                var lst = ls.Skip(offset).Take(size);
                foreach (var hs in lst)
                {
                    TKBCTDTO dto = new TKBCTDTO();
                    dto.MaTkb = hs.MaTkb;
                    dto.Malop = hs.Malop;
                    dto.MaMh = hs.MaMh;
                    dto.MaGv = hs.MaGv;
                    dto.Thu = hs.Thu;
                    dto.Tiet = hs.Tiet;
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

        public IList<TKBCTDTO> getTkbByLop(int maLop)
        {
            List<TKBCTDTO> result = new List<TKBCTDTO>();
            try
            {
                var ls1 = db.Tkbdbs.Where(x => x.TrangThai == true).ToList();
                if (ls1 != null)
                {
                    foreach (var tkb in ls1)
                    {
                        var ls = db.Tkbctdbs.Where(x => x.Malop == maLop && x.MaTkb == tkb.MaTkb).OrderBy(x => x.Thu).ThenBy(x => x.Tiet);
                        if(ls != null)
                        {    
                            foreach (var c in ls)
                            {
                                TKBCTDTO res = new TKBCTDTO();
                                res.MaTkb = c.MaTkb;
                                res.Thu = c.Thu;
                                res.Tiet = c.Tiet;
                                res.MaMh = c.MaMh;
                                res.Malop = c.Malop;
                                res.MaGv = c.MaGv;
                                result.Add(res);
                            }

                        } 
                    }

                }
            }
            catch (Exception ex1)
            {
                result = null;
            }
            return result;
        }
        public IList<TKBCTDTO> getTbkByGv(string maGv)
        {
            List< TKBCTDTO> res = new List<TKBCTDTO>();
            var ma = db.Tkbctdbs.Where(x => x.MaGv == maGv).Join(db.Tkbdbs,
                                                                x => x.MaTkb,
                                                                y => y.MaTkb,
                                                                (x, y) => new { y.MaTkb, y.TrangThai }).FirstOrDefault(y => y.TrangThai == true);
            var ls = db.Tkbctdbs.Where(x => x.MaGv == maGv && x.MaTkb == ma.MaTkb).OrderBy(x => x.Thu).ThenBy(x => x.Tiet);
            try
            {
                foreach (var c in ls)
                {
                    TKBCTDTO dto = new TKBCTDTO();
                    dto.MaTkb = c.MaTkb;
                    dto.Thu = c.Thu;
                    dto.Tiet = c.Tiet;
                    dto.MaMh = c.MaMh;
                    dto.Malop = c.Malop;
                    dto.MaGv = c.MaGv;
                    res.Add(dto);
                }
            }
            catch (Exception ex1)
            {
                res = null;
            }
            return res;
        }
    }
}
