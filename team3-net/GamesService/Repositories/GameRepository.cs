using GamesService.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesService.Repositories
{
    public class Repository<T> : IRepository<T> where T:BaseEntity 
    {
        private readonly GamesDbContext _context;
        private DbSet<T> _enteties;
        public Repository(GamesDbContext context)
        {
            _context = context;
            _enteties = context.Set<T>();
        }

        public async Task CreateAsync(T comment)
        {
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var comment = await _context.Games.FindAsync(id);

            _context.Games.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync()
        {
            return await _enteties.ToListAsync();
        }

        public async Task<T> GetItemByIdAsync(Guid id)
        {
            return await _enteties.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(T comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

