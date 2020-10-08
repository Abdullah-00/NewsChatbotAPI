using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatbotAPI.Data;
using ChatbotAPI.Models;
using ChatbotAPI.Clients;
using Microsoft.Extensions.Options;

namespace ChatbotAPI.Controllers
{
    [Route("api/chatbot")]
    [ApiController]
    public class WatsonController : ControllerBase
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ChatbotContext _context;

        public WatsonController(ChatbotContext context, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
            _context = context;
        }

        // POST: api/Watson
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<List<Article>>> PostWatsonPayload(WatsonPayload watsonPayload)
        {
            // _context.WatsonPayload.Add(watsonPayload);
            // await _context.SaveChangesAsync();
            NewsAPIClient newsAPI = new NewsAPIClient(_appSettings);
            var articles = new List<Article>();
            switch (watsonPayload.Action)
            {
                case WatsonAction.News_Highlights:
                    articles = await newsAPI.FindNewsHighlights(watsonPayload);
                    break;
                case WatsonAction.News_Keyphrase:
                    return await newsAPI.FindNewsKeyphrase(watsonPayload);
                    break;
                default:
                    return NotFound();
            }
            if (articles != null)
                return articles;
            return NotFound();
        }

        private bool WatsonPayloadExists(int id)
        {
            return _context.WatsonPayload.Any(e => e.id == id);
        }
    }
}
