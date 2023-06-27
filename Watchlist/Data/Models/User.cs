using Microsoft.AspNetCore.Identity;

namespace Watchlist.Data.Models
{
    public class User : IdentityUser
    {

        public virtual List<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();


    }
}
