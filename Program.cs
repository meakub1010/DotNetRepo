using RazorPagesMovie.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.ConsentCookieValue = "true";
});
// Add services to the container.
builder.Services.AddRazorPages();
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

app.UseHttpsRedirection();

// register custom middleware
app.UseRequestLogging();

app.UseRouting();

app.MapGet("/hello", () => "Hello Muhammad"); // register endpoints with MapGet
app.MapGet("/test", context =>
{
    context.Response.Redirect("/privacy");
    return Task.CompletedTask;
}); // custom endpoint that take us to Home/Index page
app.UseCookiePolicy();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
