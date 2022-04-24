using CommentsService.Entities;

namespace CommentsService.Repositories
{
    public class ItemRepository<EntityType> : IRepository<EntityType> where EntityType : BaseEntity
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

        public Task CreateAsync(EntityType item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(id)
        {
            var item = await _context.FindAsync(id);
            _context.Remove(item);
        }

        public async Task<IEnumerable<EntityType>> GetAllItemsAsync()
        {
            return await GetAllItemsAsync();
        }

        public Task<EntityType> GetItemByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EntityType item)
        {
            throw new NotImplementedException();
        }
    }
}

