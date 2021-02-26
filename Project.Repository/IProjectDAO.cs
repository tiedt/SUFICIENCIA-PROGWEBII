using System.Threading.Tasks;
using Project.Domain;
using Project.Domain.Identity;

namespace Project.Repository
{
    public interface IProjectDAO
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<User[]> GetAllUsersAsync();
        Task<User> GetUserAsyncById(int userId);

        Task<Produto[]> GetAllProdutoAsync();
        Task<Produto> GetProdutoAsyncById(int TelefoneId);

        Task<Comandas> GetComandasAsyncById(int id);
        Task<Comandas[]> GetComandasAsync();
    }
}