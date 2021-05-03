using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject_Manager_Api.Dtos.Querying_Models{
    public class PageResult<T>{
        
        public List<T> Items { get; set; }
        public int TotalPagesCount { get; set; }
        public int NumberItem_First { get; set; }
        public int NumberItem_Last { get; set; }
        public int TotalItemsCount { get; set; }

        public PageResult(List<T> items, int totalItemsCount, int pageSize, int pageNumber) {
            this.Items = items;
            this.TotalItemsCount = totalItemsCount;
            this.NumberItem_First = pageSize * (pageNumber-1) +1;
            this.NumberItem_Last = this.NumberItem_First + pageSize-1;
            this.TotalPagesCount = Convert.ToInt32(Math.Ceiling(totalItemsCount/(double) pageSize));
        }

    }
}
