using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PagOnlineAPI;
using System.Linq;

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
                // IMPORTANTE agregar validaciones!!
                Models.ComprobantePago cp = _mapper.Map<DTO.ComprobantePago, Models.ComprobantePago>(comprobantePago);
                _context.ComprobantePago.Add(cp);
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
