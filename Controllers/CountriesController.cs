namespace ASPNETMaker2023.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("countrieslist/{CountryID?}", Name = "countrieslist-Countries-list")]
    [Route("home/countrieslist/{CountryID?}", Name = "countrieslist-Countries-list-2")]
    public async Task<IActionResult> CountriesList()
    {
        // Create page object
        countriesList = new GLOBALS.CountriesList(this);
        countriesList.Cache = _cache;

        // Run the page
        return await countriesList.Run();
    }

    // add
    [Route("countriesadd/{CountryID?}", Name = "countriesadd-Countries-add")]
    [Route("home/countriesadd/{CountryID?}", Name = "countriesadd-Countries-add-2")]
    public async Task<IActionResult> CountriesAdd()
    {
        // Create page object
        countriesAdd = new GLOBALS.CountriesAdd(this);

        // Run the page
        return await countriesAdd.Run();
    }

    // view
    [Route("countriesview/{CountryID?}", Name = "countriesview-Countries-view")]
    [Route("home/countriesview/{CountryID?}", Name = "countriesview-Countries-view-2")]
    public async Task<IActionResult> CountriesView()
    {
        // Create page object
        countriesView = new GLOBALS.CountriesView(this);

        // Run the page
        return await countriesView.Run();
    }

    // edit
    [Route("countriesedit/{CountryID?}", Name = "countriesedit-Countries-edit")]
    [Route("home/countriesedit/{CountryID?}", Name = "countriesedit-Countries-edit-2")]
    public async Task<IActionResult> CountriesEdit()
    {
        // Create page object
        countriesEdit = new GLOBALS.CountriesEdit(this);

        // Run the page
        return await countriesEdit.Run();
    }

    // delete
    [Route("countriesdelete/{CountryID?}", Name = "countriesdelete-Countries-delete")]
    [Route("home/countriesdelete/{CountryID?}", Name = "countriesdelete-Countries-delete-2")]
    public async Task<IActionResult> CountriesDelete()
    {
        // Create page object
        countriesDelete = new GLOBALS.CountriesDelete(this);

        // Run the page
        return await countriesDelete.Run();
    }
}
