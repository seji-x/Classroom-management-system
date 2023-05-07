using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class DiemDb
    {
        public int MaMh { get; set; }
        public string MaHs { get; set; }
        public int MaKh { get; set; }
        public double Diem { get; set; }

        public virtual HocSinhDb MaHsNavigation { get; set; }
        public virtual KhoaHocDb MaKhNavigation { get; set; }
        public virtual MonHocDb MaMhNavigation { get; set; }
    }
}
