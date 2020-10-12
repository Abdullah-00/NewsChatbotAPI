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
        public async Task<ActionResult<WatsonResponse>> PostWatsonPayload(WatsonPayload watsonPayload)
        {
            // _context.WatsonPayload.Add(watsonPayload);
            // await _context.SaveChangesAsync();
            WatsonResponse response = new WatsonResponse();
            NewsAPIClient newsAPI = new NewsAPIClient(_appSettings);
            switch (watsonPayload.Action)
            {
                case WatsonAction.News_Highlights:
                    response.articles = await newsAPI.FindNewsHighlights(watsonPayload);
                    response.status = true;
                    break;
                case WatsonAction.News_Keyphrase:
                    response.articles = await newsAPI.FindNewsKeyphrase(watsonPayload);
                    response.status = true;
                    break;
                default:
                    response.status = false;
                    break;
            }
            return response;
        }

        private bool WatsonPayloadExists(int id)
        {
            return _context.WatsonPayload.Any(e => e.id == id);
        }
    }
}
