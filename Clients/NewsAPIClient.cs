using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json;
using ChatbotAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ChatbotAPI.Clients
{
    public class NewsAPIClient
    {
        public string NewsAPIKey { get; set; }
        public NewsAPIClient(IOptions<AppSettings> appSettings)
        {
            NewsAPIKey = appSettings.Value.NewsAPIKey;
        }

        public async Task<List<Article>> FindNewsHighlights(WatsonPayload watsonJson)
        {
            List<Article> foundArticles = new List<Article>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(NewsAPIKey);
                using (var response = await httpClient.GetAsync("https://newsapi.org/v2/top-headlines?country=sa"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(apiResponse);
                    if (!jsonResponse["status"].ToString().Equals("ok"))
                        return null;
                    foundArticles = JsonConvert.DeserializeObject<List<Article>>(jsonResponse["articles"].ToString());
                }
            }
            return foundArticles;
        }

        public async Task<List<Article>> FindNewsKeyphrase(WatsonPayload watsonJson)
        {
            List<Article> foundArticles = new List<Article>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(NewsAPIKey);
                using (var response = await httpClient.GetAsync($"https://newsapi.org/v2/everything?q={watsonJson.Query}&language=en&sortBy=relevancy"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(apiResponse);
                    if (!jsonResponse["status"].ToString().Equals("ok"))
                        return null;
                    foundArticles = JsonConvert.DeserializeObject<List<Article>>(jsonResponse["articles"].ToString());
                }
            }
            return foundArticles;
        }
    }
}