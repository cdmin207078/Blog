using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core
{
    public abstract partial class __Base
    {
        public int Id { get; set; }
    }

    public abstract partial class __Base_C : __Base
    {
        public DateTime CreateTime { get; set; }

        public int CreateUserId { get; set; }
    }

    public abstract partial class __Base_CU : __Base_C
    {
        public DateTime? UpdateTime { get; set; }

        public int? UpdateUserId { get; set; }
    }

}
