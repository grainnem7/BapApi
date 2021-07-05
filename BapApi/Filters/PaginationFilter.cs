//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BapApi.Filters
//{
//    public class PaginationFilter
//    {
//        public int PageNumber { get; set; }
//        public int PageSize { get; set; }
//        public PaginationFilter()
//        {
//            this.PageNumber = 1;
//            this.PageSize = 10;
//        }
//        public PaginationFilter(int pageNumber, int pageSize)
//        {
//            //minimum page number is always set to 1
//            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
//            //maximum page number set to 10 for now. Can be changed
//            this.PageSize = pageSize > 10 ? 10 : pageSize;
//        }
//    }
//}
