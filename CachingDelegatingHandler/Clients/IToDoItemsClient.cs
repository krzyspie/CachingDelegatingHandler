using CachingDelegatingHandler.Models;

namespace CachingDelegatingHandler.Clients
{
    public interface IToDoItemsClient
    {
        public Task<IReadOnlyCollection<Todo>> GetItems();
    }
}
