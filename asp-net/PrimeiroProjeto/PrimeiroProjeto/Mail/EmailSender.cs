using MimeKit;
using PrimeiroProjeto.Mail.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace PrimeiroProjeto.Mail
{
    public class EmailSender
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailSender> _logger;
        private string _to;
        private string _subject;
        private string _body;
        private readonly List<MailboxAddress>
            _recepients = new();
        private string? _attachment;

        public EmailSender(EmailSettings settings,
            ILogger<EmailSender> logger)
        {
            _settings = settings;
            _logger = logger;
        }
        public EmailSender To(string to)
        {
            _to = to;
            _recepients.Clear();
            _recepients.AddRange
                (ParseRecipients(to));
            return this;
        }
        public EmailSender WithSubject(string subject)
        {
            _subject = subject;
            return this;
        }
        public EmailSender WithMessage(string body)
        {
            _body = body;
            return this;
        }
        public EmailSender Attach
            (string filePath)
        {
            if (File.Exists(filePath))
            {

                _attachment = filePath;
                return this;
            }
            else
            {
                _logger.LogWarning($"attachment file" +
                    $" not found {filePath}");
            }
            return this;
        }
        public void Send()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress
                (_settings.From,
                _settings.Username));
            message.To.AddRange(_recepients);
            message.Subject = _subject ?? 
                _settings.Subject?? "No subject";

            var builder = new BodyBuilder
            {
                TextBody=_body?? _settings.Message ??
                ""
            };
            if(!string.IsNullOrWhiteSpace
                (_attachment))
            {
                var fileName = Path
                    .GetFileName(_attachment);
                builder.Attachments
                    .Add(fileName,File
                    .ReadAllBytes(_attachment));
            }
            message.Body = builder.ToMessageBody();
            try
            {
                using var client = new
                    SmtpClient();
                client.Connect(_settings.Host,
                    _settings.Port,
                    _settings.Ssl?SecureSocketOptions
                    .StartTls : SecureSocketOptions.None);
                client.Authenticate(_settings.Username,
                    _settings.Password);
                client.Send(message);
                client.Disconnect(true);
                _logger.LogInformation($"Email succ" +
                    $"essfully sent to {string.Join(
                    ";",_recepients)}");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex,
                    $"Failed to send e-mail" +
                    $"to {string.Join(";"
                    , _recepients)}");
                throw;
            }
            finally
            {
                Reset();
            }

        }


        private IEnumerable<MailboxAddress> ParseRecipients(string to)
        {
            var tosWithoutSpaces = to.Replace
                (" ",string.Empty);
            var recepients = tosWithoutSpaces
                .Split(';',
                StringSplitOptions
                .RemoveEmptyEntries);
            var list = new 
                List<MailboxAddress>();
            foreach (var address in recepients)
            {
                try
                {
                    var mailbox = MailboxAddress
                        .Parse(address);
                    list.Add(mailbox);
                }
                catch (Exception ex)
                {

                    _logger.LogWarning(ex,
                        $"Invalid e-mail address:" +
                        $" {address}");
                }
            }
            return list;
        }
        private void Reset()
        {
            _to = null;
            _subject = null;
            _body = null;
            _recepients.Clear();
            _attachment = null;

        }
    }
}
