using Microsoft.AspNetCore.DataProtection.KeyManagement;
using pruebaEVERTEC.Models;
using System.Net.Http;

namespace pruebaEVERTEC.Servicios
{
    public interface IService
    {
        Task<List<Movie>> GetMoviesAsync();
        Task<List<Movie>> SearchMoviesAsync(string searchTerm);
    }
}
