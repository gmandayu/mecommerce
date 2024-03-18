namespace ASPNETMaker2023.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("audittraillist/{Id?}", Name = "audittraillist-AuditTrail-list")]
    [Route("home/audittraillist/{Id?}", Name = "audittraillist-AuditTrail-list-2")]
    public async Task<IActionResult> AuditTrailList()
    {
        // Create page object
        auditTrailList = new GLOBALS.AuditTrailList(this);
        auditTrailList.Cache = _cache;

        // Run the page
        return await auditTrailList.Run();
    }

    // view
    [Route("audittrailview/{Id?}", Name = "audittrailview-AuditTrail-view")]
    [Route("home/audittrailview/{Id?}", Name = "audittrailview-AuditTrail-view-2")]
    public async Task<IActionResult> AuditTrailView()
    {
        // Create page object
        auditTrailView = new GLOBALS.AuditTrailView(this);

        // Run the page
        return await auditTrailView.Run();
    }
}
