namespace ASPNETMaker2023.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("subscriptionslist/{Id?}", Name = "subscriptionslist-Subscriptions-list")]
    [Route("home/subscriptionslist/{Id?}", Name = "subscriptionslist-Subscriptions-list-2")]
    public async Task<IActionResult> SubscriptionsList()
    {
        // Create page object
        subscriptionsList = new GLOBALS.SubscriptionsList(this);
        subscriptionsList.Cache = _cache;

        // Run the page
        return await subscriptionsList.Run();
    }

    // add
    [Route("subscriptionsadd/{Id?}", Name = "subscriptionsadd-Subscriptions-add")]
    [Route("home/subscriptionsadd/{Id?}", Name = "subscriptionsadd-Subscriptions-add-2")]
    public async Task<IActionResult> SubscriptionsAdd()
    {
        // Create page object
        subscriptionsAdd = new GLOBALS.SubscriptionsAdd(this);

        // Run the page
        return await subscriptionsAdd.Run();
    }

    // view
    [Route("subscriptionsview/{Id?}", Name = "subscriptionsview-Subscriptions-view")]
    [Route("home/subscriptionsview/{Id?}", Name = "subscriptionsview-Subscriptions-view-2")]
    public async Task<IActionResult> SubscriptionsView()
    {
        // Create page object
        subscriptionsView = new GLOBALS.SubscriptionsView(this);

        // Run the page
        return await subscriptionsView.Run();
    }

    // edit
    [Route("subscriptionsedit/{Id?}", Name = "subscriptionsedit-Subscriptions-edit")]
    [Route("home/subscriptionsedit/{Id?}", Name = "subscriptionsedit-Subscriptions-edit-2")]
    public async Task<IActionResult> SubscriptionsEdit()
    {
        // Create page object
        subscriptionsEdit = new GLOBALS.SubscriptionsEdit(this);

        // Run the page
        return await subscriptionsEdit.Run();
    }

    // delete
    [Route("subscriptionsdelete/{Id?}", Name = "subscriptionsdelete-Subscriptions-delete")]
    [Route("home/subscriptionsdelete/{Id?}", Name = "subscriptionsdelete-Subscriptions-delete-2")]
    public async Task<IActionResult> SubscriptionsDelete()
    {
        // Create page object
        subscriptionsDelete = new GLOBALS.SubscriptionsDelete(this);

        // Run the page
        return await subscriptionsDelete.Run();
    }
}
