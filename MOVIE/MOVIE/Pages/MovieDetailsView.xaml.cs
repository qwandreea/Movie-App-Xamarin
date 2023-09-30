using MOVIE.Models;
using MOVIE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MOVIE.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailsView : ContentPage
    {
        Movie movie;
        MovieDetailsViewModel movieDetailsView;

        public MovieDetailsView(object item)
        {
            InitializeComponent();
            movie = (Movie)item;
            movieDetailsView = new MovieDetailsViewModel(movie);
            BindingContext = movieDetailsView;
            this._labelClicked();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new TabbedMainPage(), true);
        }

        void _labelClicked()
        {
            labelHomepage.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                CommandParameter = movie.HomePage,
                Command = new Command<string>((url) =>
                {
                    try
                    {
                        Device.OpenUri(new System.Uri(url));
                    }
                    catch(Exception e)
                    {
                        throw;
                    }
                })
            });
        }
    }
}