using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using QLL.DAL.Models;
using QLL.DTO;

namespace QLL.DAL
{
    public class DiemDAL
    {
        private QuanLyLopContext db;
        public DiemDAL()
        {
            db = new QuanLyLopContext();
        }
        public bool DeleteByIdHs(string maHs)
        {
            bool res = false;
            var ls = db.DiemDbs.Where(x => x.MaHs == maHs).ToList();
            try
            {
                foreach (var d in ls)
                {
                    db.DiemDbs.Remove(d);
                }
                db.SaveChanges();
                res = true;
            }

            catch (Exception ex1)
            {
                res = false;
            } 
            return res;
        }
        public object GetDiemByPage(int page, int size)
        {

            List<DiemDTO> data = new List<DiemDTO>();
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
                var ls = db.DiemDbs.ToList();
                var offset = (page - 1) * size;
                var totalRecord = ls.Count();
                var tottalPage = (totalRecord % size) == 0 ? (int)(totalRecord / size) : (int)(totalRecord / size + 1);
                var lst = ls.Skip(offset).Take(size);
                foreach (var hs in lst)
                {
                    DiemDTO dto = new DiemDTO();
                    dto.MaHs = hs.MaHs;
                    dto.MaKh = hs.MaKh;
                    dto.MaMh = hs.MaMh;
                    dto.Diem = hs.Diem;
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
        public IList<DiemDTO> GetByMonGV(int maLop, string maGv)
        {
            List<DiemDTO> res = new List<DiemDTO>();
            try
            {
                var lst = db.Tkbctdbs.Where(tkb => tkb.MaGv == maGv && tkb.Malop == maLop).Join(db.Tkbdbs,
                                                                                        tkb => tkb.MaTkb,
                                                                                        ct => ct.MaTkb,
                                                                                        (tkb, ct) => new { ct.MaKh, tkb.MaMh, ct.TrangThai }).Where(tkb=>tkb.TrangThai == true).
                                                                                        Join(db.DiemDbs,
                                                                                        ct => ct.MaMh,
                                                                                        d => d.MaMh,
                                                                                        (ct, d) => new { ct.MaMh, ct.MaKh, d.MaHs, d.Diem }).ToList();
                foreach(var d in lst.Distinct())
                {
                    DiemDTO dto = new DiemDTO();
                    dto.MaMh = d.MaMh;
                    dto.MaHs = d.MaHs;
                    dto.Diem = d.Diem;
                    dto.MaKh = d.MaKh;
                    res.Add(dto);
                }
            }
            catch(Exception ex1)
            {
                res = null;  
            }
            return res;
        }
        public IList<DiemDTO> GetAll()
        {
            List<DiemDTO> res = new List<DiemDTO>();
            try
            {
                var ls = db.DiemDbs.ToList();
                foreach (var d in ls)
                {
                    DiemDTO dto = new DiemDTO();
                    dto.MaMh = d.MaMh;
                    dto.MaHs = d.MaHs;
                    dto.Diem = d.Diem;
                    dto.MaKh = d.MaKh;
                    res.Add(dto);
                }
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;

        }
        public IList<PhoDiem> GetAllGroupBy(int maKh, int maLop, int maMon)
        {
            string cnstr = "Server = DESKTOP-73SG70D\\SQLEXPRESS; Database=QuanLyLop; User id = sa; password = 25251025aA";
            SqlConnection cnn = new SqlConnection(cnstr);
            List<PhoDiem> res = new List<PhoDiem>();
            try 
            {
                int f = 0;
                string sqlstr = "select Diem, count(d.MaHS) as SL from DiemDb d inner join HocSinhDb hs on d.MaHS = hs.MaHS where";
                if(maKh != 0)
                {
                    if (f != 0)
                    {
                        sqlstr += " and d.MaKH = " + maKh.ToString();
                        
                    }
                    else
                    {
                        sqlstr += " d.MaKH = " + maKh.ToString();f = 1;
                    }
                }
                if (maLop != 0)
                {
                    if (f != 0)
                    {
                        sqlstr += " and hs.MaLop = " + maLop.ToString();
                    }
                    else
                    {
                        sqlstr += " hs.MaLop = " + maLop.ToString();
                        f = 1;
                    }
                }
                if (maMon != 0)
                {
                    if (f != 0)
                    {
                        sqlstr += " and d.MaMH = " + maMon.ToString();
                    }
                    else
                    {
                        sqlstr += " d.MaMH = " + maMon.ToString();
                        f = 1;
                    }
                }
                sqlstr += " group by Diem";
                SqlCommand cmd = new SqlCommand(sqlstr, cnn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow row in ds.Tables[0].Rows)
                    {
                        PhoDiem ph = new PhoDiem();
                        ph.Diem = double.Parse(row["Diem"].ToString());
                        ph.TotalSV = int.Parse(row["SL"].ToString());
                        res.Add(ph);
                    }
                }


            }
            catch(Exception ex1)
            {
                res = null;
            }
            return res;
        }
        public bool Update(DiemDTO d)
        {
            bool res = false;
            var c = db.DiemDbs.FirstOrDefault(x=> x.MaMh == d.MaMh  && x.MaHs == d.MaHs);
            if (c.Diem != d.Diem)
                c.Diem = d.Diem;
            try
            {
                db.DiemDbs.Update(c);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public bool Delete(int maMh, string maHs)
        {
            bool res = false;
            var d = db.DiemDbs.FirstOrDefault(x => x.MaMh == maMh && x.MaHs == maHs);
            try
            {
                db.DiemDbs.Remove(d);
                db.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public DiemDTO Add(DiemDTO d)
        {
            DiemDTO res = new DiemDTO();
            var c = new DiemDb();
            c.MaMh = d.MaMh;
            c.MaHs = d.MaHs;
            c.Diem = d.Diem;
            c.MaKh = d.MaKh;
            try
            {
                db.DiemDbs.Add(c);
                db.SaveChanges();
                res.MaMh = c.MaMh;
                res.MaHs = c.MaHs;
                res.Diem = c.Diem;
                res.MaKh = c.MaKh;
            }
            catch (Exception ex)
            {
                res = null;
            }
            return res;
        }
        public double GetById(int maMh, string maHs, int maKh)
        {
            var t = db.DiemDbs.FirstOrDefault(x => x.MaHs == maHs && x.MaMh == maMh && x.MaKh == maKh);
            if(t != null)
            {
                return t.Diem;
            }
            return -1;
                
        }
    }
}
