using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Parameters
{
    /// <RequestParameter>
    /// In this class we will receive the request to return the number of pages and size of this list for pagination.
    /// </RequestParameter>
    public class RequestParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public RequestParameter()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        /// <RequestParameter>
        /// In this constructor we will request to the client the way in which the page will be shown, 
        /// always taking as default values that it is a minimum of one page and 10 minimum data.
        /// </RequestParameter>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="pageSize">Number of rows for that page</param>
        public RequestParameter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
