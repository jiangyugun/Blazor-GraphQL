using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Server.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Title = string.Empty;
            Overview = string.Empty;
            Genre = string.Empty;
            Language = string.Empty;
        }

        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Overview { get; set; } = null!;

        [Required]
        public string Genre { get; set; } = null!;

        [Required]
        public string Language { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "This field accepts only positive numbers.")]
        public int Duration { get; set; }

        [Required]
        [Range(0, 10.0, ErrorMessage = "The value should be less than or equal to 10.")]
        public decimal Rating { get; set; }
        public string? PosterPath { get; set; }
    }
}
