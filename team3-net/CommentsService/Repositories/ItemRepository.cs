using CommentsService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentsService.Repositories
{
    public class ItemRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly CommentsDbContext _context;
        public ItemRepository(CommentsDbContext context)
        {
            _context = context;
            //if (!context.Comment.Any())
            //{
            //    context.Comment.Add(new Item { Name = "First Comment", Description = "Some description", CreatedDate = DateTime.UtcNow });
            //    context.Comment.Add(new Item { Name = "Second Comment", Description = "Some description", CreatedDate = DateTime.UtcNow });


            //    context.SaveChanges();
            //}
        }

        public async Task CreateAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

            _context.Set<T>().Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetItemByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);

        }

        public async Task UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

