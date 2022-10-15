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

        public async Task<List<Genre>> GetGenres()
        {
            return await _dBContext.Genres.AsNoTracking().ToListAsync();
        }
    }
}
