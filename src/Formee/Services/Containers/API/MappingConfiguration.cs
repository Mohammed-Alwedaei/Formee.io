namespace API;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<ContainerEntity, ContainerDto>().ReverseMap();
    }
}
