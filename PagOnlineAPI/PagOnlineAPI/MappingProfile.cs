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

            CreateMap<Models.Tarjeta, DTO.Tarjeta>();

            #endregion

            #region DTO >>> Models

            CreateMap<DTO.Tarjeta, Models.Tarjeta>();

            #endregion
        }
    }
}
