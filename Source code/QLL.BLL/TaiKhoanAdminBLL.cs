using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL;
using QLL.DTO;

namespace QLL.BLL
{
    public class TaiKhoanAdminBLL
    {
        private TaiKhoanAdminDAL dal;
        public TaiKhoanAdminBLL()
        {
            dal = new TaiKhoanAdminDAL();
        }
        public IList<TaiKhoanAdminDTO> GetAll()
        {
            return dal.GetAll();
        }
        public bool Update(TaiKhoanAdminDTO tk)
        {
            return dal.Update(tk);
        }
        public bool Delete(int maTk)
        {
            return dal.Delete(maTk);
        }
        public TaiKhoanAdminDTO Add(TaiKhoanAdminDTO tk)
        {
            return dal.Add(tk);
        }
        public string login(string username, string password)
        {
            return dal.login(username, password);
        }
    }
}
