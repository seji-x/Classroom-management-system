using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class LopDb
    {
        public LopDb()
        {
            HocSinhDbs = new HashSet<HocSinhDb>();
            Hocs = new HashSet<Hoc>();
            Tkbctdbs = new HashSet<Tkbctdb>();
        }

        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public string MoTa { get; set; }
        public string PhongHoc { get; set; }
        public string TrangThai { get; set; }

        public virtual ICollection<HocSinhDb> HocSinhDbs { get; set; }
        public virtual ICollection<Hoc> Hocs { get; set; }
        public virtual ICollection<Tkbctdb> Tkbctdbs { get; set; }
    }
}
