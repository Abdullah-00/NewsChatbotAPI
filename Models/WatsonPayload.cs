namespace ChatbotAPI.Models
{
    public enum Action
    {
        News_Highlights,
        News_Keyphrase,
        News_Category
    }
    public class WatsonPayload
    {
        public Action Action { get; set; }
        public string Query { get; set; }
    }
}