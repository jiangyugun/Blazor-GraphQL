using MovieApp.Server.Interface;
using MovieApp.Server.Models;

namespace MovieApp.Server.GraphQL
{
    public class MovieMutationResolver
    {
        public record AddMoviePayload(Movie Movie);

        readonly IWebHostEnvironment _hostingEnironment;
        readonly IMovie _movieService;
        readonly IConfiguration _config;
        readonly string posterFolderPath = string.Empty;

        public MovieMutationResolver(IWebHostEnvironment hostingEnironment, IMovie movieService, IConfiguration config)
        {
            _hostingEnironment = hostingEnironment;
            _movieService = movieService;
            _config = config;
            posterFolderPath = System.IO.Path.Combine(_hostingEnironment.ContentRootPath, "Poster");
        }

        /// <summary>
        /// 新增電影資料
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public AddMoviePayload AddMovie(Movie movie)
        {
            if (!string.IsNullOrEmpty(movie.PosterPath))
            {
                string fileName = Guid.NewGuid() + ".jpg";
                string fullPath = System.IO.Path.Combine(posterFolderPath, fileName);

                byte[] imageByte = Convert.FromBase64String(movie.PosterPath);
                File.WriteAllBytes(fullPath, imageByte);
                movie.PosterPath = fileName;
            }
            else
            {
                movie.PosterPath = _config["DefaultPoster"];
            }
            _movieService.AddMovie(movie);
            return new AddMoviePayload(movie);
        }
    }
}
