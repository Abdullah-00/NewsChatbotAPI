using System.Collections.Generic;

namespace ChatbotAPI.Models
{
    public class WatsonResponse
    {
        public bool status { get; set; }
        public List<Article> articles { get; set; }
    }
}