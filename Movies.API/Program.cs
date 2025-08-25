using GraphQL;
using Microsoft.EntityFrameworkCore;
using Movies.API.GraphQL;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Movies");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Movies.API.Data.MoviesDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<Movies.API.GraphQL.Queries.MovieQuery>();
builder.Services.AddScoped<Movies.API.GraphQL.Mutations.MovieMutation>();
builder.Services.AddScoped<MovieSchema>();
builder.Services.AddGraphQL(options => options.AddGraphTypes().AddSystemTextJson().AddDataLoader());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGraphQL<MovieSchema>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
