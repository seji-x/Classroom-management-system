using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLL.DTO
{
    public class DiemDTO
    {
        public int MaMh { get; set; }
        public string MaHs { get; set; }
        public double Diem { get; set; }
        public int MaKh { get; set; }
    }
    public class PhoDiem
    {
        public int TotalSV { get; set; }
        public double Diem { get; set; }
    }
}
