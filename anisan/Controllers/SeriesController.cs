using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using anisan.Models;
using Newtonsoft.Json.Linq;

namespace anisan.Controllers
{
    public class SeriesController : Controller
    {
        private readonly List<Series> _series = new List<Series>();
        private readonly string url = "https://kitsu.io/api/edge/anime";
        private HttpClient client = new HttpClient();
        public SeriesController()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
        }
        // GET: /series/
        public async Task<ViewResult> Index()
        {

            string response = await GetAnime();
            JObject res = JObject.Parse(response);
            foreach (var r in res["data"])
            {
                var attr = r["attributes"];
                Series x = new Series();
                x.Id = Int32.Parse(r["id"].ToString());
                x.SeriesType = r["type"].ToString();
                if (attr["titles"]["en"] != null)
                {
                    x.Title = attr["titles"]["en"].ToString();
                } else if (attr["titles"]["en_jp"] != null)
                {
                    x.Title = attr["titles"]["en_jp"].ToString();
                }
                x.Status = attr["status"].ToString();
                x.PosterImage = attr["posterImage"]["medium"].ToString();
                x.Synopsis = attr["synopsis"].ToString();
                x.StartDate = attr["startDate"].ToString();
                x.EndDate = attr["endDate"].ToString();

                _series.Add(x);
            }
            ViewData["Message"] = "Your series page2.";
            return View(_series);
        }

        private async Task<String> GetAnime()
        {
            var result = await client.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var res = await result.Content.ReadAsStringAsync();
                        
                return res;
            }
            else
            {
                return null;
            }
        }
    }
 }
