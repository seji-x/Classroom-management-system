using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL;
using QLL.DTO;

namespace QLL.BLL
{
    public class TKBCTBLL
    {
        private TKBCTDAL dal;
        public TKBCTBLL()
        {
            dal = new TKBCTDAL();
        }
        public IList<TKBCTDTO> GetById(int maTKB)
        {
            return dal.GetById(maTKB);
        }
        public bool DeleteByIdTKB(int maTKB)
        {
            return dal.DeleteByIdTKB(maTKB);
        }
        public IList<TKBCTDTO> GetAll()
        {
            return dal.GetAll();
        }
        public bool Update(TKBCTDTO tkb)
        {
            return dal.Update(tkb);
        }
        public bool Delete(int maTKB, int thu, int tiet)
        {
            return dal.Delete(maTKB, thu, tiet);
        }
        public TKBCTDTO Add(TKBCTDTO tkb)
        {
            return dal.Add(tkb);
        }

        public IList<List_Mh> GetMonHocByIdGv(string maGv, int maLop)
        {
            return dal.GetMonHocByIdGv(maGv, maLop);
        }
        public IList<TKBCTDTO> getTkbByLop(int maLop)
        {
            return dal.getTkbByLop(maLop);
        }
        public IList<TKBCTDTO> getTbkByGv(string maGv)
        {
            return dal.getTbkByGv(maGv);
        }

        public object GetTKBByPage(int page, int size)
        {
            return dal.GetTKBByPage(page, size);
        }
    }
}
