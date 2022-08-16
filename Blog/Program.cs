using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Blog.Data;
using Blog;
using Blog.Data.Repos;
using Blog.Data.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

// registering a repository service to interact with the database
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IPostRepository, PostRepository>();
//builder.Services.AddScoped<ITagRepository, TagRepository>();
//builder.Services.AddScoped<ICommentRepository, CommentRepository>();
//builder.Services.AddScoped<IRoleRepository, RoleRepository>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddDbContext<BlogContext>(options => options.UseSqlite(connection), ServiceLifetime.Singleton)
    .AddIdentity<User, IdentityRole>(opts => {
        opts.Password.RequiredLength = 5;
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequireLowercase = false;
        opts.Password.RequireUppercase = false;
        opts.Password.RequireDigit = false;
    })
    .AddEntityFrameworkStores<BlogContext>();


var mapperConfig = new MapperConfiguration((v) =>
{
    v.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(options => options.DefaultScheme = "Cookies")
    //.AddCookie("Cookies", options =>
    //{
    //    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
    //    {
    //        OnRedirectToLogin = redirectContext =>
    //        {
    //            redirectContext.HttpContext.Response.StatusCode = 401;
    //            return Task.CompletedTask;
    //        }
    //    };
    //})
    .AddCookie(options => options.LoginPath = "/Home/authorization"); 


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//TestData.EnterDataToBlogDb(); 



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
