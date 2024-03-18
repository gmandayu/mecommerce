namespace ASPNETMaker2023.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("customerslist/{CustomerID?}", Name = "customerslist-Customers-list")]
    [Route("home/customerslist/{CustomerID?}", Name = "customerslist-Customers-list-2")]
    public async Task<IActionResult> CustomersList()
    {
        // Create page object
        customersList = new GLOBALS.CustomersList(this);
        customersList.Cache = _cache;

        // Run the page
        return await customersList.Run();
    }

    // add
    [Route("customersadd/{CustomerID?}", Name = "customersadd-Customers-add")]
    [Route("home/customersadd/{CustomerID?}", Name = "customersadd-Customers-add-2")]
    public async Task<IActionResult> CustomersAdd()
    {
        // Create page object
        customersAdd = new GLOBALS.CustomersAdd(this);

        // Run the page
        return await customersAdd.Run();
    }

    // view
    [Route("customersview/{CustomerID?}", Name = "customersview-Customers-view")]
    [Route("home/customersview/{CustomerID?}", Name = "customersview-Customers-view-2")]
    public async Task<IActionResult> CustomersView()
    {
        // Create page object
        customersView = new GLOBALS.CustomersView(this);

        // Run the page
        return await customersView.Run();
    }

    // edit
    [Route("customersedit/{CustomerID?}", Name = "customersedit-Customers-edit")]
    [Route("home/customersedit/{CustomerID?}", Name = "customersedit-Customers-edit-2")]
    public async Task<IActionResult> CustomersEdit()
    {
        // Create page object
        customersEdit = new GLOBALS.CustomersEdit(this);

        // Run the page
        return await customersEdit.Run();
    }

    // delete
    [Route("customersdelete/{CustomerID?}", Name = "customersdelete-Customers-delete")]
    [Route("home/customersdelete/{CustomerID?}", Name = "customersdelete-Customers-delete-2")]
    public async Task<IActionResult> CustomersDelete()
    {
        // Create page object
        customersDelete = new GLOBALS.CustomersDelete(this);

        // Run the page
        return await customersDelete.Run();
    }
}
