using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Catalog.Data.Models
{
    [Table("movies")]
    public class Movie
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("release_year")]
        public int? ReleaseYear { get; set; }

        [ForeignKey("Genre")]
        [Column("genre_id")]
        public int? GenreId { get; set; }

        public Genre? Genre { get; set; }

        [ForeignKey("Director")]
        [Column("director_id")]
        public int? DirectorId { get; set; }

        public Director? Director { get; set; }

        public ICollection<Rating>? Ratings { get; set; }

        [Column("rating")]
        public double Rating { get; set; }

    }
}