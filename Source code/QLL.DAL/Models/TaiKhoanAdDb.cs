using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class TaiKhoanAdDb
    {
        public int MaTk { get; set; }
        public string MaAdmin { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }

        public virtual AdminDb MaAdminNavigation { get; set; }
    }
}
