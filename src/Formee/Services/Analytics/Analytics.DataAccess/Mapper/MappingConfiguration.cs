using Analytics.Utilities.Dtos.Category;
using Analytics.Utilities.Dtos.Site;
using Analytics.Utilities.Entities;
using AutoMapper;

namespace Analytics.BusinessLogic.Mapper;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        //Site entities mapping
        CreateMap<SiteEntity, CreateSiteDto>().ReverseMap();
        CreateMap<SiteEntity, UpdateSiteDto>().ReverseMap();
        CreateMap<SiteEntity, DeleteSiteDto>().ReverseMap();

        //Category entities mapping
        CreateMap<CategoryEntity, CreateCategoryDto>().ReverseMap();
        CreateMap<CategoryEntity, UpdateCategoryDto>().ReverseMap();
        CreateMap<CategoryEntity, DeleteCategoryDto>().ReverseMap();
    }
}
