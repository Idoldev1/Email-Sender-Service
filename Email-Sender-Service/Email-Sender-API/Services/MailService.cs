using Email_Sender_API.MailRepository;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Email_Sender_API.Services;


public class MailService : IMailServices
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }



    
    public async Task SendMailAsync(MailRequest mailRequest)
    {
        var mail = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Mail)
        };
        mail.To.Add(MailboxAddress.Parse(mailRequest.RecipientMail));
        mail.Subject = mailRequest.Subject;
        var builder = new BodyBuilder();
        /*if (mailRequest.Attachments != null)
        {
            byte[] fileBytes;
            foreach (var file in mailRequest.Attachments)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));

                }
            }
        }*/
        builder.HtmlBody = mailRequest.Body;
        mail.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(mail);
        smtp.Disconnect(true);
    }
}