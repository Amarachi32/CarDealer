using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.DLL.Entities
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int totalCount, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            PageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }
     
    }
}
