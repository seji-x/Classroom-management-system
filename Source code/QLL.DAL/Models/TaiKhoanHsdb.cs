using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class TaiKhoanHsdb
    {
        public int MaTk { get; set; }
        public string MaHs { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }

        public virtual HocSinhDb MaHsNavigation { get; set; }
    }
}
