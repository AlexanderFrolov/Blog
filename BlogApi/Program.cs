using AutoMapper;
using Blog.Data;
using Blog.Data.Repos;
using BlogApi;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
builder.Services.AddControllersWithViews();

// registering a repository service to interact with the database
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlogContext>(options => options.UseSqlite(connection), ServiceLifetime.Singleton);


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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

TestData.EnterDataToBlogDb();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
