using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPhoneDomain
{
    public class PaginationInformations
    {
        public int CurrentPageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalNumberOfPages { get; private set; }

        public PaginationInformations(int totalNumberOfPages, int pageSize, int currentPageIndex)
        {
            if (totalNumberOfPages >= 0)
                TotalNumberOfPages = totalNumberOfPages;

            if (pageSize >= 0)
                PageSize = pageSize;

            if (currentPageIndex > 0)
                CurrentPageIndex = currentPageIndex;
        }
    }
}
