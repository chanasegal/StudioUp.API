using AutoMapper;
using StudioUp.DTO;
using StudioUp.Models;

namespace StudioUp.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContentType, ContentTypeDTO>().ReverseMap();
            CreateMap<ContentSection, ContentSectionDTO>().ReverseMap();


            CreateMap<Training, TrainingDTO>()
               .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.FirstName + " " + src.Trainer.LastName))
                  .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => string.Format("{0}:{1}", src.Hour, src.Minute)))
            .ForMember(dest => dest.CustomerTypeName, opt => opt.MapFrom(src => src.TrainingCustomerType.CustomerType.Title))
            .ForMember(dest => dest.TrainingCustomerTypeName, opt => opt.MapFrom(src => src.TrainingCustomerType.TrainingType.Title + ' ' + src.TrainingCustomerType.CustomerType.Title))
            .ForMember(dest => dest.TrainingTypeName, opt => opt.MapFrom(src => src.TrainingCustomerType.TrainingType.Title));



            CreateMap<Training,TrainingPostDTO >();
           CreateMap<TrainingPostDTO, Training>()
            .ForMember(dest => dest.TrainerID, opt => opt.MapFrom(src => src.TrainerID))
            .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
            .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => src.Hour))
            .ForMember(dest => dest.Minute, opt => opt.MapFrom(src => src.Minute))
            .ForMember(dest => dest.TrainingCustomerTypeId, opt => opt.MapFrom(src => src.TrainingCustomerTypeId))
            .ForMember(dest => dest.ParticipantsCount, opt => opt.MapFrom(src => src.ParticipantsCount))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

        }
    }
}
