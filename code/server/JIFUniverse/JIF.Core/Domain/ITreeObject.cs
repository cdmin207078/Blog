using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain
{
    public interface ITreeObject<T>
    {

        int ParentId { get; set; }

        T Parent { get; set; }

        IEnumerable<T> Subs { get; set; }

    }
}
