using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MessageService.Server.Attributes;
using MessageService.ViewModel;
using MessageService.IService;

namespace MessageService.Server.Controllers
{
    [ApiController]
    [RouteController]
    public class MessagesController : ControllerBase
    {
        public IMessageService MessageService { get; set; }
        [HttpPost("CanSend")]
        public async Task<IActionResult> CanSend([FromBody] MessageRequestViewModel messageViewModel)
        {
            var responseViewModel = await MessageService.CanSend(messageViewModel); 
                       
            return Ok(responseViewModel);
        }
    }
}
