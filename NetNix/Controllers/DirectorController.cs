using NetNix.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NetNix.Controllers
{
    public class DirectorController : Controller
    {
        public async Task<ActionResult> Index(string id)
        {
            DirectorDetailsModel model = await _GetDirectorDetails(id);

            return View("~/Views/Director/Index.cshtml", model);
        }

        private async Task<DirectorDetailsModel> _GetDirectorDetails(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://virtserver.swaggerhub.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("BartvdPost/NetNix/0.2.0/director/" + id);

                DirectorDetailsModel model = new DirectorDetailsModel();
                if (response.IsSuccessStatusCode)
                {
                    model = await response.Content.ReadAsAsync<DirectorDetailsModel>();
                }
                else
                {
                    //Console.WriteLine("Internal server Error");
                }

                return model;
            }
        }
    }
}