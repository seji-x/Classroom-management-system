using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL;
using QLL.DTO;

namespace QLL.BLL
{
    public class TaiKhoanGVBLL
    {
        private TaiKhoanGVDAL dal;
        public TaiKhoanGVBLL()
        {
            dal = new TaiKhoanGVDAL();
        }
        public IList<TaiKhoanGVDTO> GetAll()
        {
            return dal.GetAll();
        }
        public bool Update(TaiKhoanGVDTO tk)
        {
            return dal.Update(tk);
        }
        public bool Delete(int maTk)
        {
            return dal.Delete(maTk);
        }
        public TaiKhoanGVDTO Add(TaiKhoanGVDTO tk)
        {
            return dal.Add(tk);
        }
        public string login(string username, string password)
        {
            return dal.login(username, password);
        }
    }
}
