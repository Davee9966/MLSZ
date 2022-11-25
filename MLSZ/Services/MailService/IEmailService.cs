using MLSZ.Entities;
using MLSZ.Models;

namespace MLSZ.Services.MailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(User user);
    }
}
