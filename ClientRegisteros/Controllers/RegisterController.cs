using ClientRegisteros.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientRegisteros.Controllers
{
    public class RegisterController : Controller
    {
        private readonly HttpClient _httpClient;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:55681/api");
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Register/getRegisters");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var registers = JsonConvert.DeserializeObject<IEnumerable<RegisterModel>>(content);
                return View("Formulario",registers);
            }
            return View(new List<RegisterModel>());
        }

    }
}
