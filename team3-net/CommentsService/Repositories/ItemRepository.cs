using CommentsService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentsService.Repositories
{
    public class Repository<T> : IRepository<T> where T:BaseEntity 
    {
        private readonly CommentsDbContext _context;
        private DbSet<T> _enteties;
        public Repository(CommentsDbContext context)
        {
            _context = context;
            _enteties = context.Set<T>();
        }

        public async Task CreateAsync(T item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _context.Items.FindAsync(id);

            _context.Items.Remove(item);
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

        public async Task UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

