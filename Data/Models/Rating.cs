using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Catalog.Data.Models
{
    [Table("ratings")]
    public class Rating
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("movie_id")]
        public int? MovieId { get; set; }

        [Required]
        [Range(1.0, 10.0)]
        [Column("rating_value")]
        public double RatingValue { get; set; }

        [ForeignKey("MovieId")]
        public Movie? Movie { get; set; }
    }
}