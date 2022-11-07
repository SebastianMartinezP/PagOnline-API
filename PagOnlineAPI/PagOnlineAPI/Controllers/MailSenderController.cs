using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PagOnlineAPI;
using PagOnlineAPI.DTO;
using RestSharp;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace PagOnlineAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MailSenderController : ControllerBase
    {
        private readonly Models.ModelContext _context;
        private readonly ILogger<MailSenderController> _logger;
        private readonly IMapper _mapper;
        private readonly IMailHandler _mailHandler;

        public MailSenderController(ILogger<MailSenderController> logger, IMapper mapper, Models.ModelContext context, IMailHandler mailHandler)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _mailHandler = mailHandler;
        }

        [HttpPost]
        public async Task<MailResponse?> SendPaymentEmail(string emailFrom, string emailTo, string nameTo, string paymentData)
        {
            try
            {
                string subject = "Pago realizado - Security";

                StringBuilder message = new();
                message.AppendLine(string.Format("<h1>Hola {0},</h1>", nameTo));
                message.AppendLine("<p> Queremos comunicarte que se realizó un pago recientemente a tu nombre. </p>");
                message.AppendLine(string.Format("<p> Datos: {0} </p>", paymentData));
                message.AppendLine("<p> Muchas gracias por contratar nuestro servicio! </p>");
                message.AppendLine(" - Security.");



                MailResponse? response = await _mailHandler.SendMailAsync(emailFrom, emailTo, nameTo, subject, message.ToString());
                return response;
            }
            catch (Exception)
            {
                return new MailResponse();
            }
        }

        [HttpPost]
        public async Task<MailResponse?> SendContractExpiredEmail(string emailFrom, string emailTo, string nameTo, string contractData)
        {
            try
            {
                string subject = "Contrato caducado - Security";

                StringBuilder message = new();
                message.AppendLine(string.Format("<h1>Hola {0},</h1>", nameTo));
                message.AppendLine("<p> Queremos informarte que tu contrato mensual ha caducado debido a que no pudimos efectuar el pago. </p>");
                message.AppendLine(string.Format("<p> Contrato: {0} </p>", contractData));
                message.AppendLine("<p> Te recomendamos comunicarte con nosotros para gestionar el pago manualmente. </p>");
                message.AppendLine(" - Security.");



                MailResponse? response = await _mailHandler.SendMailAsync(emailFrom, emailTo, nameTo, subject, message.ToString());
                return response;
            }
            catch (Exception)
            {
                return new MailResponse();
            }
        }
    }
}
