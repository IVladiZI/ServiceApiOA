using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappres
{
    /// <PagedResponse>
    /// This class will help us to respond to a customer page.
    /// where we inherit from the response class for already customized values
    /// </PagedResponse>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedResponse(T data,int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            Message = string.Empty;
            Success = true;
            Errors = null;
        }
    }
}
