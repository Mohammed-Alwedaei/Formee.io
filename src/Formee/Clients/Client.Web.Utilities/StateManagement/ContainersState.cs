using Client.Web.Utilities.Dtos;

namespace Client.Web.Utilities.StateManagement;

public class ContainersState
{
    public List<ContainerDto> ContainersCollection;
    public ContainerDto Container;

    public bool IsFetching;

    public event Action StateChanged;

    public void SetContainersCollectionState(List<ContainerDto>? containers)
    {
        ContainersCollection = containers;
        StateChanged.Invoke();
    }
    
    public void SetContainerState(ContainerDto container)
    {
        Container = container;
        StateChanged.Invoke();
    }
}