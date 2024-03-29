﻿using Analytics.Utilities.Dtos.PageHit;
using Analytics.Utilities.Dtos.Session;

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

        //Hit entities mapping
        CreateMap<PageHitEntity, CreatePageHitDto>().ReverseMap();
        CreateMap<PageHitEntity, PageHitDto>().ReverseMap();

        //Session entities mapping
        CreateMap<SessionEntity, SessionDto>().ReverseMap();
    }
}
