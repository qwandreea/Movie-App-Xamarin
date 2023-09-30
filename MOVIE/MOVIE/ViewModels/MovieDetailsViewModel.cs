using MOVIE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOVIE.ViewModels
{
    class MovieDetailsViewModel
    {
        public Movie movie {get; set;}

        public MovieDetailsViewModel(Movie movie) {
            this.movie = movie;
        }
    }
}
