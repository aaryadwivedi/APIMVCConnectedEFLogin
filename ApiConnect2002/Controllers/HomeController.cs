using ApiConnect2002.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace ApiConnect2002.Controllers
{
    //[Route("/[controller]")]
    public class HomeController : Controller
    {
        private readonly IConfiguration config;
        private readonly string baseUrl;
        public HomeController(IConfiguration configuration)
        {
            this.config = configuration;
            baseUrl = config["ApiBaseUrl"];
        }
        [Route("/login")]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        //[HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginMaster user)
        {
            string? token;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(this.baseUrl + "api/Login/login/",content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        token = await response.Content.ReadAsStringAsync();
                        //token = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(apiresponse);
                        TempData["message"] = $"User logged in successfully";
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}