using Microsoft.EntityFrameworkCore;
using TicketApplication.Data.Data;
using TicketApplication.Data.Repository.Imp;
using TicketApplication.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using TicketApplication.Utility;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Services.Interface;
using TicketApplication.Services.Impl;
using TicketApplication.Models.Relationship;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages();

//  Repositories 

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository<Movie>, MoviesRepository>();
builder.Services.AddScoped<IRepository<CinemaHall>, CinemaHallRepository>();
builder.Services.AddScoped<IRepository<MovieShowing>, MovieShowingRepository>();
builder.Services.AddScoped<IRepository<ShoppingCart>, ShoppingCartRepository>();
builder.Services.AddScoped<IRepository<ShowingInShoppingCart>, ShowingInShoppingCartRepository>();
builder.Services.AddScoped<IRepository<ShowingInOrder>, ShowingInOrderRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();

// Services

builder.Services.AddScoped<IMovieShowingService, MovieShowingService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IEmailSender, EmailSender>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
