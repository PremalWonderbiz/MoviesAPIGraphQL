using Movies.GraphQL.Client.Services;

var service = new MovieService();

var movie = await service.GetMovie(52);

Console.WriteLine($"Movie : {movie.Name}");
Console.ReadKey();