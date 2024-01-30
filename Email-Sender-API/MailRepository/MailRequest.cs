namespace Email_Sender_API.MailRepository;


public class MailRequest
{
    public string RecipientMail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    //public List<IFormFile> Attachments { get; set; }
}