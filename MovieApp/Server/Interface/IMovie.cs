using MovieApp.Server.Models;

namespace MovieApp.Server.Interface
{
    public interface IMovie
    {
        Task<List<Movie>> GetAllMovies();

        Task<List<Genre>> GetGenres();

        Task AddMovie(Movie movie);

        Task UpdateMovie (Movie movie);

        Task<string> DeleteMovie(int movieId);
    }
}
