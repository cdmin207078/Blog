using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain
{
    public interface ITreeObject<T>
    {
        int Id { get; set; }

        int ParentId { get; set; }

        T Parent { get; set; }

        IList<T> Subs { get; set; }

    }
}
