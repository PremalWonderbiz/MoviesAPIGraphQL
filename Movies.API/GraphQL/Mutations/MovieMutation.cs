using GraphQL;
using GraphQL.Types;
using Movies.API.Data;
using Movies.API.GraphQL.Types;
using Movies.API.GraphQL.Types.Inputs;

namespace Movies.API.GraphQL.Mutations
{
    public class MovieMutation : ObjectGraphType
    {
        public MovieMutation(MoviesDbContext dbContext)
        {
            Field<MovieType>("addMovie").Arguments(
                new QueryArgument<NonNullGraphType<MovieInputType>>()
                {
                    Name = "movie",
                    Description = "The input movie to add"
                }
            ).ResolveAsync( async context =>
            {
                var movieInput = context.GetArgument<Movies.Models.Movie>("movie");
                dbContext.Movies.Add(movieInput);
                await dbContext.SaveChangesAsync();
                return movieInput;
            });

            Field<MovieType>("updateMovie").Arguments(
                new QueryArgument<NonNullGraphType<IdGraphType>>()
                {
                    Name = "id",
                    Description = "The ID of the movie to update"
                },
                new QueryArgument<NonNullGraphType<MovieInputType>>()
                {
                    Name = "movie",
                    Description = "The input movie to add"
                }
            ).ResolveAsync(async context =>
            {
                var id = context.GetArgument<int>("id");
                var movieInput = context.GetArgument<Movies.Models.Movie>("movie");
                movieInput.Id = id;
                dbContext.Movies.Update(movieInput);
                await dbContext.SaveChangesAsync();
                return movieInput;
            });  

            Field<BooleanGraphType>("deleteMovie").Arguments(
                new QueryArgument<NonNullGraphType<IdGraphType>>()
                {
                    Name = "id",
                    Description = "The ID of the movie to update"
                }
            ).ResolveAsync(async context =>
            {
                var id = context.GetArgument<int>("id");
                var movie = await dbContext.Movies.FindAsync(id);
                if(movie is not null)
                {
                    dbContext.Movies.Remove(movie);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            });  
        }
    }
}
