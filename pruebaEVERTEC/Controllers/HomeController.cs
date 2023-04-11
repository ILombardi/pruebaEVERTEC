using Microsoft.AspNetCore.Mvc;
using pruebaEVERTEC.Models;
using pruebaEVERTEC.Servicios;
using System.Diagnostics;

namespace pruebaEVERTEC.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IService _servicio;
      
        private readonly HttpClient httpClient;
        public HomeController( Service omdb, IService servicio, HttpClient httpClient)
        {
            
          
            _servicio = servicio;
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> Index(string title)
        {
            var movie = await _servicio.GetMoviesAsync();
            return View(movie);
        }

        public async Task<IActionResult> BuscarPorTitulo(string title)
        {     
            var movie = await _servicio.SearchMoviesAsync(title);
            if(movie == null) { return NotFound(); }
            return View(movie);
            
        }

        public IActionResult Privacy()
        {            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}