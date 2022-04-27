using CommentsService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentsService.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly CommentsDbContext _context;

        public ItemRepository(CommentsDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Item item)
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

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetItemByIdAsync(Guid id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task UpdateAsync(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

