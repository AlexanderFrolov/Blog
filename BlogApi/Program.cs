using AutoMapper;
using Blog.Data;
using Blog.Data.Models;
using Blog.Data.Repos;
using BlogApi;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// registering a repository service to interact with the database
//builder.Services.AddScoped<IPostRepository, PostRepository>();
//builder.Services.AddScoped<ITagRepository, TagRepository>();
//builder.Services.AddScoped<ICommentRepository, CommentRepository>();

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
    v.AddProfile(new MappingProfileApi());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options => options.DefaultScheme = "Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
        {
            OnRedirectToLogin = redirectContext =>
            {
                redirectContext.HttpContext.Response.StatusCode = 401;
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//TestData.EnterDataToBlogDb();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
