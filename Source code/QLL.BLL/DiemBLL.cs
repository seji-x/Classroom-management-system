using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL;
using QLL.DTO;

namespace QLL.BLL
{
   public class DiemBLL
    {
        private DiemDAL dal;
        public DiemBLL()
        {
            dal = new DiemDAL();
        }
        public object GetDiemByPage(int page, int size)
        {
            return dal.GetDiemByPage(page, size);
        }
        public bool DeleteByIdHs(string maHs)
        {
            return dal.DeleteByIdHs(maHs);
        }
        public IList<DiemDTO> GetByMonGV(int maLop, string maGv)
        {
            return dal.GetByMonGV(maLop, maGv);
        }
        public IList<PhoDiem> GetAllGroupBy(int maKh, int maLop, int maMon)
        {
            return dal.GetAllGroupBy(maKh, maLop, maMon);
        }
        public double GetById(int maMh, string maHs, int maKh)
        {
            return dal.GetById(maMh, maHs, maKh);
        }
        public IList<DiemDTO> GetAll()
        {
            return dal.GetAll();
        }
        public bool Update(DiemDTO d)
        {
            return dal.Update(d);
        }
        public bool Delete(int maMh, string maHs)
        {
            return dal.Delete(maMh, maHs);
        }
        public DiemDTO Add(DiemDTO d)
        {
            return dal.Add(d);
        }
    }
}
