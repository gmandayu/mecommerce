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

    // add
    [Route("audittrailadd/{Id?}", Name = "audittrailadd-AuditTrail-add")]
    [Route("home/audittrailadd/{Id?}", Name = "audittrailadd-AuditTrail-add-2")]
    public async Task<IActionResult> AuditTrailAdd()
    {
        // Create page object
        auditTrailAdd = new GLOBALS.AuditTrailAdd(this);

        // Run the page
        return await auditTrailAdd.Run();
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

    // edit
    [Route("audittrailedit/{Id?}", Name = "audittrailedit-AuditTrail-edit")]
    [Route("home/audittrailedit/{Id?}", Name = "audittrailedit-AuditTrail-edit-2")]
    public async Task<IActionResult> AuditTrailEdit()
    {
        // Create page object
        auditTrailEdit = new GLOBALS.AuditTrailEdit(this);

        // Run the page
        return await auditTrailEdit.Run();
    }

    // delete
    [Route("audittraildelete/{Id?}", Name = "audittraildelete-AuditTrail-delete")]
    [Route("home/audittraildelete/{Id?}", Name = "audittraildelete-AuditTrail-delete-2")]
    public async Task<IActionResult> AuditTrailDelete()
    {
        // Create page object
        auditTrailDelete = new GLOBALS.AuditTrailDelete(this);

        // Run the page
        return await auditTrailDelete.Run();
    }
}
