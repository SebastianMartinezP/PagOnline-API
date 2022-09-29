using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PagOnlineAPI;
using PagOnlineAPI.DTO;
using RestSharp;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace PagOnlineAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class ComprobantePagoController : ControllerBase
    {
        private readonly Models.ModelContext _context;
        private readonly ILogger<ComprobantePagoController> _logger;
        private readonly IMapper _mapper;

        public ComprobantePagoController(ILogger<ComprobantePagoController> logger, IMapper mapper, Models.ModelContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public DTO.ComprobantePago GetComprobante(int idComprobante)
        {
            try
            {
                Models.ComprobantePago? comprobante = _context.ComprobantePago.FirstOrDefault(cp =>cp.Idcomprobante == idComprobante);

                if (comprobante != null)
                {
                    return _mapper.Map<Models.ComprobantePago, DTO.ComprobantePago>(comprobante);
                }

                return new DTO.ComprobantePago();

            }
            catch (Exception)
            {
                return new DTO.ComprobantePago();
            }
        }


        [HttpPost]
        public string SaveComprobante(DTO.ComprobantePago comprobantePago)
        {
            try
            {
                Models.ComprobantePago comprobante = 
                    _mapper.Map<DTO.ComprobantePago, Models.ComprobantePago>(comprobantePago);

                if (comprobante.Tipomoneda != null)
                {
                    CurrencyExchange currency = new CurrencyExchange();
                    CurrencyData? data = null;

                    switch (comprobante.Tipomoneda.ToLower())
                    {
                        case "uf":
                            data = currency.GetCurrency("uf");
                            comprobante.Valoruf = (decimal)(data?.value ?? 0);
                            break;

                        case "utm":
                            data = currency.GetCurrency("utm");
                            comprobante.Valoruf = (decimal)(data?.value ?? 0);
                            break;

                        case "usd":
                            data = currency.GetCurrency("dolar");
                            comprobante.Valoruf = (decimal)(data?.value ?? 0);
                            break;

                        default:
                            break;
                    }
                }

                _context.ComprobantePago.Add(comprobante);
                _context.SaveChanges();

                _logger.LogInformation("Comprobante pago generado exitosamente.");

                return "Comprobante pago generado exitosamente.";

            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                return e.Message;
            }
        }


    }
}
