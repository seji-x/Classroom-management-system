using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class HocSinhDb
    {
        public HocSinhDb()
        {
            DiemDbs = new HashSet<DiemDb>();
        }

        public string MaHs { get; set; }
        public string TenHs { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public double? Sdt { get; set; }
        public string Email { get; set; }
        public int MaLop { get; set; }

        public virtual LopDb MaLopNavigation { get; set; }
        public virtual TaiKhoanHsdb TaiKhoanHsdb { get; set; }
        public virtual ICollection<DiemDb> DiemDbs { get; set; }
    }
}
