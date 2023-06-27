using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Watchlist.Data.Models
{
    public class UserMovie
    {
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        public Movie Movie { get; set; } = null!;

    }
}
