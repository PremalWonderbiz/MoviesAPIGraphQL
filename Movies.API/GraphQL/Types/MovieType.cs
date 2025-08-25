using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.GraphQL.Types.Enums;
using Movies.Models;

namespace Movies.API.GraphQL.Types
{
    public class MovieType : ObjectGraphType<Movie>
    {

        public MovieType(MoviesDbContext dbContext, IDataLoaderContextAccessor dataLoader)
        {
            Field(m => m.Id).Description("The ID of the movie.");
            Field(m => m.Name).Description("The name of the movie.");
            Field(m => m.Description).Description("The description of the movie.");
            Field(m => m.LaunchDate).Description("The launch date of the movie.");
            Field<MovieGenreType>("Genre").Description("The genre of the movie");
            Field<ListGraphType<MovieReviewType>>("MovieReviews").Description("The reviews of the movie.")
                .Resolve(context =>
                {
                    if (context.Source.MovieReviews != null) 
                        return context.Source.MovieReviews;

                    var loader = dataLoader.Context
                                           .GetOrAddCollectionBatchLoader<int, MovieReview>("GetMovieReviewsByMovieId",
                                                async movieIds =>
                                                {
                                                    var reviews = await dbContext.MovieReviews.Where(r => movieIds.Contains(r.MovieId)).ToListAsync();

                                                    return reviews.ToLookup(reviews => reviews.MovieId);
                                                } 
                                           );

                    var movieId = context.Source.Id;

                    return loader.LoadAsync(movieId);
                });
        }
        
    }
}
