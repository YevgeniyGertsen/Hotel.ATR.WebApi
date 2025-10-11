using Hotel.Atr.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Hotel.Atr.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var accessToken = GenerateJSONToken();

            using (var httpClient  = new HttpClient() )
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                using (var responce = await httpClient.GetAsync("http://localhost:5279/get-all-teams"))
                {
                    if(responce.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponce = await responce.Content.ReadAsStringAsync();    
                    }
                }


            }//Dispose()


            return View();
        }

        private string GenerateJSONToken()
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("MySuperDuperMegaSecretKey_2025_Token!"));
            var credentioal = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://ok.kz",
                audience: "http://ok.kz",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentioal);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
