using MovieApp.Server.Interface;
using MovieApp.Server.Models;

namespace MovieApp.Server.GraphQL
{
    public class MovieQueryResolver
    {
        readonly IMovie _movieService;

        public MovieQueryResolver(IMovie movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// 取得電影類型
        /// </summary>
        /// <returns></returns>
        public async Task<List<Genre>> GetGenreList()
        {
            return await _movieService.GetGenres();
        }

        /// <summary>
        /// 取得電影列表
        /// </summary>
        /// <returns></returns>
        [UseSorting]
        [UseFiltering]
        public async Task<IQueryable<Movie>> GetMovieList()
        {
            List<Movie> availableMovies = await _movieService.GetAllMovies();
            return availableMovies.AsQueryable();
        }
    }
}
