﻿using CBMS.Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CBMS.UI.Controllers
{
    public class BusStopController : Controller
    {
        private IConfiguration _configuration;
        public BusStopController(IConfiguration configuration)

        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<BusStop> busstopDetails = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "BusStop/GetBusStop";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        busstopDetails = JsonConvert.DeserializeObject<IEnumerable<BusStop>>(result);
                    }
                }
            }
            return View(busstopDetails);
        }
        public IActionResult BusStopEntry()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BusStopEntry(BusStop busstop)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(busstop), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "BusStop/AddBusStop";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Bus Stop details saved successfully!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }
                }
            }
            return View();
        }
    }
}
