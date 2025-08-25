using GraphQL.Types;
using Movies.API.GraphQL.Types.Enums;
using Movies.Models;

namespace Movies.API.GraphQL.Types.Inputs
{
    public class MovieInputType : InputObjectGraphType<Movie>
    {
        public MovieInputType()
        {
            Field(m => m.Name).Description("The name of the movie.");
            Field(m => m.Description).Description("The description of the movie.");
            Field(m => m.LaunchDate).Description("The launch date of the movie.");
            Field<MovieGenreType>("Genre").Description("The genre of the movie");
        }
    }
}
