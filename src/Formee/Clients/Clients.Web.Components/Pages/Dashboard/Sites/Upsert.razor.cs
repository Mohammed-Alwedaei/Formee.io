using Client.Web.Utilities.Models;
using Microsoft.AspNetCore.Components.Web;

namespace Clients.Web.Components.Pages.Dashboard.Sites;

[Route(Routes.UpsertSites)]
public partial class Upsert
{
    [Parameter]
    [SupplyParameterFromQuery(Name = "upsert")]
    public string UpsertType { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "containerId")]
    public string ContainerId { get; set; }

    private bool IsDeleteUpsert { get; set; }

    private bool IsVisibleModal { get; set; }

    private SiteDto Site { get; set; } = new ();

    private SiteDto SitePreview { get; set; } = new ();

    private List<IconModel> IconsList { get; set; } = new()
    {
        new IconModel { Id= "fa-solid fa-house", Text = "House" },
        new IconModel { Id= "Icon2", Text = "Smile" },
        new IconModel { Id= "Icon3", Text = "Book" },
        new IconModel { Id= "Icon4", Text = "Laptop" },
        new IconModel { Id= "Icon5", Text = "Business" },
    };

    protected override Task OnParametersSetAsync()
    {
        Site.ContainerId = ContainerId;
        // if true then create new container
        // if not true then update a container
        if (string.IsNullOrEmpty(ContainerId) && UpsertType == "create")
        {
           
        }
        else if (!string.IsNullOrEmpty(ContainerId)
                 && UpsertType == "update"
                 || UpsertType == "delete")
        {
            if (UpsertType == "delete")
            {
                IsDeleteUpsert = true;
            }
        }

        return base.OnParametersSetAsync();
    }

    private void HandleValidFormSubmit()
    {

    }
    
    private void HandleActionApprove()
    {

    }
    
    private void HandleActionReject()
    {

    }

    private void HandleSiteNamePreviewChange(ChangeEventArgs args)
        => Site.Name = args.Value?.ToString();

    private void HandleSiteIconPreviewChange(ChangeEventArgs args)
        => Site.Icon = args.Value?.ToString();
    
    private void HandleSiteDescriptionPreviewChange(Syncfusion.Blazor.RichTextEditor.ChangeEventArgs args)
        => Site.Description = args.Value;
}
