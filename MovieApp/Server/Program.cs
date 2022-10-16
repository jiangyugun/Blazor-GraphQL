using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MovieApp.Server.DataAccess;
using MovieApp.Server.GraphQL;
using MovieApp.Server.Interface;
using MovieApp.Server.Models;
using System.Security.AccessControl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddPooledDbContextFactory<MovieDBContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMovie, MovieDataAccessLayer>();
builder.Services.AddGraphQLServer()
    .AddQueryType<MovieQueryResolver>()
    .AddMutationType<MovieMutationResolver>()
    .AddFiltering()
    .AddSorting(); ;

var app = builder.Build();
var FilleProviderPath = app.Environment.ContentRootPath + "/Poster";

if (!Directory.Exists(FilleProviderPath))
{
    Directory.CreateDirectory(FilleProviderPath);
}

app.UseFileServer(new FileServerOptions { 
    FileProvider = new PhysicalFileProvider(FilleProviderPath),
    RequestPath = "/Poster",
    EnableDirectoryBrowsing = true
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});
app.MapFallbackToFile("index.html");

app.Run();
