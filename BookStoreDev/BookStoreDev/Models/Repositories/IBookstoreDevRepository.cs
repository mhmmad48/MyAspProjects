using System.Collections.Generic;

namespace BookStoreDev.Models.Repositories
{
    public interface IBookstoreDevRepository<entity>
    {
        Task Add(entity entity);
        Task Update(entity entity);
        Task Delete(int id);
        Task<entity> Find(int id);
        IList<entity>GetAll();

    }
}
