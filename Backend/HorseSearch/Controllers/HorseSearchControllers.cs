using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HorseSearchApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private static readonly List<string> TrackSlugs = new()
        {
            "bergen-travpark", "biri-travbane", "bjerke-travbane", "forus-travbane",
            "harstad-travpark", "jarlsberg-travbane", "klosterskogen-travbane", "varig-orkla-arena",
            "momarken-travbane", "sorlandets-travpark", "ovrevoll-galopp", "bodo-travbane",
            "nossum-travbane", "kala-travpark", "lofoten-travpark", "magnor-travbane",
            "mo-travpark", "skoglund-travbane", "kongsvinger-travbane"
        };

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string horseName)
        {
            if (string.IsNullOrWhiteSpace(horseName))
                return BadRequest("Horse name is required.");

            var results = new List<object>();
            var today = DateTime.Today;

            using var httpClient = new HttpClient();

            for (int i = 0; i <= 7; i++)
            {
                var date = today.AddDays(i);
                var dateStr = date.ToString("yyyy-MM-dd");

                foreach (var slug in TrackSlugs)
                {
                    var url = $"https://www.travsport.no/travbaner/{slug}/startlist/{dateStr}";

                    try
                    {
                        var html = await httpClient.GetStringAsync(url);
                        var doc = new HtmlDocument();
                        doc.LoadHtml(html);

                        var horseLinks = doc.DocumentNode.SelectNodes("//table//tr/th/a");

                        if (horseLinks != null)
                        {
                            foreach (var link in horseLinks)
                            {
                                if (link.InnerText.Trim().Equals(horseName, StringComparison.OrdinalIgnoreCase))
                                {
                                    results.Add(new
                                    {
                                        date = dateStr,
                                        track = ToTitleCase(slug.Replace("-", " ")),
                                        url
                                    });
                                }
                            }
                        }
                    }
                    catch
                    {
                        continue; // silently ignore failed fetches
                    }
                }
            }

            return Ok(results);
        }

        private static string ToTitleCase(string input)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }
    }
}
