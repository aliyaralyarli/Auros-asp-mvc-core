using Auros.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auros.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        readonly IMediator mediator;
        public CardController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{PageIndex}/{PageSize}")]
        public async Task<IActionResult> Get(DashboardController query)
        {
            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}
