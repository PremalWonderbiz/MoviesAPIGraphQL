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
                .Resolve(m => dbContext.Movies.ToList());

            Field<MovieType>("movie")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>() { Name = "id" }))
                .Resolve(m =>
                {
                    var id = m.GetArgument<int>("id");
                    return dbContext.Movies.SingleOrDefault(movie => movie.Id == id);
                });
        }
    }
}
