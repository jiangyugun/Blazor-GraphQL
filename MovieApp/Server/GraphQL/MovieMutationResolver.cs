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

        /// <summary>
        /// 編輯電影資料
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public async Task<AddMoviePayload> EditMovie(Movie movie)
        {
            bool IsBase64String = CheckBase64String(movie.PosterPath);

            if (IsBase64String)
            {
                string fileName = Guid.NewGuid() + ".jpg";
                string fullPath = System.IO.Path.Combine(posterFolderPath, fileName);

                byte[] imageBytes = Convert.FromBase64String(movie.PosterPath);
                File.WriteAllBytes(fullPath, imageBytes);

                movie.PosterPath = fileName;
            }

            await _movieService.UpdateMovie(movie);

            return new AddMoviePayload(movie);
        }

        /// <summary>
        /// 依據電影ID刪除資料
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public async Task<int> DeleteMovie(int movieId)
        {
            string converFileName = await _movieService.DeleteMovie(movieId);
            if(!string.IsNullOrEmpty(converFileName) && converFileName != _config["DefaultPoster"])
            {
                string fullPath = System.IO.Path.Combine(posterFolderPath, converFileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            return movieId;
        }

        static bool CheckBase64String(string base64)
        {
            Span<byte> buffer = new(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    }
}
