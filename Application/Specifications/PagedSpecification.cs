using Application.DTOs;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    /// <PagedSpecification>
    /// This class helps us to be able to manipulate the information that we are going to request to the database.
    /// The inheritance Specification class helps us to control what we are going to request to the database.
    /// </PagedSpecification>
    public class PagedSpecification : Specification<User>
    {
        /// <PagedSpecification>
        /// In this constructor we are going to configure the pagination, the nugget 
        /// specification helps us to easily configure the pagination, 
        /// it will also help us to filter the information that we will request to the database.
        /// </summary>
        /// <PagedSpecification name="filter">Class where are the paging parameters</param>
        public PagedSpecification(GetAllClientParametersDto filter)
        {
            Query.Include(x => x.Client);
            Query.Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .OrderBy(x => x.UserId);

            if (!string.IsNullOrEmpty(filter.Name))
                Query.Search(x => x.Client.Name, $"%{filter.Name.ToUpper()}%")
                    .OrderBy(x => x.Client.Name);
            if (!string.IsNullOrEmpty(filter.DocumentNumber))
                Query.Where(x => x.Client.DocumentNumber == filter.DocumentNumber);
        }
    }
}
