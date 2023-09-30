using MOVIE.Models;
using MOVIE.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MOVIE.ViewModels
{
    class MovieListViewModel: BindableObject
    {
        private List<Movie> _movieList;

        public List<Movie> MovieList
        {
            get
            {
                return _movieList;
            }
            set
            {
                _movieList = value;
                OnPropertyChanged();
            }
        }

        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public string OverviewFormat { get; set; }
        public string GenreFormat { get; set; }
        public List<Genre> Genres { get; set; }
        public ICommand AddCommand { get; set; }


        public DaoFavorite daoFavorite = new DaoFavorite();

        public MovieListViewModel()
        {
            _movieListInit();
            AddCommand = new Command(OnAddTapped);
        }

        public async void _movieListInit()
        {
            MovieList = await Task.Run(()=>new MovieService()._getMovieList());
        }

        public void OnAddTapped(object obj)
        {
            var movie = obj as Movie;
            int index = daoFavorite.insertToWatchlist(movie);
            
            string Message;
            if (index < 0)
            {
                Message = movie.OriginalTitle + " already added to watchlist";
            }
            else
            {
                Message = movie.OriginalTitle + " added to watchlist";
            }
            Acr.UserDialogs.UserDialogs.Instance.Toast(Message);
            MessagingCenter.Send<MovieListViewModel, object>(this, "added", movie);
        }
    }
}
