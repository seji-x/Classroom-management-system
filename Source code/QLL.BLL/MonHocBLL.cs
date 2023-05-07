using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DTO;
using QLL.DAL;

namespace QLL.BLL
{
    public class MonHocBLL
    {
        private MonHocDAL dal;
        public MonHocBLL()
        {
            dal = new MonHocDAL();
        }

        public object GetMHByPage(int page, int size)
        {
            return dal.GetMHByPage(page, size);
        }
        public IList<MonHocDTO> GetAll()
        {
            return dal.GetAll();
        }
        public bool Update(MonHocDTO mh)
        {
            return dal.Update(mh);
        }
        public bool Delete(int maMh)
        {
            return dal.Delete(maMh);
        }
        public MonHocDTO Add(MonHocDTO mh)
        {
            return dal.Add(mh);
        }

        public MonHocDTO GetMHById(int mh)
        {
            return dal.GetMHById(mh);
        }
        public MonHocDTO GetById(int id)
        {
            return dal.GetById(id);
        }
    }
}
