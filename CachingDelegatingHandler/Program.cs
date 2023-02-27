using CachingDelegatingHandler.Clients;
using CachingDelegatingHandler.DelegatingHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddLogging();
builder.Services.AddTransient<CachingHandler>();
builder.Services.AddTransient<LoggingHandler>();
builder.Services
    .AddHttpClient<IToDoItemsClient, ToDoItemsClient>(client => client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"))
    .AddHttpMessageHandler<LoggingHandler>()
    .AddHttpMessageHandler<CachingHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
