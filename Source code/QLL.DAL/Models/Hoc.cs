using System;
using System.Collections.Generic;

#nullable disable

namespace QLL.DAL.Models
{
    public partial class Hoc
    {
        public int MaKh { get; set; }
        public int MaLop { get; set; }

        public virtual KhoaHocDb MaKhNavigation { get; set; }
        public virtual LopDb MaLopNavigation { get; set; }
    }
}
