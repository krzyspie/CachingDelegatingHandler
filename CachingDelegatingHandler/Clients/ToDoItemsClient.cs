using CachingDelegatingHandler.Models;

namespace CachingDelegatingHandler.Clients
{
    public class ToDoItemsClient : IToDoItemsClient
    {
        private readonly HttpClient httpClient;

        public ToDoItemsClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<Todo>> GetItems()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{httpClient.BaseAddress}todos");
            
            if(!response.IsSuccessStatusCode)
            {
                return (IReadOnlyCollection<Todo>)Task.FromResult(Enumerable.Empty<Todo>());
            }

            return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<Todo>>();
        }
    }
}
