namespace Client.Web.Utilities.Constants;

public static class Routes
{
    public const string Home = "/";

    //Containers pages route
    public const string Containers = "/dashboard/containers";
    public const string ViewContainer = "/dashboard/containers/view";
    public const string UpsertContainer = "/dashboard/containers/upsert";

    //Sites pages routes
    public const string Sites = "/dashboard/sites";
    public const string ViewSites = "/dashboard/sites/view";
    public const string UpsertSites = "/dashboard/sites/upsert";

    public const string Links = "/dashboard/links";

    public const string LinksView = "/dashboard/links/view";

    public const string History = "/dashboard/history";

    public const string Identity = "/dashboard/identity";

    public const string IdentityUpsert = "/dashboard/identity/upsert";
    
    public const string Aggregate = "/dashboard/aggregate";

    public const string Error = "/dashboard/error";
}
