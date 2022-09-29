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
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly Models.ModelContext _context;
        private readonly ILogger<ComprobantePagoController> _logger;
        private readonly IMapper _mapper;

        public CurrencyExchangeController(ILogger<ComprobantePagoController> logger, IMapper mapper, Models.ModelContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public CurrencyData? GetCurrencyValue(string indicador)
        {
            try
            {
                DTO.CurrencyData? currencyData = new CurrencyExchange().GetCurrency(indicador.ToLower());

                return currencyData;
            }
            catch (Exception)
            {
                return new CurrencyData();
            }
        }

    }
}
