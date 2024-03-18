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
}
