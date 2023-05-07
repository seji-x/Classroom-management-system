using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL;
using QLL.DTO;

namespace QLL.BLL
{
    public class KhoaHocBLL
    {
        private KhoaHocDAL dal;
        public KhoaHocBLL()
        {
            dal = new KhoaHocDAL();
        }
        public IList<KhoaHocDTO> GetAll()
        {
            return dal.GetAll();
        }

        public KhoaHocDTO GetById(int maKh)
        {
            return dal.GetById(maKh);
        }
        public bool Update(KhoaHocDTO kh)
        {
            return dal.Update(kh);
        }
        public bool Delete(int maKh)
        {
            return dal.Delete(maKh);
        }
        public KhoaHocDTO Add(KhoaHocDTO kh)
        {
            return dal.Add(kh);
        }

        public IList<MonHocDTO> GetAllMH(int maLop)
        {
            return dal.GetAllMH(maLop);
        }
    }
}
