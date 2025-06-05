using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.ConsentCookieValue = "true";
});
builder.Services.AddDbContext<TodoDb>(options =>
    options.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoAPI";
    config.Title = "TodoAPI v1";
    config.Version = "v1";
});
// register ratemit middleware
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("Fixed", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 5; // allow 5 requests per 10 seconds
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
    options.AddSlidingWindowLimiter("Sliding", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 5; // allow 5 requests per 10 seconds
        opt.SegmentsPerWindow = 2; // divide the window into segments
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
    options.AddTokenBucketLimiter("TokenBucket", opt =>
    {
        opt.TokenLimit = 10; // allow 10 tokens
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
        opt.ReplenishmentPeriod = TimeSpan.FromSeconds(10); // replenish tokens every 10 seconds
        opt.TokensPerPeriod = 2; // replenish 2 tokens per period
    });
    options.AddConcurrencyLimiter("Concurrency", opt =>
    {
        opt.PermitLimit = 5; // allow 5 concurrent requests
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2; // allow 2 requests in the queue
    });
});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.Configure<ThemeSettings>(
    builder.Configuration.GetSection("Theme"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.UseHttpsRedirection();

// register custom middleware
app.UseRequestLogging();

app.UseRouting();
app.UseRateLimiter(); // apply rate limiting middleware
//app.MapGet("/hello", () => "Hello Muhammad"); // register endpoints with MapGet
app.MapGet("/test", context =>
{
    context.Response.Redirect("/privacy");
    return Task.CompletedTask;
}); // custom endpoint that take us to Home/Index page

// todo api
app.MapGet("/api/todos", async (TodoDb db) =>
{
    return await db.Todos.ToListAsync();
});
app.MapGet("/api/todos/{id}", async (int id, TodoDb db) =>
{
    return await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound();
});
app.MapPost("/api/todos", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/api/todos/{todo.Id}", todo);
});
app.MapPut("/api/todos/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/api/todos/{id}", async (int id, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();

    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.UseCookiePolicy();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.MapControllers().RequireRateLimiting("Fixed"); // apply rate limiting to controllers
app.Run();
