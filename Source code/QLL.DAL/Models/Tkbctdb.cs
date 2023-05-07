using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class Tkbctdb
    {
        public int MaTkb { get; set; }
        public int Malop { get; set; }
        public int Thu { get; set; }
        public int Tiet { get; set; }
        public int MaMh { get; set; }
        public string MaGv { get; set; }

        public virtual GiaoVienDb MaGvNavigation { get; set; }
        public virtual MonHocDb MaMhNavigation { get; set; }
        public virtual Tkbdb MaTkbNavigation { get; set; }
        public virtual LopDb MalopNavigation { get; set; }
    }
}
