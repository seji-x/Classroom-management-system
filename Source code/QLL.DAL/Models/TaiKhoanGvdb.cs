using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class TaiKhoanGvdb
    {
        public int MaTk { get; set; }
        public string MaGv { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }

        public virtual GiaoVienDb MaGvNavigation { get; set; }
    }
}
