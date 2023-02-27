using CachingDelegatingHandler.Clients;
using Microsoft.AspNetCore.Mvc;

namespace RetryDelegatingHandler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IToDoItemsClient toDoItemsClient;

        public ToDoItemsController(IToDoItemsClient toDoItemsClient)
        {
            this.toDoItemsClient = toDoItemsClient;
        }

        [HttpGet()]
        public async Task<IEnumerable<object>> Get()
        {
            return await this.toDoItemsClient.GetItems();
        }
    }
}