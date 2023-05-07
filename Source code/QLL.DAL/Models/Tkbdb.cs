using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class Tkbdb
    {
        public Tkbdb()
        {
            Tkbctdbs = new HashSet<Tkbctdb>();
        }

        public int MaTkb { get; set; }
        public int MaKh { get; set; }
        public string TenTkb { get; set; }
        public bool TrangThai { get; set; }

        public virtual KhoaHocDb MaKhNavigation { get; set; }
        public virtual ICollection<Tkbctdb> Tkbctdbs { get; set; }
    }
}
