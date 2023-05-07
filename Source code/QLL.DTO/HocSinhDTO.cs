using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLL.DTO
{
    public class HocSinhDTO
    {
        public string MaHs { get; set; }
        public string TenHs { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public double? Sdt { get; set; }
        public string? Email { get; set; }
        public int MaLop { get; set; }
    }
}
