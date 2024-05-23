using ConsumeMVCFromAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumeMVCFromAPI.Controllers
{
    public class ProductController : Controller
    {
        Uri bassAddress = new Uri("https://localhost:7269/api");
        private readonly HttpClient _client;
        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = bassAddress;
        }
        public IActionResult Index()
        {
            List<ProductViewModel> VMProducts = new List<ProductViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Product/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string date=response.Content.ReadAsStringAsync().Result;
                VMProducts=JsonConvert.DeserializeObject<List<ProductViewModel>>(date);
            }
            return View(VMProducts);
        }
    }
}
