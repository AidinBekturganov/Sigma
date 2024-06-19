using AutoMapper;
using Sigma.Domain.Dto.Client;
using Sigma.Domain.Dto.ClientServices;
using Sigma.Domain.Entity;

namespace Sigma.BLL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<AddClientRequestDto, Client>();
            CreateMap<Client, AddClientResponseDto>();
        }
    }
}
