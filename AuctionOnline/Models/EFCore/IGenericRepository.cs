using System.Linq;
using System.Threading.Tasks;

namespace AuctionOnline.Models.EFCore
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> FindAll();

        Task<TEntity> FindById(int id);
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(int id);

        Task Create(TEntity entity);

        Task Update(int id, TEntity entity);

        Task Delete(int id);
    }
}