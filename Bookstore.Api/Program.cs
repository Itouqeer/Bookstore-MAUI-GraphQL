using Bookstore.Api.Data;
using Bookstore.Api.GraphQL;
using Bookstore.Api.GraphQL.Mutations;
using Bookstore.Api.GraphQL.Queries;
using Bookstore.Api.Repositories;
using Bookstore.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(5, 7, 29))));

// Dependency Injection for Repositories and Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

// Register individual query and mutation classes
builder.Services.AddScoped<BookQuery>();
builder.Services.AddScoped<AuthorQuery>();
builder.Services.AddScoped<BookMutation>();
builder.Services.AddScoped<AuthorMutation>();

// GraphQL Setup
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<BookQuery>()
    .AddType<BookMutation>()
    .AddType<AuthorQuery>()
    .AddType<AuthorMutation>();

// Build the application
var app = builder.Build();

// Middleware configuration
app.UseRouting();
app.MapGraphQL();

app.Run();
