using NetNix.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NetNix.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public async Task<ActionResult> Index(string id, string directorId)
        {
            MovieDetailsModel model = await _GetMovieDetails(id);
            model.director.id = directorId;

            return View("~/Views/Movie/Index.cshtml", model);
        }

        public async Task LikeMovie(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://virtserver.swaggerhub.com/");
                var likeModel = new LikePostModel() { username = "Katharina Rabanus", movieId = id };
                HttpResponseMessage response = await client.PostAsJsonAsync("BartvdPost/NetNix/0.2.0/like", likeModel);
            }

            return;
        }

        private async Task<MovieDetailsModel> _GetMovieDetails(string id)
        {
            MovieDetailsModel model = new MovieDetailsModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://virtserver.swaggerhub.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("BartvdPost/NetNix/0.2.0/movie/" + id);

                if (response.IsSuccessStatusCode)
                {
                    model = await response.Content.ReadAsAsync<MovieDetailsModel>();

                    string result = await response.Content.ReadAsStringAsync();
                    model.image = _GetImageValue(result);
                }

            }

            return model;
        }
        private string _GetImageValue(string result)
        {
            int indexImageStart = result.IndexOf("image");

            int indexImageEnd = _GetNextIndexQuotationMark(result, indexImageStart);
            int indexImageValueStart = _GetNextIndexQuotationMark(result, indexImageEnd);
            int indexImageValueEnd = _GetNextIndexQuotationMark(result, indexImageValueStart) - 1;

            result = result.Remove(indexImageValueEnd);
            result = result.Remove(0, indexImageValueStart);

            return result;
        }

        private int _GetNextIndexQuotationMark(string result, int indexStart)
        {
            char quotationMarks = '"';
            bool quotationMarkFound = false;
            int index = indexStart;

            while (!quotationMarkFound && index < result.Length)
            {
                if (result[index] == quotationMarks)
                    quotationMarkFound = true;
                index++;
            }

            return index;
        }
    }
}
