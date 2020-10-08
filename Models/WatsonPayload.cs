namespace ChatbotAPI.Models
{
    public enum WatsonAction
    {
        News_Highlights = 0,
        News_Keyphrase = 1,
    }
    public class WatsonPayload
    {
        public int id { get; set; }
        public WatsonAction Action { get; set; }
        public string Query { get; set; }
    }
}