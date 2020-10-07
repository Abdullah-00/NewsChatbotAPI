using Microsoft.EntityFrameworkCore;
using ChatbotAPI.Models;

namespace ChatbotAPI.Data
{
    public class ChatbotContext : DbContext
    {
        public ChatbotContext (DbContextOptions<ChatbotContext> options)
            : base(options)
        {
        }

        public DbSet<WatsonPayload> WatsonPayload { get; set; }
        public DbSet<Article> Article { get; set; }
    }
}