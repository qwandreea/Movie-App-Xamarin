using MOVIE.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MOVIE.Models
{
    class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool Adult { get; set; }
        public double Budget { get; set; }
        public string HomePage { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }
        public string PosterPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Revenue { get; set; }
        public string Status { get; set; }
        public string TagLine { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; }
        [Ignore]
        public List<Genre> Genre { get; set; }

        public Movie(int Id, bool Adult, double Budget, string HomePage, string OriginalTitle, string Overview, double Popularity, string PosterPath, DateTime ReleaseDate, double Revenue, string Status, string TagLine, double VoteAverage, int VoteCount, List<Genre>Genre)
        {
            this.Id = Id;
            this.Adult = Adult;
            this.Budget = Budget;
            this.HomePage = HomePage;
            this.OriginalTitle = OriginalTitle;
            this.Overview = Overview;
            this.Popularity = Popularity;
            this.PosterPath = PosterPath;
            this.ReleaseDate = ReleaseDate;
            this.Revenue = Revenue;
            this.Status = Status;
            this.TagLine = TagLine;
            this.VoteAverage = VoteAverage;
            this.VoteCount = VoteCount;
            this.Genre = Genre;
        }

        public Movie() { 
        }
        public override string ToString()
        {
            string genres = "";
            if (Genre != null)
            {
                foreach (Genre g in Genre)
                {
                    genres += g.Name + ",";
                }
            }
           
            return "ID= " + Id + " Adult= " + Adult + " Budget= " + Budget + " OriginalTitle= " + OriginalTitle + " Overview = " + Overview
                + " Popularity= " + Popularity + " Releasedate= " + ReleaseDate + " Revenue= " + Revenue + " Status= " + Status +
                " Tagline= " + TagLine + " Vote average = " + VoteAverage + " Votecount= " + VoteCount + " Genres = " + genres;
        }

        [Ignore]
        public string OverviewFormat
        {
            get
            {
                return "Overview: " + Overview;
            }
        }

        [Ignore]
        public string GenreFormat
        {
            get
            {
                string genres = "Genres ";
                int i = 0;
                foreach (Genre g in Genre)
                {
                    if (i + 1 == Genre.Count) genres += g.Name;
                    else genres += g.Name + " | ";
                    i++;
                }
                return genres;
            }
        }

    }
}
