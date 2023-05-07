using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL.Models;
using QLL.DTO;

namespace QLL.DAL
{
    public class TKBDAL
    {
        private QuanLyLopContext db;
        public TKBDAL()
        {
            db = new QuanLyLopContext();
        }
        public IList<TKBDTO> GetAll()
        {
            List<TKBDTO> res = new List<TKBDTO>();
            try
            {
                var ls = db.Tkbdbs.ToList();
                foreach (var m in ls)
                {
                    TKBDTO dto = new TKBDTO();
                    dto.MaTkb = m.MaTkb;
                    dto.MaKh = m.MaKh;
                    dto.TrangThai = m.TrangThai;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;

        }
        public bool Update(TKBDTO tkb)
        {
            bool res = false;
            var c = db.Tkbdbs.FirstOrDefault(x => x.MaTkb == tkb.MaTkb);
            if (c.MaTkb != tkb.MaTkb)
                c.MaKh = tkb.MaTkb;
            if (c.TrangThai != tkb.TrangThai)
                c.TrangThai = tkb.TrangThai;
            try
            {
                db.Tkbdbs.Update(c);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public bool Delete(int maTkb)
        {
            bool res = false;
            var tkb = db.Tkbdbs.FirstOrDefault(x => x.MaTkb == maTkb);
            try
            {
                TKBCTDAL tkbct = new TKBCTDAL();
                tkbct.DeleteByIdTKB(maTkb);
                db.Tkbdbs.Remove(tkb);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public TKBDTO Add(TKBDTO tkb)
        {
            TKBDTO res = new TKBDTO();
            var c = new Tkbdb();
            c.MaKh = tkb.MaKh;
            try
            {
                db.Tkbdbs.Add(c);
                db.SaveChanges();
                res.MaTkb = c.MaTkb;
                res.MaKh = c.MaKh;
                res.TrangThai = c.TrangThai;
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
    }
}
