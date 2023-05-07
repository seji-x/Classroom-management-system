using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLL.DAL;
using QLL.DTO;

namespace QLL.BLL
{
    public class TKBBLL
    {
        private TKBDAL dal;
        public TKBBLL()
        {
            dal = new TKBDAL(); 
        }
        public IList<TKBDTO> GetAll() 
        { 
            return dal.GetAll();
        } public bool Update(TKBDTO tkb) 
        { 
            return dal.Update(tkb);
        }
        public bool Delete(int maTkb) 
        { 
            return dal.Delete(maTkb);
        } 
        public TKBDTO Add(TKBDTO tkb) 
        {
            return dal.Add(tkb);
        }
    }
}
