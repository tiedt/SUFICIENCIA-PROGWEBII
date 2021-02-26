using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Domain.Identity;
using Project.Respository;

namespace Project.Repository
{
    public class ProjectDAO : IProjectDAO
    {
        private readonly ProjectContext _context;
        public ProjectDAO(ProjectContext context)
        {
            _context = context;
        }

        //Gerais
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            IQueryable<User> query = _context.Users;
            query = query.AsNoTracking().OrderBy(c => c.Id);
            return await query.ToArrayAsync();
        }
        public async Task<User> GetUserAsyncById(int userId)
        {
            IQueryable<User> query = _context.Users;
            query = query.AsNoTracking().OrderBy(c => c.Id)
            .Where(c => c.Id == userId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Produto[]> GetAllProdutoAsync()
        {
            IQueryable<Produto> query = _context.Produtos;
            query = query.AsNoTracking().OrderBy(c => c.id);
            return await query.ToArrayAsync();
        }

        public async Task<Produto> GetProdutoAsyncById(int ProdutoId)
        {
            IQueryable<Produto> query = _context.Produtos;
            query = query.AsNoTracking().OrderBy(c => c.id)
            .Where(c => c.id == ProdutoId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Comandas[]> GetComandasAsync()
        {
            IQueryable<Comandas> query = _context.Comandas;
            query = query.AsNoTracking().OrderBy(c => c.idUsuario);
            return await query.ToArrayAsync();
        }

        public async Task<Comandas> GetComandasAsyncById(int id)
        {
            IQueryable<Comandas> query = _context.Comandas
                .Include(c => c.Produtos);
            query = query.AsNoTracking().OrderBy(c => c.idUsuario)
            .Where(c => c.idUsuario == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}