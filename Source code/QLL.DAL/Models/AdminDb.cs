using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class AdminDb
    {
        public string MaAdmin { get; set; }
        public string TenAdmin { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public double Sdt { get; set; }
        public string Email { get; set; }

        public virtual TaiKhoanAdDb TaiKhoanAdDb { get; set; }
    }
}
