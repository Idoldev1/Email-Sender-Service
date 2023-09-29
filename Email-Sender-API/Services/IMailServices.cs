using Email_Sender_API.MailRepository;

namespace Email_Sender_API.Services;


public interface IMailServices
{
    Task SendMailAsync(MailRequest mailRequest);
}