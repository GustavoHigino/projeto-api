using PrimeiroProjeto.Data.DTO.V1;

namespace PrimeiroProjeto.Services
{
    public interface IEmailService
    {
        void SendSimpleEmail(EmailRequestDTO
            emailRequest);
        Task SendEmailWithAttachment
            (EmailRequestDTO emailRequest,
            IFormFile attachment);
    }
}
