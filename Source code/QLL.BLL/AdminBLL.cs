using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL;
using QLL.DTO;

namespace QLL.BLL
{
    public class AdminBLL
    {
        private AdminDAL dal;
        public AdminBLL()
        {
            dal = new AdminDAL();
        }

        public object GetAdByPage(int page, int size)
        {
            return dal.GetAdByPage(page, size);
        }
        public IList<AdminDTO> GetAll()
        {
            return dal.GetAll();
        }
        public List<string> GetID()
        {
            return dal.GetID();
        }
        public bool Update(AdminDTO ad)
        {
            return dal.Update(ad);
        }
        public bool Delete(string maAd)
        {
            return dal.Delete(maAd);
        }
        public AdminDTO Add(AdminDTO ad)
        {
            return dal.Add(ad);
        }

        public AdminDTO GetById(string maAd)
        {
            return dal.GetById(maAd);
        }
    }
}
