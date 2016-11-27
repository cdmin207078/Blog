using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core
{
    public class PagedData<T>
    {
        public PagedData(IPagedList<T> source)
        {
            this.Items = source;
            this.PageIndex = source.PageIndex;
            this.PageSize = source.PageSize;
            this.TotalCount = source.TotalCount;
            this.TotalPages = source.TotalPages;
        }

        public PagedData(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex < 1 ? 1 : pageIndex;

            this.Items = source.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

        }

        public PagedData(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;


            this.Items = source;
        }

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 1); }
        }
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }

        public IEnumerable<T> Items { get; set; }

    }
}
