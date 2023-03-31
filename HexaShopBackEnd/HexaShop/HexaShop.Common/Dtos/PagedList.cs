using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common.Dtos
{
    public class PagedList<T> : List<T>
    {
        public GetListMetaData MetaData { get; set; } = new GetListMetaData();
        public bool HasPreviouse
        {
            get
            {
                return MetaData.PageNumber > 1;
            }
        }
        public bool HasNext
        {
            get
            {
                return MetaData.PageNumber < MetaData.PagesCount;
            }
        }


        public PagedList(List<T> dataList, int pageSize, int pageNumber, int totalCount, string? search)
        {
            MetaData.PageNumber = pageNumber;
            MetaData.TotalCount = totalCount;
            MetaData.PageSize = pageSize;
            MetaData.RowsCount = dataList.Count();
            MetaData.PagesCount = (int)Math.Ceiling(totalCount / (double)pageSize);
            MetaData.Search = search;
            AddRange(dataList);
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
