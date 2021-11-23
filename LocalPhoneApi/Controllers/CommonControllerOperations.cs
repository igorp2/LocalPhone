using LocalPhoneDomain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalPhoneApi
{
    public static class CommonControllerOperations
    {
        public static (string, bool) GetOrderByThatWillBeUsedAndIfIsDescending(string orderBy, string orderByDescending)
        {
            string whichOrderByToUse = null;
            bool isOrderByInDescendingOrder = false;

            if (!string.IsNullOrEmpty(orderBy))
            {
                whichOrderByToUse = orderBy;
            }
            else if (!string.IsNullOrEmpty(orderByDescending))
            {
                whichOrderByToUse = orderByDescending;
                isOrderByInDescendingOrder = true;
            }

            return (whichOrderByToUse, isOrderByInDescendingOrder);
        }

        public static void AddPaginationInfosToHttpResponseHeaders(HttpResponse response, PaginationInformations paginationInfos)
        {
            if (paginationInfos != null)
            {
                response.Headers.Add("X-Pagination-PageSize", paginationInfos.PageSize.ToString());
                response.Headers.Add("X-Pagination-PageIndex", paginationInfos.CurrentPageIndex.ToString());
                response.Headers.Add("X-Pagination-TotalNumberOfPages", paginationInfos.TotalNumberOfPages.ToString());
            }
        }
    }
}
