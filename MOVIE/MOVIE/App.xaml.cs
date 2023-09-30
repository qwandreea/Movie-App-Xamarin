using MOVIE.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MOVIE
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TabbedMainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
