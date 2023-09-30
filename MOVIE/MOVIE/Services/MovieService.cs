using MOVIE.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOVIE.Services
{
    class MovieService
    {
        private const string apiKey = "8f971e8198e3b185d19f8a1443170b4a";
        private const string movieDetailsUrl_Base = "https://api.themoviedb.org/3/movie/";
        private const string movieUrl_Base = "https://api.themoviedb.org/3/discover/movie?api_key=";
        private const string apiConfigUrl = "https://api.themoviedb.org/3/configuration?api_key=";
        private HttpClient httpClient;

        public async Task<string> _getAPIConfig()
        {
            httpClient = new HttpClient();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiConfigUrl + apiKey);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var jobject = (JObject)JsonConvert.DeserializeObject(responseBody);
               return (string)(JValue)jobject["images"]["base_url"] + (string)(JValue)jobject["images"]["logo_sizes"][5];
            }
            catch(Exception){throw;}
           
        }

        public async Task<List<Movie>> _getMovieList()
        {
            httpClient = new HttpClient();
           
            try {
                HttpResponseMessage response = await httpClient.GetAsync(movieUrl_Base + apiKey);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<int> idS = new List<int>();
                var jobject = (JObject)JsonConvert.DeserializeObject(responseBody);

                foreach (JObject o in jobject["results"])
                {
                    idS.Add((int)o["id"]);
                }

                string base_url = await this._getAPIConfig();
                List<Movie> movieList = new List<Movie>();
                foreach (int id in idS)
                {
                    response = await httpClient.GetAsync(movieDetailsUrl_Base + id + "?api_key=" + apiKey + "&language=en-US");
                    string responseBodyM = await response.Content.ReadAsStringAsync();
                    var jobjectM = (JObject)JsonConvert.DeserializeObject(responseBodyM);
                   
                    List<Genre> genreList = new List<Genre>();
                    int ID = (int)(JValue)jobjectM["id"];
                    bool Adult = (bool)(JValue)jobjectM["adult"];
                    double Buget = (double)(JValue)jobjectM["budget"];
                    string HomePage = (string)(JValue)jobjectM["homepage"];
                    string OriginalTitle = (string)(JValue)jobjectM["original_title"];
                    string Overview = (string)(JValue)jobjectM["overview"];
                    double Popularity = (double)(JValue)jobjectM["popularity"];
                    string PosterPath = base_url + (string)(JValue)jobjectM["poster_path"];
                    DateTime ReleaseDate = (DateTime)(JValue)jobjectM["release_date"];
                    double Revenue = (double)(JValue)jobjectM["revenue"];
                    string Status = (string)(JValue)jobjectM["status"];
                    string TagLine = (string)(JValue)jobjectM["tagline"];
                    double VoteAverage = (double)(JValue)jobjectM["vote_average"];
                    int VoteCount = (int)(JValue)jobjectM["vote_count"]; 
                    JArray Genres = (JArray)jobjectM["genres"];
                    foreach (JObject j in Genres)
                    {

                        Genre genre = new Genre((int)j["id"], (string)j["name"]);
                        genreList.Add(genre);
                    }
                    Movie movie = new Movie(ID,Adult,Buget,HomePage,OriginalTitle,Overview,Popularity,PosterPath,ReleaseDate,Revenue,Status,TagLine,VoteAverage,VoteCount,genreList);
                    movieList.Add(movie);
                }
                return movieList;
            } catch (Exception) { throw; }
        }
    }
}
