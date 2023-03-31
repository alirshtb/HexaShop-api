using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common.Dtos
{
    public class PagedList<T> : List<T>
    {
        public int PageNumber { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }
        public int PagesCount { get; private set; } 
        public int RowsCount { get; private set; }
        public string? Search { get; set; }
        public bool HasPreviouse
        {
            get
            {
                return PageNumber > 1;
            }
        }
        public bool HasNext
        {
            get
            {
                return PageNumber < PagesCount;
            }
        }


        public PagedList(List<T> dataList, int pageSize, int pageNumber, int totalCount, string? search)
        {
            PageNumber = pageNumber;
            TotalCount = totalCount;
            PageSize = pageSize;
            RowsCount = dataList.Count();
            PagesCount = (int)Math.Ceiling(totalCount / (double)pageSize);
            AddRange(dataList);
            Search = search;
            Search = search;
        }

        /// <summary>
        /// create paginated list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static PagedList<T> Create<T>(IQueryable<T> source, int pageSize, int pageNumber, string? search)
        {
            var result = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(result, pageSize, pageNumber, source.Count(), search);
        }



    }
}
