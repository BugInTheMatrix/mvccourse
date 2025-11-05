using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Repositories;
// "MyMvcAppDb": "Server=localhost;Database=MyMvcAppDb;User Id=sa;Password=SQLConnect1!;TrustServerCertificate=true"

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyMvcAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyMvcAppDb"));

});
builder.Services.AddScoped<ITagInterface, TagRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
