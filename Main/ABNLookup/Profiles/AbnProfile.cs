using ABNLookup.Dtos;
using ABNLookup.Domain.Models;
using AutoMapper;

namespace ABNLookup.Profiles
{
    public class AbnProfile : Profile
    {
        public AbnProfile()
        {
            CreateMap<Abn, AbnV1DTO>();
            CreateMap<Abn, AbnV2DTO>()
                .ForMember(dest => dest.AustralianBusinessNumber,
                    opt => opt.MapFrom(src => src.ABNidentifierValue))                
                .ForMember(dest => dest.MainOrganisationName,
                    opt => opt.MapFrom(src => src.MainNameorganisationName));

            CreateMap<Abn, AcnV1DTO>();
            CreateMap<Abn, AcnV2DTO>()
                .ForMember(dest => dest.AustralianCompanyNumber,
                    opt => opt.MapFrom(src => src.ACNidentifierValue))
                .ForMember(dest => dest.MainOrganisationName,
                    opt => opt.MapFrom(src => src.MainNameorganisationName));

            CreateMap<AbnRegisterDTO, Abn>()
                .ForMember(dest => dest.MainNameorganisationName,
                opt => opt.MapFrom(src => src.BusinessName));

            CreateMap<Abn, AbnNewDTO>()
               .ForMember(dest => dest.AustralianBusinessNumber,
               opt => opt.MapFrom(src => src.ABNidentifierValue));            
        }
    }
}