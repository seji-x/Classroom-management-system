using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class GiaoVienDb
    {
        public GiaoVienDb()
        {
            Tkbctdbs = new HashSet<Tkbctdb>();
        }

        public string MaGv { get; set; }
        public string TenGv { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public double Sdt { get; set; }
        public string Email { get; set; }
        public string ChuyenNganh { get; set; }
        public string TrinhDoChuyenMon { get; set; }

        public virtual TaiKhoanGvdb TaiKhoanGvdb { get; set; }
        public virtual ICollection<Tkbctdb> Tkbctdbs { get; set; }
    }
}
