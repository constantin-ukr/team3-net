using CommentsService.Entities;

namespace CommentsService.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(Guid id);
        Task CreateAsync(Item entity);
        Task UpdateAsync(Item entity);
        Task DeleteAsync(Guid id);

    }
}
