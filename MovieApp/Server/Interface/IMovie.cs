using MovieApp.Server.Models;

namespace MovieApp.Server.Interface
{
    public interface IMovie
    {
        Task<List<Genre>> GetGenres();
    }
}
