using System.ComponentModel.DataAnnotations;

namespace Watchlist.Models
{
    public class MovieViewModel
    {
        
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(10)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Director { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;


        [Required]
        [Range(typeof(decimal), "0.00", "10.00", ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }

    

        [Required]
        public string Genre { get; set; } = null!;   


    }
}
