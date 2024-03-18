namespace ASPNETMaker2023.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("userslist/{UserID?}", Name = "userslist-Users-list")]
    [Route("home/userslist/{UserID?}", Name = "userslist-Users-list-2")]
    public async Task<IActionResult> UsersList()
    {
        // Create page object
        usersList = new GLOBALS.UsersList(this);
        usersList.Cache = _cache;

        // Run the page
        return await usersList.Run();
    }

    // add
    [Route("usersadd/{UserID?}", Name = "usersadd-Users-add")]
    [Route("home/usersadd/{UserID?}", Name = "usersadd-Users-add-2")]
    public async Task<IActionResult> UsersAdd()
    {
        // Create page object
        usersAdd = new GLOBALS.UsersAdd(this);

        // Run the page
        return await usersAdd.Run();
    }

    // view
    [Route("usersview/{UserID?}", Name = "usersview-Users-view")]
    [Route("home/usersview/{UserID?}", Name = "usersview-Users-view-2")]
    public async Task<IActionResult> UsersView()
    {
        // Create page object
        usersView = new GLOBALS.UsersView(this);

        // Run the page
        return await usersView.Run();
    }

    // edit
    [Route("usersedit/{UserID?}", Name = "usersedit-Users-edit")]
    [Route("home/usersedit/{UserID?}", Name = "usersedit-Users-edit-2")]
    public async Task<IActionResult> UsersEdit()
    {
        // Create page object
        usersEdit = new GLOBALS.UsersEdit(this);

        // Run the page
        return await usersEdit.Run();
    }

    // delete
    [Route("usersdelete/{UserID?}", Name = "usersdelete-Users-delete")]
    [Route("home/usersdelete/{UserID?}", Name = "usersdelete-Users-delete-2")]
    public async Task<IActionResult> UsersDelete()
    {
        // Create page object
        usersDelete = new GLOBALS.UsersDelete(this);

        // Run the page
        return await usersDelete.Run();
    }
}
