
using AutoMapper;
using NewZealandWalks.Models.Domain;
using NewZealandWalks.Models.Dto.Difficulty;
using NewZealandWalks.Models.Dto.Image;
using NewZealandWalks.Models.Dto.Region;
using NewZealandWalks.Models.Dto.Walks;

namespace NewZealandWalks.Mapping
{
    public class AutoMappingProfile : Profile
    {
       public AutoMappingProfile() {
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<Region,CreateRegionDto>().ReverseMap();
            CreateMap<Region,UpdateRegionDto>().ReverseMap();

            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<Walk,CreateWalksDto>().ReverseMap();
            CreateMap<Walk,UpdateWalksDto>().ReverseMap();

            CreateMap<Difficulty,DifficultyDto>().ReverseMap();

            CreateMap<ImageUploadRequestDto, Image>()
                .ForMember(dest => dest.sizeinbytes, opt => opt.MapFrom(src => src.File.Length))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => Path.GetExtension(src.File.FileName)));
                
        }

    }
}
