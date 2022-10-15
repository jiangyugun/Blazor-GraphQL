using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Models;

namespace MovieApp.Server.DataAccess
{
    public class MovieDataAccessLayer:Movie
    {
        readonly MovieDBContext _dBContext;

        public MovieDataAccessLayer(IDbContextFactory<MovieDBContext> dbContext)
        {
            _dBContext = dbContext.CreateDbContext();
        }

        public async Task<List<Genre>> GetGenres()
        {
            return await _dBContext.Genres.AsNoTracking().ToListAsync();
        }
    }
}
