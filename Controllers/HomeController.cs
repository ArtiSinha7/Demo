using Demo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Demo.Models.SchoolModel;
namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        string baseUrl = ConfigurationManager.AppSettings["BaseApiUrl"];
        string projectToken = ConfigurationManager.AppSettings["ProjectToken"];

        // Index action
        public async Task<ActionResult> Index()
        {
            var popupBanner = await LoadPopupBanner();
            var banners = await LoadMainBanners();
            var model = new IndexView
            {
                PopupBanner = popupBanner,
                Banners = banners,
            };

            return View(model);
        }

        private async Task<SchoolModel.ProjectData> LoadPopupBanner()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("ProjectToken", projectToken);
                client.DefaultRequestHeaders.Add("Domain", "jinynjony");
                var response = await client.GetAsync("/api/PopUpBanner/get");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SchoolModel.ApiResponse>(json);
                    return result?.Data?.FirstOrDefault();
                }

                return null;
            }
        }

        private async Task<List<SchoolModel.ProjectData>> LoadMainBanners()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("ProjectToken", projectToken);
                client.DefaultRequestHeaders.Add("Domain", "jinynjony");
                var response = await client.GetAsync("/api/Banner/Get");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SchoolModel.ApiResponse>(json);
                    return result?.Data ?? new List<SchoolModel.ProjectData>();
                }

                return new List<SchoolModel.ProjectData>();
            }
        }



        [ChildActionOnly]
        public PartialViewResult Marquee()
        {
            List<SchoolModel.ProjectData> offers = new List<SchoolModel.ProjectData>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("ProjectToken", projectToken);
                client.DefaultRequestHeaders.Add("Domain", "jinynjony");
                var response = client.GetAsync("/api/Custom/Get").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<SchoolModel.ApiResponse>(json);
                    if (result != null)
                        offers = result.Data;
                }
            }

            string iconHtml = "<i class='fas fa-bolt'></i> ";
            string marqueeContent = (offers != null && offers.Count > 0)
                ? string.Join(" \u00A0\u00A0\u00A0 ", offers.Select(o => iconHtml + o.title))
                : "No offers available";

            ViewBag.MarqueeContent = marqueeContent;

            return PartialView("_Marquee");
        }


        [Route("about")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Route("contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(new Contact());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("contact")]
        public async Task<ActionResult> Contact(Contact model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill all required fields correctly.";
                return View(model);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Domain", "imsh");
                    client.DefaultRequestHeaders.Add("ProjectToken", projectToken);
                    client.DefaultRequestHeaders.Add("Domain", "jinynjony");
                    client.DefaultRequestHeaders.Add("CaptchaResponse", model.CaptchaResponse);

                    var payload = new
                    {
                        name = model.name,
                        email = model.email,
                        mobile = model.mobile,
                        whatsapp = model.whatsapp,
                        message = model.message
                    };

                    var json = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("/api/Contact/New", content);

                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.SuccessMessage = "Form submitted successfully!";
                        ModelState.Clear();
                        return View(new Contact());
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = "API Error: " + error;
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Exception occurred: " + ex.Message;
                return View(model);
            }
        }
        [Route("gallery")]
        public ActionResult Gallery()
        {
            ViewBag.Message = "Your gallery page.";

            return View();
        }
        [Route("Facility")]
        public ActionResult Services()
        {
            ViewBag.Message = "Your services page.";

            return View();
        }
        [Route("notice")]
        public ActionResult Notice()
        {
            ViewBag.Message = "Your notice page.";

            return View();
        } 
        [Route("directordesk")]
        public ActionResult DirectorDesk()
        {
            ViewBag.Message = "Your DirectorDesk page.";

            return View();
        }
    }
}