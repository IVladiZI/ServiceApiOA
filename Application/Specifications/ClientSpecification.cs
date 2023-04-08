using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class ClientSpecification : Specification<User>
    {
        public ClientSpecification(int userId)
        {
            Query.Where(x => x.UserId == userId)
                .Include(x => x.Client);
        }
    }
}
