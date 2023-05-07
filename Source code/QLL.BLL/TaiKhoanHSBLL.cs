using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DTO;
using QLL.DAL;

namespace QLL.BLL
{
    public class TaiKhoanHSBLL
    {
        private TaiKhoanHSDAL dal;
        public TaiKhoanHSBLL()
        {
            dal = new TaiKhoanHSDAL();
        }
        public IList<TaiKhoanHSDTO> GetAll()
        {
            return dal.GetAll();
        }
        public bool DeleteById(string maHs)
        {
            return dal.DeleteById(maHs);
        }
        public bool Update(TaiKhoanHSDTO tk)
        {
            return dal.Update(tk);
        }
        public bool Delete(int maTk)
        {
            return dal.Delete(maTk);
        }
        public TaiKhoanHSDTO Add(TaiKhoanHSDTO tk)
        {
            return dal.Add(tk);
        }
        public string login(string username, string password)
        {
            return dal.login(username, password);
        }
    }
}
