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
    }
}
