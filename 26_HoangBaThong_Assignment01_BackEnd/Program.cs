using Microsoft.AspNetCore.OData;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Implements;
using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;
using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Implements;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DI for Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
builder.Services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();

// DI for Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<INewsArticleService, NewsArticleService>();
builder.Services.AddScoped<ISystemAccountService, SystemAccountService>();
builder.Services.AddScoped<ITagService, TagService>();

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Category>("Categories").EntityType.HasKey(c => c.CategoryId);
    builder.EntitySet<NewsArticle>("NewsArticles").EntityType.HasKey(n => n.NewsArticleId);
    builder.EntitySet<SystemAccount>("SystemAccounts").EntityType.HasKey(a => a.AccountId);
    return builder.GetEdmModel();
}

builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

