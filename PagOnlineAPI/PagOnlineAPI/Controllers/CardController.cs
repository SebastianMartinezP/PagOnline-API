using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PagOnlineAPI;
using System.Linq;

namespace PagOnlineAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class CardController : ControllerBase
    {
        private readonly Models.ModelContext _context;
        private readonly ILogger<CardController> _logger;
        private readonly IMapper _mapper;

        public CardController(ILogger<CardController> logger, IMapper mapper, Models.ModelContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public DTO.Tarjeta GetCard(int pin)
        {
            try
            {
                Models.Tarjeta tarjeta = _context.Tarjeta.FirstOrDefault(t => t.Pin == pin) ?? new Models.Tarjeta();

                return _mapper.Map<Models.Tarjeta, DTO.Tarjeta>(tarjeta);
            }
            catch (Exception e)
            {
                return new DTO.Tarjeta();
            }
        }

        [HttpPost]
        public string SaveCard(DTO.Tarjeta tarjeta)
        {
            try
            {

                Models.Tarjeta t = _mapper.Map<DTO.Tarjeta, Models.Tarjeta>(tarjeta);

                if(_context.Tarjeta.FirstOrDefault(t => t.Numero.Equals(tarjeta.Numero)) != null)
                {
                    
                    _logger.LogInformation("Tarjeta existente");
                    return "Tarjeta existente";
                }

                _context.Tarjeta.Add(t);
                _context.SaveChanges();
                _logger.LogInformation("ok");
                return "ok";

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
