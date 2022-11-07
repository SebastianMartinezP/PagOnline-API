using FluentEmail.Smtp;
using FluentEmail.Core;
using PagOnlineAPI.DTO;
using System.Net.Mail;

namespace PagOnlineAPI
{
    #region Interface

    public interface IMailHandler
    {
        public Task<MailResponse?> SendMailAsync(string emailFrom, string emailTo, string nameTo, string subject, string message);
    }

    #endregion



    // MailHandler para hambiente de desarrollo
    #region PaperCutMailHandler

    public class PaperCutMailHandler : IMailHandler
    {
        public SmtpSender? sender { get; set; }
        public PaperCutMailHandler()
        {

            sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25,
            });

            Email.DefaultSender = this.sender;
        }

        public async Task<MailResponse?> SendMailAsync(string emailFrom, string emailTo, string nameTo, string subject, string message)
        {
            try
            {
                var email = await Email
                    .From(emailFrom)
                    .To(emailTo, nameTo)
                    .Subject(subject)
                    .Body(message, isHtml: true)
                    .SendAsync();

                if (email.Successful)
                {
                    return new MailResponse()
                    {
                        Result = "envio de correo OK",
                        IsSuccessful = email.Successful,
                    };
                }

                return new MailResponse()
                {
                    Result = "Error en el envío: " + email.ErrorMessages.ToString(),
                    IsSuccessful = email.Successful,
                };

            }
            catch (Exception e)
            {
                return new MailResponse()
                {
                    IsSuccessful = false,
                    Result = e.Message,
                };
            }
        }
    }

    #endregion

}
