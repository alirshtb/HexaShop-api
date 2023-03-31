using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.Common
{
    public abstract class GetListBaseDto
    {
        private string? _search { get; set; }
        private int _pageNumber { get; set; }
        private int _pageSize { get; set; }


        public string? Search
        {
            get
            {
                return _search == null ? null : _search.ToLower();
            }
            set
            {
                _search = value;
            }
        }
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                if(value > 0) { _pageNumber = value; }
            }
        }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if(value > 0) { _pageSize = value; }
            }
        }

        public GetListBaseDto()
        {
            PageNumber = 1;
            PageSize = 10;
            Search = null;
        }

    }
}
