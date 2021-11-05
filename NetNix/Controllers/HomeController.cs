using NetNix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NetNix.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<MovieOverviewModel> data = await _GetTop5SoonToBeReleasedMoviesAsync();

            HomeViewModel model = new HomeViewModel();
            model.movieList = data.OrderBy(x => x.releaseDate).Take(5).ToList();

            return View("~/Views/Home/Index.cshtml", model);
        }

        private async Task<List<MovieOverviewModel>> _GetTop5SoonToBeReleasedMoviesAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://virtserver.swaggerhub.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("BartvdPost/NetNix/0.2.0/soon/");

                List<MovieOverviewModel> list = new List<MovieOverviewModel>();
                if (response.IsSuccessStatusCode)
                {
                    list = await response.Content.ReadAsAsync<List<MovieOverviewModel>>();
                }
                else
                {
                    //Console.WriteLine("Internal server Error");
                }

                return list;
            }
        }
    }
}