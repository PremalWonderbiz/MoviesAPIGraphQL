using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace Movies.GraphQL.Client
{
    public class GraphQLClient
    {
        private string EndPoint = "https://localhost:7250/graphql";
        protected readonly GraphQLHttpClient Client;
        public GraphQLClient()
        {
            Client = new GraphQLHttpClient(EndPoint, new SystemTextJsonSerializer());
        }
    }
}
