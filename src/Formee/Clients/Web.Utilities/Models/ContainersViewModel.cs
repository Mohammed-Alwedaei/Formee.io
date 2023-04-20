using Client.Web.Utilities.Dtos;
using Client.Web.Utilities.Dtos.Analytics;

namespace Client.Web.Utilities.Models;

public class ContainersViewModel
{
    public List<ContainerDto> Containers { get; set; }

    public List<SiteDto> Sites { get; set; }
}
