using GraphQL.Types;

namespace Movies.API.GraphQL.Types
{
    public class MovieReviewType : ObjectGraphType<Models.MovieReview>
    {
        public MovieReviewType()
        {
            Field(mr => mr.Id).Description("The ID of the movie review.");
            Field(mr => mr.Rate).Description("The rate given in the movie review.");
            Field(mr => mr.Comment).Description("The comment of the movie review.");
            Field(mr => mr.CreatedAt).Description("The date when the movie review was created.");
            Field(mr => mr.MovieId).Description("The ID of the movie associated with this review.");
        }
    }
}
