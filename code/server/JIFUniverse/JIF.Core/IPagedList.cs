using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core
{
    public interface IPaged
    {
        int PageIndex { get; }
        int PageSize { get; }
        //int IndividualPagesDisplayedCount { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }

        //int FirstIndividualPageIndex { get; }
        //int LastIndividualPageIndex { get; }
    }


    /// <summary>
    /// Paged list interface
    /// </summary>
    public interface IPagedList<T> : IPaged, IList<T>
    {

    }
}
