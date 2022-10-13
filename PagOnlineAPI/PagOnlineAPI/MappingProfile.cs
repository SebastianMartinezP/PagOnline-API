using AutoMapper;
using PagOnlineAPI.DTO;
using PagOnlineAPI.Models;

namespace PagOnlineAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region DTO >>> DTO

            CreateMap<DTO.ComprobantePagoRequest, DTO.ComprobantePago>();

            #endregion

            #region Models >>> DTO

            CreateMap<Models.ComprobantePago, DTO.ComprobantePago>();
            CreateMap<Models.ComprobantePago, DTO.ComprobantePagoRequest>();

            #endregion

            #region DTO >>> Models

            CreateMap<DTO.ComprobantePagoRequest, Models.ComprobantePago>();
            CreateMap<DTO.ComprobantePago, Models.ComprobantePago>();

            #endregion
        }
    }
}
