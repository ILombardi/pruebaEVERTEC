using System.Net.Http.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pruebaEVERTEC.Models;

namespace pruebaEVERTEC.Servicios
{
    public class Service: IService
    {
        private readonly HttpClient httpClient;
        private readonly string baseUrl;// = "http://www.omdbapi.com/";
        private readonly string apiKey; //= "2dadd9fb";


        public Service(HttpClient httpClient)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            apiKey = builder.GetSection("ApiSetting:ApiKey").Value;
            baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(baseUrl);
            //Obtenemos el archivo appsetting
           
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            var response = await httpClient.GetFromJsonAsync<SearchResponse>($"?apikey={apiKey}&s=full");
            
            return response.Search.Select(m => new Movie { Title = m.Title, Poster = m.Poster, Year = m.Year, imdbID = m.imdbID }).ToList();
          

        }

        public async Task<List<Movie>> SearchMoviesAsync(string searchTerm)
        {
            var response = await httpClient.GetFromJsonAsync<SearchResponse>($"?apikey={apiKey}&s={searchTerm}");
            return response.Search.Select(m => new Movie { Title = m.Title, Poster = m.Poster, Year = m.Year, imdbID = m.imdbID }).ToList();                                       
            
        }
    }

}

public class SearchResponse
{
    public List<SearchResult>? Search { get; set; }
    public int totalResults { get; set; }
    public string? message { get; set; }
}

public class SearchResult
{
    public string? Title { get; set; }
    public string? Year { get; set; }
    public string? imdbID { get; set; }
    public string? Poster { get; set; }
}