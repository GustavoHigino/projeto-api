using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Mail;

namespace PrimeiroProjeto.Services.Impl
{
    public class EmailServiceImpl
        : IEmailService
    {
        public EmailServiceImpl(
            ILogger<EmailServiceImpl> logger,
        EmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }
        private readonly ILogger<EmailServiceImpl> _logger;
        private readonly EmailSender _emailSender;
        

        public void SendSimpleEmail(EmailRequestDTO 
            emailRequest)
        {
            _emailSender
                .To(emailRequest.To)
                .WithSubject(emailRequest.Subject)
                .WithMessage(emailRequest.Body)
                .Send();
        }
        public async Task SendEmailWithAttachment
            (EmailRequestDTO emailRequest,
            IFormFile attachment)
        {
            if(attachment == null || attachment.Length == 0)
            {
                _logger.LogWarning("Attachment is " +
                    "null or empty");
                throw new ArgumentException("Attachment is " +
                    "null or empty");
            }
            string tempFilePath = Path.Combine
                (Path.GetTempPath(), attachment.FileName);
            try
            {
                await using (var stream = new
                    FileStream
                    (tempFilePath, FileMode.Create))
                {
                    await attachment.CopyToAsync
                        (stream);
                }
                _emailSender
                .To(emailRequest.To)
                .WithSubject(emailRequest.Subject)
                .WithMessage(emailRequest.Body)
                .Attach(tempFilePath)
                .Send();

            }
            catch (Exception ex)
            {

                _logger.LogError(ex,
                    "Error sending email with " +
                    "attachment");
                throw;
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
            
            
        }
    }
}
