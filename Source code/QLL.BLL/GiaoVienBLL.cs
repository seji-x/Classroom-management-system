using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL;
using QLL.DTO;

namespace QLL.BLL
{
    public class GiaoVienBLL
    {
        private GiaoVienDAL dal;
        public GiaoVienBLL()
        {
            dal = new GiaoVienDAL();
        }
        public List<string> GetID()
        {
            return dal.GetID();
        }
        public IList<GiaoVienDTO> GetAll()
        {
            return dal.GetAll();
        }
        public object GetGVByPage(int page, int size)
        {
            return dal.GetGVByPage(page, size);
        }    
        public bool Update(GiaoVienDTO gv)
        {
            return dal.Update(gv);
        }
        public bool Delete(string maGV)
        {
            return dal.Delete(maGV);
        }
        public GiaoVienDTO Add(GiaoVienDTO gv)
        {
            return dal.Add(gv);
        }

        public GiaoVienDTO GetByID(string maGv)
        {
            return dal.GetByID(maGv);
        }
    }
}
