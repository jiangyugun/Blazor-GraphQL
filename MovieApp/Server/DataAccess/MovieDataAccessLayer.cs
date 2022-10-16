using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Interface;
using MovieApp.Server.Models;

namespace MovieApp.Server.DataAccess
{
    public class MovieDataAccessLayer : IMovie
    {
        readonly MovieDBContext _dBContext;

        public MovieDataAccessLayer(IDbContextFactory<MovieDBContext> dbContext)
        {
            _dBContext = dbContext.CreateDbContext();
        }

        public async Task AddMovie(Movie movie)
        {
            try
            {
                await _dBContext.Movies.AddAsync(movie);
                await _dBContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteMovie(int movieId)
        {
            try
            {
                Movie? movie = await _dBContext.Movies.FindAsync(movieId);

                if(movie is not null)
                {
                    _dBContext.Movies.Remove(movie);
                    await _dBContext.SaveChangesAsync();
                    return movie.PosterPath;
                }

                return String.Empty;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _dBContext.Movies.AsNoTracking().ToListAsync();
        }

        public async Task<List<Genre>> GetGenres()
        {
            return await _dBContext.Genres.AsNoTracking().ToListAsync();
        }

        public async Task UpdateMovie(Movie movie)
        {
            try
            {
                var result = await _dBContext.Movies.FirstOrDefaultAsync(e => e.MovieId == movie.MovieId);
                if(result is not null)
                {
                    result.Title = movie.Title;
                    result.Genre = movie.Genre;
                    result.Duration = movie.Duration;
                    result.PosterPath = movie.PosterPath;
                    result.Rating = movie.Rating;
                    result.Overview = movie.Overview;
                    result.Language = movie.Language;
                }
                await _dBContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
