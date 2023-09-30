using MOVIE.Models;
using MOVIE.Services;
using MOVIE.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MOVIE.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieListView : ContentPage
    {
        MovieListViewModel movieModel { get; set; }
        List<Movie> movieList = new List<Movie>();

        public MovieListView()
        {
            InitializeComponent();
            movieModel = new MovieListViewModel();
            this._movieListInit();
            BindingContext = movieModel;
        }

        public async void _movieListInit()
        {
            movieList = await new MovieService()._getMovieList();
            listViewMovies.ItemsSource = movieList;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void listViewMovies_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new MovieDetailsView((Movie)e.Item), true);
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            this._searchListFiltering();
        }

        private void _searchListFiltering()
        {
            var searchResult = movieList.Where(c => c.GenreFormat.Contains(searchBar.Text) || c.OriginalTitle.Contains(searchBar.Text));
            if (searchResult.ToList().Count > 0)
            {
                listViewMovies.ItemsSource = searchResult.ToList();
                labelResults.Text = searchResult.ToList().Count + " results found";
            }
        }

    }
}