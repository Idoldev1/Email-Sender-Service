using Email_Sender_API.MailRepository;
using Email_Sender_API.Services;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Email_Sender_API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    private readonly IMailServices _mailService;
    public MailController(IMailServices mailService)
    {
        _mailService = mailService;
    }


    [HttpPost]
    public async Task<IActionResult> SendMail([FromBody] MailRequest mailRequest)
    {
        try
        {
            await _mailService.SendMailAsync(mailRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}