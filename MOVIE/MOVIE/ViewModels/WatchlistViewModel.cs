using MOVIE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MOVIE.ViewModels
{
    class WatchlistViewModel: BindableObject
    {
        private ObservableCollection<Movie> _watchList;

        public ObservableCollection<Movie> watchList
        {
            get
            {
                return _watchList;
            }
            set
            {
                _watchList = value;
                OnPropertyChanged();
            }
        }
        public string OriginalTitle { get; set; }
        public string PosterPath { get; set; }
        public string ReleaseDate{get;set;}
        public string Status { get; set; }

        DaoFavorite daoFavorite = new DaoFavorite();

        public ICommand DeleteCommand { get; set; }

        public WatchlistViewModel()
        {
            List<Movie> list = daoFavorite.getAllWatchListMovie();
            watchList = new ObservableCollection<Movie>(list);
            DeleteCommand = new Command(OnDeleteTapped);
        }
     
        public void OnDeleteTapped(object obj)
        {
            var content = obj as Movie;
            Application.Current.MainPage.DisplayAlert("Delete", "Are you sure you want to delete from watchlist?", "Yes", "No").ContinueWith(tsk =>
            {
                if (tsk.Result == true)
                {
                    Application.Current.Dispatcher.BeginInvokeOnMainThread((Action)(() => daoFavorite.removeFromWatchlist(content.Id)));
                    Application.Current.Dispatcher.BeginInvokeOnMainThread((Action)(() => watchList.Remove(content)));
                }
            });
        }
    }
}
