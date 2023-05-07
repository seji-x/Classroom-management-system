using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL.Models;
using QLL.DTO;

namespace QLL.DAL
{
    public class MonHocDAL
    {
        private QuanLyLopContext db;
        public MonHocDAL()
        {
            db = new QuanLyLopContext();
        }

        public object GetMHByPage(int page, int size)
        {

            List<MonHocDTO> data = new List<MonHocDTO>();
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
                var ls = db.MonHocDbs.ToList();
                var offset = (page - 1) * size;
                var totalRecord = ls.Count();
                var tottalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)(totalRecord / size + 1);
                var lst = ls.Skip(offset).Take(size);
                foreach (var hs in lst)
                {
                    MonHocDTO dto = new MonHocDTO();
                    dto.MaMh = hs.MaMh;
                    dto.TenMh = hs.TenMh;
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
        public IList<MonHocDTO> GetAll()
        {
            List<MonHocDTO> res = new List<MonHocDTO>();
            try
            {
                var ls = db.MonHocDbs.ToList();
                foreach (var m in ls)
                {
                    MonHocDTO dto = new MonHocDTO();
                    dto.MaMh = m.MaMh;
                    dto.TenMh = m.TenMh;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;

        }
        public bool Update(MonHocDTO mh)
        {
            bool res = false;
            var c = db.MonHocDbs.FirstOrDefault(x => x.MaMh == mh.MaMh);
            if (c.TenMh != mh.TenMh)
                c.TenMh = mh.TenMh;
            try
            {
                db.MonHocDbs.Update(c);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public MonHocDTO GetMHById(int mh)
        {
            MonHocDTO res = new MonHocDTO();
            var c = db.MonHocDbs.FirstOrDefault(x => x.MaMh == mh);
            try
            {
                res.MaMh = c.MaMh;
                res.TenMh = c.TenMh;
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public bool Delete(int maMh)
        {
            bool res = false;
            var mh = db.MonHocDbs.FirstOrDefault(x => x.MaMh == maMh);
            try
            {
                db.MonHocDbs.Remove(mh);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public MonHocDTO Add(MonHocDTO mh)
        {
            MonHocDTO res = new MonHocDTO();
            var c = new MonHocDb();
            c.TenMh = mh.TenMh;
            try
            {
                db.MonHocDbs.Add(c);
                db.SaveChanges();
                res.MaMh = c.MaMh;
                res.TenMh = c.TenMh;
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public MonHocDTO GetById(int id)
        {
            MonHocDTO res = new MonHocDTO();
            try 
            {
                var mh = db.MonHocDbs.FirstOrDefault(mh => mh.MaMh == id);
                if(mh != null)
                {
                    res.MaMh = mh.MaMh;
                    res.TenMh = mh.TenMh;
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
