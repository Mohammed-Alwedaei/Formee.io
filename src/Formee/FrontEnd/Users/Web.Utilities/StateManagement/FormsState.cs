using Client.Web.Utilities.Dtos.Forms;

namespace Client.Web.Utilities.StateManagement;

public class FormsState
{
    public List<FormDto> FormsCollection;

    public FormDto Form;

    public bool IsFetching;

    public event Action StateChanged;

    public void SetFormsCollectionState(List<FormDto> formsCollection)
    {
        FormsCollection = formsCollection;
    }
    
    public void SetFormState(FormDto form)
    {
        Form = form;
    }

    public void InverseFetchingState() => IsFetching = !IsFetching;
}
