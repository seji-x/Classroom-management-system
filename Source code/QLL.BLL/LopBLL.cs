using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL;
using QLL.DTO;

namespace QLL.BLL
{
    public class LopBLL
    {
        private LopDAL dal;
        public LopBLL()
        {
            dal = new LopDAL();
        }
        public IList<LopDTO> GetAll()
        {
            return dal.GetAll();
        }
        public LopDTO GetLopById(int id)
        {
            return dal.GetLopById(id);
        }
        public bool UpdateLop(LopDTO lop)
        {
            return dal.Update(lop);
        }
        public bool Delete(int maLop)
        {
            return dal.Delete(maLop);
        }
        public LopDTO Add(LopDTO lop)
        {
            return dal.Add(lop);
        }
        public IList<LopDTO> GetLopByIdGV(string maGv)
        {
            return dal.GetLopByIdGV(maGv);
        }

        public IList<KhoaHocDTO> GetAllKH(int maLop)
        {
            return dal.GetAllKH(maLop);
        }
    }
}
