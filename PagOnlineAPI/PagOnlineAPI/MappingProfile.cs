using AutoMapper;
using PagOnlineAPI.DTO;
using PagOnlineAPI.Models;

namespace PagOnlineAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Models >>> DTO

            CreateMap<Models.ComprobantePago, DTO.ComprobantePago>();

            #endregion

            #region DTO >>> Models

            CreateMap<DTO.ComprobantePago, Models.ComprobantePago>();

            #endregion
        }
    }
}
