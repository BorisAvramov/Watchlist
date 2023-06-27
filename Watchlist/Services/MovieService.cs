using Microsoft.EntityFrameworkCore;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Models;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;

        public MovieService(WatchlistDbContext _context)
        {
            this.context = _context; 
        }

        public async Task AddMovieAsync(AddMovieViewModel model)
        {
            var entity = new Movie()
            {
               Director= model.Director,
               GenreId= model.GenreId,
               ImageUrl= model.ImageUrl,
               Title=model.Title,
               Rating=model.Rating,
               

            };

            await context.Movies.AddAsync(entity);

            await context.SaveChangesAsync();
        }

        public async Task AddMovieToCollectionAsunc(int movieId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                throw new ArgumentException("Invalid Movie ID");
            }

            if (!user.UsersMovies.Any(um => um.MovieId == movieId))
            {
                user.UsersMovies.Add(new UserMovie
                {
                    MovieId = movie.Id,
                    UserId = user.Id,
                    Movie = movie,
                    User = user
                });

                await context.SaveChangesAsync();

            }


        }

        public async Task<IEnumerable<MovieViewModel>> GetAllAsync()
        {
            return await context.Movies
                .Select(m => new MovieViewModel
                {
                    Id = m.Id,
                    Director= m.Director,
                    Title= m.Title,
                    Genre = m.Genre.Name,
                    Rating = m.Rating,
                    ImageUrl = m.ImageUrl


                })
                .ToListAsync();

        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await context.Genres.ToListAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetWatchedAsync(string userId)
        {
            var user = await context.Users
                .Where (u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .ThenInclude(um => um.Movie)
                .ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid User ID");

            }

            return user.UsersMovies
                .Select(m => new MovieViewModel()
                {
                    Director = m.Movie.Director,
                    Title = m.Movie.Title,
                    Genre = m.Movie.Genre.Name,
                    Id = m.MovieId,
                    ImageUrl = m.Movie.ImageUrl,
                    Rating= m.Movie.Rating,
                });



        }

        public async Task RemoveMovieFromCollectionAsync(int movieId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");

            }

            var movie = user.UsersMovies.FirstOrDefault(um => um.MovieId == movieId);

            if (movie != null)
            {
                user.UsersMovies.Remove(movie);
                await context.SaveChangesAsync();

            }





        }
    }
}
