using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

using LinkShortener.Entities;
using LinkShortener.Services;

namespace LinkShortener.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly string _domainName;
        private string _userId =>
            HttpContext.Session.GetString("UserId");
        
        public MainController(ILinkService linkService,
            IConfiguration configuration)
        {
            _linkService = linkService;
            _domainName = configuration.GetValue<string>("DomainName");
        }
        
        [HttpGet("api/shorten")]
        public async Task<string> GetShortLink(string fullLink)
        {
            var shortAlias = await _linkService.GetShortAlias(fullLink, _userId);
            return $"{_domainName}/{shortAlias}";
        }

        [HttpGet("api/getAll")]
        public async Task<IEnumerable<Link>> GetAllLinks()
        {
            var links = await _linkService.GetAllLinks(_userId);

            return links;
        }

        [HttpGet("/{shortAlias}")]
        public async Task<IActionResult> RedirectFromShort(string shortAlias)
        {
            var fullLink = await _linkService.GetFullLinkAndIncreaseVisitCount(shortAlias);

            if (fullLink == null)
            {
                return NotFound($"Not found link by alias '{shortAlias}'");
            }

            return Redirect(fullLink);
        }
    }
}