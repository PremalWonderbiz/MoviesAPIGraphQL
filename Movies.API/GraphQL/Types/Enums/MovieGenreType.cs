using GraphQL.Types;
using Movies.Models.Enums;

namespace Movies.API.GraphQL.Types.Enums
{
    public class MovieGenreType : EnumerationGraphType<MovieGenre>
    {
        public MovieGenreType()
        {
            Name = "Genre";
            Description = "The genre of the movie. Possible values: Comedy, Action, Romance, Fantasy, Horror.";
        }
    }
}
