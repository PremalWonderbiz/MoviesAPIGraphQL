using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.GraphQL.Types;

namespace Movies.API.GraphQL.Queries
{
    public class MovieQuery : ObjectGraphType
    {
        public MovieQuery(MoviesDbContext dbContext)
        {
            Field<ListGraphType<MovieType>>("movies")
                .ResolveAsync(async m => await dbContext.Movies.Include(m => m.MovieReviews).ToListAsync());

            Field<MovieType>("movie")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>() { Name = "id" }))
                .ResolveAsync(async m =>
                {
                    var id = m.GetArgument<int>("id");
                    return await dbContext.Movies.Include(m => m.MovieReviews).SingleOrDefaultAsync(movie => movie.Id == id);
                });
        }
    }
}
