using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using SQLite;

namespace MOVIE.Models
{
    class DaoFavorite
    {
        SQLiteConnection _sqlConnection;

        public DaoFavorite()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "movie_watchlist.db");
            _sqlConnection = new SQLiteConnection(path);
            _sqlConnection.CreateTable<Movie>();
        }

        public int insertToWatchlist(Movie movie)
        {
            if (findMovieById(movie.Id)){
                return -1;
            }
            else
            {
                return _sqlConnection.InsertOrReplace(movie);
            }
        }

        public List<Movie> getAllWatchListMovie()
        {
            return _sqlConnection.Query<Movie>("SELECT * FROM Movie");
        }

        public void removeFromWatchlist(int Id)
        {
            _sqlConnection.Delete<Movie>(Id);
        }

        public bool findMovieById(int Id)
        {
            Movie movie = _sqlConnection.Table<Movie>().FirstOrDefault(p => p.Id == Id);
            if (movie == null) { return false; } else { return true; }
        }

        public void deleteAll()
        {
            _sqlConnection.Execute("DELETE FROM Movie");
        }
    }
}
