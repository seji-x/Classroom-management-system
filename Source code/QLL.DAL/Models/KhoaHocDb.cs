using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class KhoaHocDb
    {
        public KhoaHocDb()
        {
            DiemDbs = new HashSet<DiemDb>();
            Hocs = new HashSet<Hoc>();
            Tkbdbs = new HashSet<Tkbdb>();
        }

        public int MaKh { get; set; }
        public string TenKh { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public virtual ICollection<DiemDb> DiemDbs { get; set; }
        public virtual ICollection<Hoc> Hocs { get; set; }
        public virtual ICollection<Tkbdb> Tkbdbs { get; set; }
    }
}
