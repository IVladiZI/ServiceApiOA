using Ardalis.Specification;

namespace Application.Interfaces
{
    /// <IRepositoryAsync>
    /// This is the repository interface where a T that will be a class is implemented, 
    /// it is inherited from IRepositoryBase which is from ardalis where the entire repository is already available
    /// </IRepositoryAsync>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryAsync<T> : IRepositoryBase<T> where T : class
    {
    }
    /// <summary>
    /// This interface brings all the methods of ardalis for the functionalities of reading in the database
    /// </summary>
    /// <typeparam name="T">Class Type</typeparam>
    public interface IReadRepositoryAsync<T> : IRepositoryBase <T> where T : class
    {

    }
}
