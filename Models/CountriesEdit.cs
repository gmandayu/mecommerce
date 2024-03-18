namespace ASPNETMaker2023.Models;

// Partial class
public partial class mecommerce {
    /// <summary>
    /// countriesEdit
    /// </summary>
    public static CountriesEdit countriesEdit
    {
        get => HttpData.Get<CountriesEdit>("countriesEdit")!;
        set => HttpData["countriesEdit"] = value;
    }

    /// <summary>
    /// Page class for Countries
    /// </summary>
    public class CountriesEdit : CountriesEditBase
    {
        // Constructor
        public CountriesEdit(Controller controller) : base(controller)
        {
        }

        // Constructor
        public CountriesEdit() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class CountriesEditBase : Countries
    {
        // Page ID
        public string PageID = "edit";

        // Project ID
        public string ProjectID = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}";

        // Table name
        public string TableName { get; set; } = "Countries";

        // Page object name
        public string PageObjName = "countriesEdit";

        // Title
        public string? Title = null; // Title for <title> tag

        // Page headings
        public string Heading = "";

        public string Subheading = "";

        public string PageHeader = "";

        public string PageFooter = "";

        // Token
        public string? Token = null; // DN

        public bool CheckToken = Config.CheckToken;

        // Action result // DN
        public IActionResult? ActionResult;

        // Cache // DN
        public IMemoryCache? Cache;

        // Page layout
        public bool UseLayout = true;

        // Page terminated // DN
        private bool _terminated = false;

        // Is terminated
        public bool IsTerminated => _terminated;

        // Is lookup
        public bool IsLookup => IsApi() && RouteValues.TryGetValue("controller", out object? name) && SameText(name, Config.ApiLookupAction);

        // Is AutoFill
        public bool IsAutoFill => IsLookup && SameText(Post("ajax"), "autofill");

        // Is AutoSuggest
        public bool IsAutoSuggest => IsLookup && SameText(Post("ajax"), "autosuggest");

        // Is modal lookup
        public bool IsModalLookup => IsLookup && SameText(Post("ajax"), "modal");

        // Page URL
        private string _pageUrl = "";

        // Constructor
        public CountriesEditBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-edit-table";

            // Language object
            Language = ResolveLanguage();

            // Table object (countries)
            if (countries == null || countries is Countries)
                countries = this;

            // Start time
            StartTime = Environment.TickCount;

            // Debug message
            LoadDebugMessage();

            // Open connection
            Conn = Connection; // DN

            // User table object (Users)
            UserTable = Resolve("usertable")!;
            UserTableConn = GetConnection(UserTable.DbId);
        }

        // Page action result
        public IActionResult PageResult()
        {
            if (ActionResult != null)
                return ActionResult;
            SetupMenus();
            return Controller.View();
        }

        // Page heading
        public string PageHeading
        {
            get {
                if (!Empty(Heading))
                    return Heading;
                else if (!Empty(Caption))
                    return Caption;
                else
                    return "";
            }
        }

        // Page subheading
        public string PageSubheading
        {
            get {
                if (!Empty(Subheading))
                    return Subheading;
                if (!Empty(TableName))
                    return Language.Phrase(PageID);
                return "";
            }
        }

        // Page name
        public string PageName => "countriesedit";

        // Page URL
        public string PageUrl
        {
            get {
                if (_pageUrl == "") {
                    _pageUrl = PageName + "?";
                }
                return _pageUrl;
            }
        }

        // Show Page Header
        public IHtmlContent ShowPageHeader()
        {
            string header = PageHeader;
            PageDataRendering(ref header);
            if (!Empty(header)) // Header exists, display
                return new HtmlString("<p id=\"ew-page-header\">" + header + "</p>");
            return HtmlString.Empty;
        }

        // Show Page Footer
        public IHtmlContent ShowPageFooter()
        {
            string footer = PageFooter;
            PageDataRendered(ref footer);
            if (!Empty(footer)) // Footer exists, display
                return new HtmlString("<p id=\"ew-page-footer\">" + footer + "</p>");
            return HtmlString.Empty;
        }

        // Valid post
        protected async Task<bool> ValidPost() => !CheckToken || !IsPost() || IsApi() || Antiforgery != null && HttpContext != null && await Antiforgery.IsRequestValidAsync(HttpContext);

        // Create token
        public void CreateToken()
        {
            Token ??= HttpContext != null ? Antiforgery?.GetAndStoreTokens(HttpContext).RequestToken : null;
            CurrentToken = Token ?? ""; // Save to global variable
        }

        // Set field visibility
        public void SetVisibility()
        {
            CountryID.Visible = false;
            ISO.SetVisibility();
            _Name.SetVisibility();
            NiceName.SetVisibility();
            ISO3.SetVisibility();
            NumCode.SetVisibility();
            PhoneCode.SetVisibility();
            CreatedBy.Visible = false;
            CreatedDateTime.Visible = false;
            UpdatedBy.Visible = false;
            UpdatedDateTime.Visible = false;
        }

        // Constructor
        public CountriesEditBase(Controller? controller = null): this() { // DN
            if (controller != null)
                Controller = controller;
        }

        /// <summary>
        /// Terminate page
        /// </summary>
        /// <param name="url">URL to rediect to</param>
        /// <returns>Page result</returns>
        public override IActionResult Terminate(string url = "") { // DN
            if (_terminated) // DN
                return new EmptyResult();

            // Page Unload event
            PageUnload();

            // Global Page Unloaded event
            PageUnloaded();
            if (!IsApi())
                PageRedirecting(ref url);

            // Gargage collection
            Collect(); // DN

            // Terminate
            _terminated = true; // DN

            // Return for API
            if (IsApi()) {
                var result = new Dictionary<string, string> { { "version", Config.ProductVersion } };
                if (!Empty(url)) // Add url
                    result.Add("url", GetUrl(url));
                foreach (var (key, value) in GetMessages()) // Add messages
                    result.Add(key, value);
                return Controller.Json(result);
            } else if (ActionResult != null) { // Check action result
                return ActionResult;
            }

            // Go to URL if specified
            if (!Empty(url)) {
                if (!Config.Debug)
                    ResponseClear();
                if (Response != null && !Response.HasStarted) {
                    // Handle modal response (Assume return to modal for simplicity)
                    if (IsModal) { // Show as modal
                        var result = new Dictionary<string, string> { {"url", GetUrl(url)}, {"modal", "1"} };
                        string pageName = GetPageName(url);
                        if (pageName != ListUrl) { // Not List page
                            result.Add("caption", GetModalCaption(pageName));
                            result.Add("view", pageName == "countriesview" ? "1" : "0"); // If View page, no primary button
                        } else { // List page
                            // result.Add("list", PageID == "search" ? "1" : "0"); // Refresh List page if current page is Search page
                            result.Add("error", FailureMessage); // List page should not be shown as modal => error
                            ClearFailureMessage();
                        }
                        return Controller.Json(result);
                    } else {
                        SaveDebugMessage();
                        return Controller.LocalRedirect(AppPath(url));
                    }
                }
            }
            return new EmptyResult();
        }

        // Get all records from datareader
        [return: NotNullIfNotNull("dr")]
        protected async Task<List<Dictionary<string, object>>> GetRecordsFromRecordset(DbDataReader? dr)
        {
            var rows = new List<Dictionary<string, object>>();
            while (dr != null && await dr.ReadAsync()) {
                await LoadRowValues(dr); // Set up DbValue/CurrentValue
                if (GetRecordFromDictionary(GetDictionary(dr)) is Dictionary<string, object> row)
                    rows.Add(row);
            }
            return rows;
        }

        // Get all records from the list of records
        #pragma warning disable 1998

        protected async Task<List<Dictionary<string, object>>> GetRecordsFromRecordset(List<Dictionary<string, object>>? list)
        {
            var rows = new List<Dictionary<string, object>>();
            if (list != null) {
                foreach (var row in list) {
                    if (GetRecordFromDictionary(row) is Dictionary<string, object> d)
                       rows.Add(row);
                }
            }
            return rows;
        }
        #pragma warning restore 1998

        // Get the first record from datareader
        [return: NotNullIfNotNull("dr")]
        protected async Task<Dictionary<string, object>?> GetRecordFromRecordset(DbDataReader? dr)
        {
            if (dr != null) {
                await LoadRowValues(dr); // Set up DbValue/CurrentValue
                return GetRecordFromDictionary(GetDictionary(dr));
            }
            return null;
        }

        // Get the first record from the list of records
        protected Dictionary<string, object>? GetRecordFromRecordset(List<Dictionary<string, object>>? list) =>
            list != null && list.Count > 0 ? GetRecordFromDictionary(list[0]) : null;

        // Get record from Dictionary
        protected Dictionary<string, object>? GetRecordFromDictionary(Dictionary<string, object>? dict) {
            if (dict == null)
                return null;
            var row = new Dictionary<string, object>();
            foreach (var (key, value) in dict) {
                if (Fields.TryGetValue(key, out DbField? fld)) {
                    if (fld.Visible || fld.IsPrimaryKey) { // Primary key or Visible
                        if (fld.HtmlTag == "FILE") { // Upload field
                            if (Empty(value)) {
                                // row[key] = null;
                            } else {
                                if (fld.DataType == DataType.Blob) {
                                    string url = FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + fld.Param + "/" + GetRecordKeyValue(dict)); // Query string format
                                    row[key] = new Dictionary<string, object> { { "type", ContentType((byte[])value) }, { "url", url }, { "name", fld.Param + ContentExtension((byte[])value) } };
                                } else if (!fld.UploadMultiple || !ConvertToString(value).Contains(Config.MultipleUploadSeparator)) { // Single file
                                    string url = FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + Encrypt(fld.PhysicalUploadPath + ConvertToString(value))); // Query string format
                                    row[key] = new Dictionary<string, object> { { "type", ContentType(ConvertToString(value)) }, { "url", url }, { "name", ConvertToString(value) } };
                                } else { // Multiple files
                                    var files = ConvertToString(value).Split(Config.MultipleUploadSeparator);
                                    row[key] = files.Where(file => !Empty(file)).Select(file => new Dictionary<string, object> { { "type", ContentType(file) }, { "url", FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + Encrypt(fld.PhysicalUploadPath + file)) }, { "name", file } });
                                }
                            }
                        } else {
                            string val = ConvertToString(value);
                            if (fld.DataType == DataType.Date && value is DateTime dt)
                                val = dt.ToString("s");
                            row[key] = ConvertToString(val);
                        }
                    }
                }
            }
            return row;
        }

        // Get record key value from array
        protected string GetRecordKeyValue(Dictionary<string, object> dict) {
            string key = "";
            key += UrlEncode(ConvertToString(dict.ContainsKey("CountryID") ? dict["CountryID"] : CountryID.CurrentValue));
            return key;
        }

        // Hide fields for Add/Edit
        protected void HideFieldsForAddEdit() {
            if (IsAdd || IsCopy || IsGridAdd)
                CountryID.Visible = false;
        }

        #pragma warning disable 219
        /// <summary>
        /// Lookup data from table
        /// </summary>
        public async Task<Dictionary<string, object>> Lookup(Dictionary<string, string>? dict = null)
        {
            Language = ResolveLanguage();
            Security = ResolveSecurity();
            string? v;

            // Get lookup object
            string fieldName = IsDictionary(dict) && dict.TryGetValue("field", out v) && v != null ? v : Post("field");
            var lookupField = FieldByName(fieldName);
            var lookup = lookupField?.Lookup;
            if (lookup == null) // DN
                return new Dictionary<string, object>();
            string lookupType = IsDictionary(dict) && dict.TryGetValue("ajax", out v) && v != null ? v : (Post("ajax") ?? "unknown");
            int pageSize = -1;
            int offset = -1;
            string searchValue = "";
            if (SameText(lookupType, "modal") || SameText(lookupType, "filter")) {
                searchValue = IsDictionary(dict) && (dict.TryGetValue("q", out v) && v != null || dict.TryGetValue("sv", out v) && v != null)
                    ? v
                    : (Param("q") ?? Post("sv"));
                pageSize = IsDictionary(dict) && (dict.TryGetValue("n", out v) || dict.TryGetValue("recperpage", out v))
                    ? ConvertToInt(v)
                    : (IsNumeric(Param("n")) ? Param<int>("n") : (Post("recperpage", out StringValues rpp) ? ConvertToInt(rpp.ToString()) : 10));
            } else if (SameText(lookupType, "autosuggest")) {
                searchValue = IsDictionary(dict) && dict.TryGetValue("q", out v) && v != null ? v : Param("q");
                pageSize = IsDictionary(dict) && dict.TryGetValue("n", out v) ? ConvertToInt(v) : (IsNumeric(Param("n")) ? Param<int>("n") : -1);
                if (pageSize <= 0)
                    pageSize = Config.AutoSuggestMaxEntries;
            }
            int start = IsDictionary(dict) && dict.TryGetValue("start", out v) ? ConvertToInt(v) : (IsNumeric(Param("start")) ? Param<int>("start") : -1);
            int page = IsDictionary(dict) && dict.TryGetValue("page", out v) ? ConvertToInt(v) : (IsNumeric(Param("page")) ? Param<int>("page") : -1);
            offset = start >= 0 ? start : (page > 0 && pageSize > 0 ? (page - 1) * pageSize : 0);
            string userSelect = Decrypt(IsDictionary(dict) && dict.TryGetValue("s", out v) && v != null ? v : Post("s"));
            string userFilter = Decrypt(IsDictionary(dict) && dict.TryGetValue("f", out v) && v != null ? v : Post("f"));
            string userOrderBy = Decrypt(IsDictionary(dict) && dict.TryGetValue("o", out v) && v != null ? v : Post("o"));

            // Selected records from modal, skip parent/filter fields and show all records
            lookup.LookupType = lookupType; // Lookup type
            lookup.FilterValues.Clear(); // Clear filter values first
            StringValues keys = IsDictionary(dict) && dict.TryGetValue("keys", out v) && !Empty(v)
                ? (StringValues)v
                : (Post("keys[]", out StringValues k) ? (StringValues)k : StringValues.Empty);
            if (!Empty(keys)) { // Selected records from modal
                lookup.FilterFields = new (); // Skip parent fields if any
                pageSize = -1; // Show all records
                lookup.FilterValues.Add(String.Join(",", keys.ToArray()));
            } else { // Lookup values
                string lookupValue = IsDictionary(dict) && (dict.TryGetValue("v0", out v) && v != null || dict.TryGetValue("lookupValue", out v) && v != null)
                    ? v
                    : (Post<string>("v0") ?? Post("lookupValue"));
                lookup.FilterValues.Add(lookupValue);
            }
            int cnt = IsDictionary(lookup.FilterFields) ? lookup.FilterFields.Count : 0;
            for (int i = 1; i <= cnt; i++) {
                var val = UrlDecode(IsDictionary(dict) && dict.TryGetValue("v" + i, out v) ? v : Post("v" + i));
                if (val != null) // DN
                    lookup.FilterValues.Add(val);
            }
            lookup.SearchValue = searchValue;
            lookup.PageSize = pageSize;
            lookup.Offset = offset;
            if (userSelect != "")
                lookup.UserSelect = userSelect;
            if (userFilter != "")
                lookup.UserFilter = userFilter;
            if (userOrderBy != "")
                lookup.UserOrderBy = userOrderBy;
            return await lookup.ToJson(this);
        }
        #pragma warning restore 219

        private Pager? _pager; // DN

        public int DisplayRecords = 1; // Number of display records

        public int StartRecord;

        public int StopRecord;

        public int TotalRecords = -1;

        public int RecordRange = 10;

        public int RecordCount;

        public Dictionary<string, string> RecordKeys = new ();

        public string FormClassName = "ew-form ew-edit-form overlay-wrapper";

        public bool IsModal = false;

        public bool IsMobileOrModal = false;

        public string DbMasterFilter = "";

        public string DbDetailFilter = "";

        public DbDataReader? Recordset; // DN

        public Pager Pager
        {
            get {
                _pager ??= new NumericPager(this, StartRecord, DisplayRecords, TotalRecords, "", RecordRange, AutoHidePager, false, false);
                _pager.PageNumberName = Config.TablePageNumber;
                _pager.PagePhraseId = "Record"; // Show as record
                return _pager;
            }
        }

        #pragma warning disable 219
        /// <summary>
        /// Page run
        /// </summary>
        /// <returns>Page result</returns>
        public override async Task<IActionResult> Run()
        {
            // Is modal
            IsModal = Param<bool>("modal");
            UseLayout = UseLayout && !IsModal;

            // Use layout
            if (!Empty(Param("layout")) && !Param<bool>("layout"))
                UseLayout = false;

            // User profile
            Profile = ResolveProfile();

            // Security
            Security = ResolveSecurity();
            if (TableVar != "")
                Security.LoadTablePermissions(TableVar);

            // Create form object
            CurrentForm ??= new ();
            await CurrentForm.Init();
            CurrentAction = Param("action"); // Set up current action
            SetVisibility();

            // Do not use lookup cache
            if (!Config.LookupCachePageIds.Contains(PageID))
                SetUseLookupCache(false);

            // Global Page Loading event
            PageLoading();

            // Page Load event
            PageLoad();

            // Check token
            if (!await ValidPost())
                End(Language.Phrase("InvalidPostRequest"));

            // Check action result
            if (ActionResult != null) // Action result set by server event // DN
                return ActionResult;

            // Create token
            CreateToken();

            // Hide fields for add/edit
            if (!UseAjaxActions)
                HideFieldsForAddEdit();
            // Use inline delete
            if (UseAjaxActions)
                InlineDelete = true;

            // Check modal
            if (IsModal)
                SkipHeaderFooter = true;
            IsMobileOrModal = IsMobile() || IsModal;

            // Load record by position
            bool loadByPosition = false;
            bool loaded = false;
            bool postBack = false;
            StringValues sv;
            object? rv;

            // Set up current action and primary key
            if (IsApi()) {
                loaded = true;

                // Load key from form
                string[] keyValues = {};
                if (RouteValues.TryGetValue("key", out object? k))
                    keyValues = ConvertToString(k).Split('/');
                if (RouteValues.TryGetValue("CountryID", out rv)) { // DN
                    CountryID.FormValue = UrlDecode(rv); // DN
                    CountryID.OldValue = CountryID.FormValue;
                } else if (CurrentForm.HasValue("x_CountryID")) {
                    CountryID.FormValue = CurrentForm.GetValue("x_CountryID");
                    CountryID.OldValue = CountryID.FormValue;
                } else if (!Empty(keyValues)) {
                    CountryID.OldValue = ConvertToString(keyValues[0]);
                } else {
                    loaded = false; // Unable to load key
                }

                // Load record
                if (loaded)
                    loaded = await LoadRow();
                if (!loaded) {
                    FailureMessage = Language.Phrase("NoRecord"); // Set no record message
                    return Terminate();
                }
                CurrentAction = "update"; // Update record directly
                OldKey = GetKey(true); // Get from CurrentValue
                postBack = true;
            } else {
                if (!Empty(Post("action"))) {
                    CurrentAction = Post("action"); // Get action code
                    if (!IsShow) // Not reload record, handle as postback
                        postBack = true;

                    // Get key from Form
                    if (Post(OldKeyName, out sv))
                        SetKey(sv.ToString(), IsShow);
                } else {
                    CurrentAction = "show"; // Default action is display

                    // Load key from QueryString
                    bool loadByQuery = false;
                    if (RouteValues.TryGetValue("CountryID", out rv)) { // DN
                        CountryID.QueryValue = UrlDecode(rv); // DN
                        loadByQuery = true;
                    } else if (Get("CountryID", out sv)) {
                        CountryID.QueryValue = sv.ToString();
                        loadByQuery = true;
                    } else {
                        CountryID.CurrentValue = DbNullValue;
                    }
                    if (!loadByQuery || IsNumeric(Get(Config.TableStartRec)) || IsNumeric(Get(Config.TablePageNumber)))
                        loadByPosition = true;
                }

                // Load recordset
                if (IsShow) {
                    if (!IsModal) { // Normal edit page
                        StartRecord = 1; // Initialize start position
                        Recordset = await LoadRecordset(); // Load records
                        TotalRecords = await ListRecordCountAsync(); // Get record count // DN
                        if (TotalRecords <= 0) { // No record found
                            if (Empty(SuccessMessage) && Empty(FailureMessage))
                                FailureMessage = Language.Phrase("NoRecord"); // Set no record message
                            if (IsApi()) {
                                if (!Empty(SuccessMessage))
                                    return new JsonBoolResult(new { success = true, message = SuccessMessage, version = Config.ProductVersion }, true);
                                else
                                    return new JsonBoolResult(new { success = false, error = FailureMessage, version = Config.ProductVersion }, false);
                            } else {
                                return Terminate("countrieslist"); // Return to list page
                            }
                        } else if (loadByPosition) { // Load record by position
                            SetupStartRecord(); // Set up start record position
                            // Point to current record
                            if (Recordset != null && StartRecord <= TotalRecords) {
                                for (int i = 1; i <= StartRecord; i++)
                                    await Recordset.ReadAsync();

                                // Redirect to correct record
                                await LoadRowValues(Recordset);
                                string url = GetCurrentUrl();
                                return Terminate(url);
                            }
                        } else { // Match key values
                            if (CountryID.CurrentValue != null) {
                                while (Recordset != null && await Recordset.ReadAsync()) {
                                    if (SameString(CountryID.CurrentValue, Recordset["CountryID"])) {
                                        StartRecordNumber = StartRecord; // Save record position
                                        loaded = true;
                                        break;
                                    } else {
                                        StartRecord++;
                                    }
                                }
                            }
                        }

                        // Load current row values
                        if (loaded)
                            await LoadRowValues(Recordset);
                } else {
                    // Load current record
                    loaded = await LoadRow();
                } // End modal checking
                OldKey = loaded ? GetKey(true) : ""; // Get from CurrentValue
            }
        }

        // Process form if post back
        if (postBack) {
            await LoadFormValues(); // Get form values
            if (IsApi() && RouteValues.TryGetValue("key", out object? k)) {
                var keyValues = ConvertToString(k).Split('/');
                CountryID.FormValue = ConvertToString(keyValues[0]);
            }
        }

        // Validate form if post back
        if (postBack) {
            if (!await ValidateForm()) {
                EventCancelled = true; // Event cancelled
                RestoreFormValues();
                if (IsApi())
                    return Terminate();
                else
                    CurrentAction = ""; // Form error, reset action
            }
        }

        // Perform current action
        switch (CurrentAction) {
                case "show": // Get a record to display
                    if (!IsModal) { // Normal edit page
                        if (!loaded) {
                            if (Empty(SuccessMessage) && Empty(FailureMessage))
                                FailureMessage = Language.Phrase("NoRecord"); // Set no record message
                            if (IsApi()) {
                                if (!Empty(SuccessMessage))
                                    return new JsonBoolResult(new { success = true, message = SuccessMessage, version = Config.ProductVersion }, true);
                                else
                                    return new JsonBoolResult(new { success = false, error = FailureMessage, version = Config.ProductVersion }, false);
                            } else {
                                return Terminate("countrieslist"); // Return to list page
                            }
                        } else {
                        }
                    } else { // Modal edit page
                        if (!loaded) { // Load record based on key
                            if (Empty(FailureMessage))
                                FailureMessage = Language.Phrase("NoRecord"); // No record found
                            return Terminate("countrieslist"); // No matching record, return to list
                        }
                    } // End modal checking
                    break;
                case "update": // Update // DN
                    CloseRecordset(); // DN
                    string returnUrl = ReturnUrl;
                    if (GetPageName(returnUrl) == "countrieslist")
                        returnUrl = AddMasterUrl(ListUrl); // List page, return to List page with correct master key if necessary
                    SendEmail = true; // Send email on update success
                    var res = await EditRow();
                    if (res) { // Update record based on key
                        if (Empty(SuccessMessage))
                            SuccessMessage = Language.Phrase("UpdateSuccess"); // Update success

                        // Handle UseAjaxActions with return page
                        if (IsModal && UseAjaxActions) {
                            IsModal = false;
                            if (GetPageName(returnUrl) != "countrieslist") {
                                TempData["Return-Url"] = returnUrl; // Save return URL
                                returnUrl = "countrieslist"; // Return list page content
                            }
                        }
                        if (IsJsonResponse()) {
                            ClearMessages(); // Clear messages for Json response
                            return res;
                        } else {
                            return Terminate(returnUrl); // Return to caller
                        }
                    } else if (IsApi()) { // API request, return
                        return Terminate();
                    } else if (IsModal && UseAjaxActions) { // Return JSON error message
                        return Controller.Json(new { success = false, error = GetFailureMessage() });
                    } else if (FailureMessage == Language.Phrase("NoRecord")) {
                        return Terminate(returnUrl); // Return to caller
                    } else {
                        EventCancelled = true; // Event cancelled
                        RestoreFormValues(); // Restore form values if update failed
                    }
                    break;
            }

            // Set up Breadcrumb
            SetupBreadcrumb();

            // Render the record
            RowType = RowType.Edit; // Render as Edit
            ResetAttributes();
            await RenderRow();

            // Set LoginStatus, Page Rendering and Page Render
            if (!IsApi() && !IsTerminated) {
                SetupLoginStatus(); // Setup login status

                // Pass login status to client side
                SetClientVar("login", LoginStatus);

                // Global Page Rendering event
                PageRendering();

                // Page Render event
                countriesEdit?.PageRender();
            }
            return PageResult();
        }
        #pragma warning restore 219

        // Confirm page
        public bool ConfirmPage = false; // DN

        #pragma warning disable 1998
        // Get upload files
        public async Task GetUploadFiles()
        {
            // Get upload data
        }
        #pragma warning restore 1998

        #pragma warning disable 1998
        // Load form values
        protected async Task LoadFormValues() {
            if (CurrentForm == null)
                return;
            bool validate = !Config.ServerValidate;
            string val;

            // Check field name 'ISO' before field var 'x_ISO'
            val = CurrentForm.HasValue("ISO") ? CurrentForm.GetValue("ISO") : CurrentForm.GetValue("x_ISO");
            if (!ISO.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("ISO") && !CurrentForm.HasValue("x_ISO")) // DN
                    ISO.Visible = false; // Disable update for API request
                else
                    ISO.SetFormValue(val);
            }

            // Check field name 'Name' before field var 'x__Name'
            val = CurrentForm.HasValue("Name") ? CurrentForm.GetValue("Name") : CurrentForm.GetValue("x__Name");
            if (!_Name.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Name") && !CurrentForm.HasValue("x__Name")) // DN
                    _Name.Visible = false; // Disable update for API request
                else
                    _Name.SetFormValue(val);
            }

            // Check field name 'NiceName' before field var 'x_NiceName'
            val = CurrentForm.HasValue("NiceName") ? CurrentForm.GetValue("NiceName") : CurrentForm.GetValue("x_NiceName");
            if (!NiceName.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("NiceName") && !CurrentForm.HasValue("x_NiceName")) // DN
                    NiceName.Visible = false; // Disable update for API request
                else
                    NiceName.SetFormValue(val);
            }

            // Check field name 'ISO3' before field var 'x_ISO3'
            val = CurrentForm.HasValue("ISO3") ? CurrentForm.GetValue("ISO3") : CurrentForm.GetValue("x_ISO3");
            if (!ISO3.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("ISO3") && !CurrentForm.HasValue("x_ISO3")) // DN
                    ISO3.Visible = false; // Disable update for API request
                else
                    ISO3.SetFormValue(val);
            }

            // Check field name 'NumCode' before field var 'x_NumCode'
            val = CurrentForm.HasValue("NumCode") ? CurrentForm.GetValue("NumCode") : CurrentForm.GetValue("x_NumCode");
            if (!NumCode.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("NumCode") && !CurrentForm.HasValue("x_NumCode")) // DN
                    NumCode.Visible = false; // Disable update for API request
                else
                    NumCode.SetFormValue(val, true, validate);
            }

            // Check field name 'PhoneCode' before field var 'x_PhoneCode'
            val = CurrentForm.HasValue("PhoneCode") ? CurrentForm.GetValue("PhoneCode") : CurrentForm.GetValue("x_PhoneCode");
            if (!PhoneCode.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("PhoneCode") && !CurrentForm.HasValue("x_PhoneCode")) // DN
                    PhoneCode.Visible = false; // Disable update for API request
                else
                    PhoneCode.SetFormValue(val, true, validate);
            }

            // Check field name 'CountryID' before field var 'x_CountryID'
            val = CurrentForm.HasValue("CountryID") ? CurrentForm.GetValue("CountryID") : CurrentForm.GetValue("x_CountryID");
            if (!CountryID.IsDetailKey)
                CountryID.SetFormValue(val);
        }
        #pragma warning restore 1998

        // Restore form values
        public void RestoreFormValues()
        {
            CountryID.CurrentValue = CountryID.FormValue;
            ISO.CurrentValue = ISO.FormValue;
            _Name.CurrentValue = _Name.FormValue;
            NiceName.CurrentValue = NiceName.FormValue;
            ISO3.CurrentValue = ISO3.FormValue;
            NumCode.CurrentValue = NumCode.FormValue;
            PhoneCode.CurrentValue = PhoneCode.FormValue;
        }

        // Load recordset // DN
        public async Task<DbDataReader?> LoadRecordset(int offset = -1, int rowcnt = -1)
        {
            // Load list page SQL
            string sql = ListSql;

            // Load recordset // DN
            var dr = await Connection.SelectLimit(sql, rowcnt, offset, !Empty(OrderBy) || !Empty(SessionOrderBy));

            // Call Recordset Selected event
            RecordsetSelected(dr);
            return dr;
        }

        // Load rows // DN
        public async Task<List<Dictionary<string, object>>> LoadRows(int offset = -1, int rowcnt = -1)
        {
            // Load list page SQL
            string sql = ListSql;

            // Load rows // DN
            using var dr = await Connection.SelectLimit(sql, rowcnt, offset, !Empty(OrderBy) || !Empty(SessionOrderBy));
            var rows = await Connection.GetRowsAsync(dr);
            dr.Close(); // Close datareader before return
            return rows;
        }

        // Load row based on key values
        public async Task<bool> LoadRow()
        {
            string filter = GetRecordFilter();

            // Call Row Selecting event
            RowSelecting(ref filter);

            // Load SQL based on filter
            CurrentFilter = filter;
            string sql = CurrentSql;
            bool res = false;
            try {
                var row = await Connection.GetRowAsync(sql);
                if (row != null) {
                    await LoadRowValues(row);
                    res = true;
                } else {
                    return false;
                }
            } catch {
                if (Config.Debug)
                    throw;
            }
            return res;
        }

        #pragma warning disable 162, 168, 1998, 4014
        // Load row values from data reader
        public async Task LoadRowValues(DbDataReader? dr = null) => await LoadRowValues(GetDictionary(dr));

        // Load row values from recordset
        public async Task LoadRowValues(Dictionary<string, object>? row)
        {
            row ??= NewRow();

            // Call Row Selected event
            RowSelected(row);
            CountryID.SetDbValue(row["CountryID"]);
            ISO.SetDbValue(row["ISO"]);
            _Name.SetDbValue(row["Name"]);
            NiceName.SetDbValue(row["NiceName"]);
            ISO3.SetDbValue(row["ISO3"]);
            NumCode.SetDbValue(row["NumCode"]);
            PhoneCode.SetDbValue(row["PhoneCode"]);
            CreatedBy.SetDbValue(row["CreatedBy"]);
            CreatedDateTime.SetDbValue(row["CreatedDateTime"]);
            UpdatedBy.SetDbValue(row["UpdatedBy"]);
            UpdatedDateTime.SetDbValue(row["UpdatedDateTime"]);
        }
        #pragma warning restore 162, 168, 1998, 4014

        // Return a row with default values
        protected Dictionary<string, object> NewRow() {
            var row = new Dictionary<string, object>();
            row.Add("CountryID", CountryID.DefaultValue ?? DbNullValue); // DN
            row.Add("ISO", ISO.DefaultValue ?? DbNullValue); // DN
            row.Add("Name", _Name.DefaultValue ?? DbNullValue); // DN
            row.Add("NiceName", NiceName.DefaultValue ?? DbNullValue); // DN
            row.Add("ISO3", ISO3.DefaultValue ?? DbNullValue); // DN
            row.Add("NumCode", NumCode.DefaultValue ?? DbNullValue); // DN
            row.Add("PhoneCode", PhoneCode.DefaultValue ?? DbNullValue); // DN
            row.Add("CreatedBy", CreatedBy.DefaultValue ?? DbNullValue); // DN
            row.Add("CreatedDateTime", CreatedDateTime.DefaultValue ?? DbNullValue); // DN
            row.Add("UpdatedBy", UpdatedBy.DefaultValue ?? DbNullValue); // DN
            row.Add("UpdatedDateTime", UpdatedDateTime.DefaultValue ?? DbNullValue); // DN
            return row;
        }

        #pragma warning disable 618, 1998
        // Load old record
        protected async Task<Dictionary<string, object>?> LoadOldRecord(DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType>? cnn = null) {
            // Load old record
            Dictionary<string, object>? row = null;
            bool validKey = !Empty(OldKey);
            if (validKey) {
                SetKey(OldKey);
                CurrentFilter = GetRecordFilter();
                string sql = CurrentSql;
                try {
                    row = await (cnn ?? Connection).GetRowAsync(sql);
                } catch {
                    row = null;
                }
            }
            await LoadRowValues(row); // Load row values
            return row;
        }
        #pragma warning restore 618, 1998

        #pragma warning disable 1998
        // Render row values based on field settings
        public async Task RenderRow()
        {
            // Call Row Rendering event
            RowRendering();

            // Common render codes for all row types

            // CountryID
            CountryID.RowCssClass = "row";

            // ISO
            ISO.RowCssClass = "row";

            // Name
            _Name.RowCssClass = "row";

            // NiceName
            NiceName.RowCssClass = "row";

            // ISO3
            ISO3.RowCssClass = "row";

            // NumCode
            NumCode.RowCssClass = "row";

            // PhoneCode
            PhoneCode.RowCssClass = "row";

            // CreatedBy
            CreatedBy.RowCssClass = "row";

            // CreatedDateTime
            CreatedDateTime.RowCssClass = "row";

            // UpdatedBy
            UpdatedBy.RowCssClass = "row";

            // UpdatedDateTime
            UpdatedDateTime.RowCssClass = "row";

            // View row
            if (RowType == RowType.View) {
                // ISO
                ISO.ViewValue = ConvertToString(ISO.CurrentValue); // DN
                ISO.ViewCustomAttributes = "";

                // Name
                _Name.ViewValue = ConvertToString(_Name.CurrentValue); // DN
                _Name.ViewCustomAttributes = "";

                // NiceName
                NiceName.ViewValue = ConvertToString(NiceName.CurrentValue); // DN
                NiceName.ViewCustomAttributes = "";

                // ISO3
                ISO3.ViewValue = ConvertToString(ISO3.CurrentValue); // DN
                ISO3.ViewCustomAttributes = "";

                // NumCode
                NumCode.ViewValue = NumCode.CurrentValue;
                NumCode.ViewValue = FormatNumber(NumCode.ViewValue, NumCode.FormatPattern);
                NumCode.ViewCustomAttributes = "";

                // PhoneCode
                PhoneCode.ViewValue = PhoneCode.CurrentValue;
                PhoneCode.ViewValue = FormatNumber(PhoneCode.ViewValue, PhoneCode.FormatPattern);
                PhoneCode.ViewCustomAttributes = "";

                // CreatedBy
                curVal = ConvertToString(CreatedBy.CurrentValue);
                if (!Empty(curVal)) {
                    if (CreatedBy.Lookup != null && IsDictionary(CreatedBy.Lookup?.Options) && CreatedBy.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                        CreatedBy.ViewValue = CreatedBy.LookupCacheOption(curVal);
                    } else { // Lookup from database // DN
                        filterWrk = SearchFilter("[UserID]", "=", CreatedBy.CurrentValue, DataType.Number, "");
                        sqlWrk = CreatedBy.Lookup?.GetSql(false, filterWrk, null, this, true, true);
                        rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                        if (rswrk?.Count > 0 && CreatedBy.Lookup != null) { // Lookup values found
                            var listwrk = CreatedBy.Lookup?.RenderViewRow(rswrk[0]);
                            CreatedBy.ViewValue = CreatedBy.HighlightLookup(ConvertToString(rswrk[0]), CreatedBy.DisplayValue(listwrk));
                        } else {
                            CreatedBy.ViewValue = FormatNumber(CreatedBy.CurrentValue, CreatedBy.FormatPattern);
                        }
                    }
                } else {
                    CreatedBy.ViewValue = DbNullValue;
                }
                CreatedBy.ViewCustomAttributes = "";

                // CreatedDateTime
                CreatedDateTime.ViewValue = CreatedDateTime.CurrentValue;
                CreatedDateTime.ViewValue = FormatDateTime(CreatedDateTime.ViewValue, CreatedDateTime.FormatPattern);
                CreatedDateTime.ViewCustomAttributes = "";

                // UpdatedBy
                curVal = ConvertToString(UpdatedBy.CurrentValue);
                if (!Empty(curVal)) {
                    if (UpdatedBy.Lookup != null && IsDictionary(UpdatedBy.Lookup?.Options) && UpdatedBy.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                        UpdatedBy.ViewValue = UpdatedBy.LookupCacheOption(curVal);
                    } else { // Lookup from database // DN
                        filterWrk = SearchFilter("[UserID]", "=", UpdatedBy.CurrentValue, DataType.Number, "");
                        sqlWrk = UpdatedBy.Lookup?.GetSql(false, filterWrk, null, this, true, true);
                        rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                        if (rswrk?.Count > 0 && UpdatedBy.Lookup != null) { // Lookup values found
                            var listwrk = UpdatedBy.Lookup?.RenderViewRow(rswrk[0]);
                            UpdatedBy.ViewValue = UpdatedBy.HighlightLookup(ConvertToString(rswrk[0]), UpdatedBy.DisplayValue(listwrk));
                        } else {
                            UpdatedBy.ViewValue = FormatNumber(UpdatedBy.CurrentValue, UpdatedBy.FormatPattern);
                        }
                    }
                } else {
                    UpdatedBy.ViewValue = DbNullValue;
                }
                UpdatedBy.ViewCustomAttributes = "";

                // UpdatedDateTime
                UpdatedDateTime.ViewValue = UpdatedDateTime.CurrentValue;
                UpdatedDateTime.ViewValue = FormatDateTime(UpdatedDateTime.ViewValue, UpdatedDateTime.FormatPattern);
                UpdatedDateTime.ViewCustomAttributes = "";

                // ISO
                ISO.HrefValue = "";

                // Name
                _Name.HrefValue = "";

                // NiceName
                NiceName.HrefValue = "";

                // ISO3
                ISO3.HrefValue = "";

                // NumCode
                NumCode.HrefValue = "";

                // PhoneCode
                PhoneCode.HrefValue = "";
            } else if (RowType == RowType.Edit) {
                // ISO
                ISO.SetupEditAttributes();
                if (!ISO.Raw)
                    ISO.CurrentValue = HtmlDecode(ISO.CurrentValue);
                ISO.EditValue = HtmlEncode(ISO.CurrentValue);
                ISO.PlaceHolder = RemoveHtml(ISO.Caption);

                // Name
                _Name.SetupEditAttributes();
                if (!_Name.Raw)
                    _Name.CurrentValue = HtmlDecode(_Name.CurrentValue);
                _Name.EditValue = HtmlEncode(_Name.CurrentValue);
                _Name.PlaceHolder = RemoveHtml(_Name.Caption);

                // NiceName
                NiceName.SetupEditAttributes();
                if (!NiceName.Raw)
                    NiceName.CurrentValue = HtmlDecode(NiceName.CurrentValue);
                NiceName.EditValue = HtmlEncode(NiceName.CurrentValue);
                NiceName.PlaceHolder = RemoveHtml(NiceName.Caption);

                // ISO3
                ISO3.SetupEditAttributes();
                if (!ISO3.Raw)
                    ISO3.CurrentValue = HtmlDecode(ISO3.CurrentValue);
                ISO3.EditValue = HtmlEncode(ISO3.CurrentValue);
                ISO3.PlaceHolder = RemoveHtml(ISO3.Caption);

                // NumCode
                NumCode.SetupEditAttributes();
                NumCode.EditValue = NumCode.CurrentValue; // DN
                NumCode.PlaceHolder = RemoveHtml(NumCode.Caption);
                if (!Empty(NumCode.EditValue) && IsNumeric(NumCode.EditValue))
                    NumCode.EditValue = FormatNumber(NumCode.EditValue, NumCode.FormatPattern);

                // PhoneCode
                PhoneCode.SetupEditAttributes();
                PhoneCode.EditValue = PhoneCode.CurrentValue; // DN
                PhoneCode.PlaceHolder = RemoveHtml(PhoneCode.Caption);
                if (!Empty(PhoneCode.EditValue) && IsNumeric(PhoneCode.EditValue))
                    PhoneCode.EditValue = FormatNumber(PhoneCode.EditValue, PhoneCode.FormatPattern);

                // Edit refer script

                // ISO
                ISO.HrefValue = "";

                // Name
                _Name.HrefValue = "";

                // NiceName
                NiceName.HrefValue = "";

                // ISO3
                ISO3.HrefValue = "";

                // NumCode
                NumCode.HrefValue = "";

                // PhoneCode
                PhoneCode.HrefValue = "";
            }
            if (RowType == RowType.Add || RowType == RowType.Edit || RowType == RowType.Search) // Add/Edit/Search row
                SetupFieldTitles();

            // Call Row Rendered event
            if (RowType != RowType.AggregateInit)
                RowRendered();
        }
        #pragma warning restore 1998

        #pragma warning disable 1998
        // Validate form
        protected async Task<bool> ValidateForm() {
            // Check if validation required
            if (!Config.ServerValidate)
                return true;
            bool validateForm = true;
            if (ISO.Required) {
                if (!ISO.IsDetailKey && Empty(ISO.FormValue)) {
                    ISO.AddErrorMessage(ConvertToString(ISO.RequiredErrorMessage).Replace("%s", ISO.Caption));
                }
            }
            if (_Name.Required) {
                if (!_Name.IsDetailKey && Empty(_Name.FormValue)) {
                    _Name.AddErrorMessage(ConvertToString(_Name.RequiredErrorMessage).Replace("%s", _Name.Caption));
                }
            }
            if (NiceName.Required) {
                if (!NiceName.IsDetailKey && Empty(NiceName.FormValue)) {
                    NiceName.AddErrorMessage(ConvertToString(NiceName.RequiredErrorMessage).Replace("%s", NiceName.Caption));
                }
            }
            if (ISO3.Required) {
                if (!ISO3.IsDetailKey && Empty(ISO3.FormValue)) {
                    ISO3.AddErrorMessage(ConvertToString(ISO3.RequiredErrorMessage).Replace("%s", ISO3.Caption));
                }
            }
            if (NumCode.Required) {
                if (!NumCode.IsDetailKey && Empty(NumCode.FormValue)) {
                    NumCode.AddErrorMessage(ConvertToString(NumCode.RequiredErrorMessage).Replace("%s", NumCode.Caption));
                }
            }
            if (!CheckInteger(NumCode.FormValue)) {
                NumCode.AddErrorMessage(NumCode.GetErrorMessage(false));
            }
            if (PhoneCode.Required) {
                if (!PhoneCode.IsDetailKey && Empty(PhoneCode.FormValue)) {
                    PhoneCode.AddErrorMessage(ConvertToString(PhoneCode.RequiredErrorMessage).Replace("%s", PhoneCode.Caption));
                }
            }
            if (!CheckInteger(PhoneCode.FormValue)) {
                PhoneCode.AddErrorMessage(PhoneCode.GetErrorMessage(false));
            }

            // Return validate result
            validateForm = validateForm && !HasInvalidFields();

            // Call Form CustomValidate event
            string formCustomError = "";
            validateForm = validateForm && FormCustomValidate(ref formCustomError);
            if (!Empty(formCustomError))
                FailureMessage = formCustomError;
            return validateForm;
        }
        #pragma warning restore 1998

        // Update record based on key values
        #pragma warning disable 168, 219

        protected async Task<JsonBoolResult> EditRow() { // DN
            bool result = false;
            Dictionary<string, object> rsold;
            string oldKeyFilter = GetRecordFilter();
            string filter = ApplyUserIDFilters(oldKeyFilter);

            // Load old row
            CurrentFilter = filter;
            string sql = CurrentSql;
            try {
                using var rsedit = await Connection.GetDataReaderAsync(sql);
                if (rsedit == null || !await rsedit.ReadAsync()) {
                    FailureMessage = Language.Phrase("NoRecord"); // Set no record message
                    return JsonBoolResult.FalseResult;
                }
                rsold = Connection.GetRow(rsedit);
                LoadDbValues(rsold);
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }

            // Set new row
            Dictionary<string, object> rsnew = new ();

            // ISO
            ISO.SetDbValue(rsnew, ISO.CurrentValue, ISO.ReadOnly);

            // Name
            _Name.SetDbValue(rsnew, _Name.CurrentValue, _Name.ReadOnly);

            // NiceName
            NiceName.SetDbValue(rsnew, NiceName.CurrentValue, NiceName.ReadOnly);

            // ISO3
            ISO3.SetDbValue(rsnew, ISO3.CurrentValue, ISO3.ReadOnly);

            // NumCode
            NumCode.SetDbValue(rsnew, NumCode.CurrentValue, NumCode.ReadOnly);

            // PhoneCode
            PhoneCode.SetDbValue(rsnew, PhoneCode.CurrentValue, PhoneCode.ReadOnly);

            // Update current values
            SetCurrentValues(rsnew);

            // Call Row Updating event
            bool updateRow = RowUpdating(rsold, rsnew);
            if (updateRow) {
                try {
                    if (rsnew.Count > 0)
                        result = await UpdateAsync(rsnew, null, rsold) > 0;
                    else
                        result = true;
                    if (result) {
                    }
                } catch (Exception e) {
                    if (Config.Debug)
                        throw;
                    FailureMessage = e.Message;
                    return JsonBoolResult.FalseResult;
                }
            } else {
                if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {
                    // Use the message, do nothing
                } else if (!Empty(CancelMessage)) {
                    FailureMessage = CancelMessage;
                    CancelMessage = "";
                } else {
                    FailureMessage = Language.Phrase("UpdateCancelled");
                }
                result = false;
            }

            // Call Row Updated event
            if (result)
                RowUpdated(rsold, rsnew);

            // Write JSON for API request
            Dictionary<string, object> d = new ();
            d.Add("success", result);
            if (IsJsonResponse() && result) {
                if (GetRecordFromDictionary(rsnew) is var row && row != null) {
                    string table = TableVar;
                    d.Add(table, row);
                }
                d.Add("action", Config.ApiEditAction);
                d.Add("version", Config.ProductVersion);
                return new JsonBoolResult(d, true);
            }
            return new JsonBoolResult(d, result);
        }

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("countrieslist")), "", TableVar, true);
            string pageId = "edit";
            breadcrumb.Add("edit", pageId, url);
            CurrentBreadcrumb = breadcrumb;
        }

        // Setup lookup options
        public async Task SetupLookupOptions(DbField fld)
        {
            if (fld.Lookup == null)
                return;
            Func<string>? lookupFilter = null;
            dynamic conn = Connection;
            if (fld.Lookup.Options.Count is int c && c == 0) {
                // Always call to Lookup.GetSql so that user can setup Lookup.Options in Lookup Selecting server event
                var sql = fld.Lookup.GetSql(false, "", lookupFilter, this);

                // Set up lookup cache
                if (!fld.HasLookupOptions && fld.UseLookupCache && !Empty(sql) && fld.Lookup.ParentFields.Count == 0 && fld.Lookup.Options.Count == 0) {
                    int totalCnt = await TryGetRecordCountAsync(sql, conn);
                    if (totalCnt > fld.LookupCacheCount) // Total count > cache count, do not cache
                        return;
                    var dict = new Dictionary<string, Dictionary<string, object>>();
                    var values = new List<object>();
                    List<Dictionary<string, object>> rs = await conn.GetRowsAsync(sql);
                    if (rs != null) {
                        for (int i = 0; i < rs.Count; i++) {
                            var row = rs[i];
                            row = fld.Lookup?.RenderViewRow(row, Resolve(fld.Lookup.LinkTable));
                            string key = row?.Values.First()?.ToString() ?? String.Empty;
                            if (!dict.ContainsKey(key) && row != null)
                                dict.Add(key, row);
                        }
                    }
                    fld.Lookup?.SetOptions(dict);
                }
            }
        }

        // Close recordset
        public void CloseRecordset()
        {
            using (Recordset) {} // Dispose
        }

        // Set up starting record parameters
        public void SetupStartRecord()
        {
            // Exit if DisplayRecords = 0
            if (DisplayRecords == 0)
                return;
            string pageNo = Get(Config.TablePageNumber);
            string startRec = Get(Config.TableStartRec);
            bool infiniteScroll = false;
            string recordNo = !Empty(pageNo) ? pageNo : startRec; // Record number = page number or start record
            if (!Empty(recordNo) && IsNumeric(recordNo))
                StartRecord = ConvertToInt(recordNo);
            else
                StartRecord = StartRecordNumber;

            // Check if correct start record counter
            if (StartRecord <= 0) // Avoid invalid start record counter
                StartRecord = 1; // Reset start record counter
            else if (StartRecord > TotalRecords) // Avoid starting record > total records
                StartRecord = ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1; // Point to last page first record
            else if ((StartRecord - 1) % DisplayRecords != 0)
                StartRecord = ((StartRecord - 1) / DisplayRecords) * DisplayRecords + 1; // Point to page boundary
            if (!infiniteScroll)
                StartRecordNumber = StartRecord;
        }

        // Get page count
        public int PageCount
        {
            get {
                return ConvertToInt(Math.Ceiling((double)TotalRecords / DisplayRecords));
            }
        }

        // Page Load event
        public virtual void PageLoad() {
            //Log("Page Load");
        }

        // Page Unload event
        public virtual void PageUnload() {
            //Log("Page Unload");
        }

        // Page Redirecting event
        public virtual void PageRedirecting(ref string url) {
            //url = newurl;
        }

        // Message Showing event
        // type = ""|"success"|"failure"|"warning"
        public virtual void MessageShowing(ref string msg, string type) {
            // Note: Do not change msg outside the following 4 cases.
            if (type == "success") {
                //msg = "your success message";
            } else if (type == "failure") {
                //msg = "your failure message";
            } else if (type == "warning") {
                //msg = "your warning message";
            } else {
                //msg = "your message";
            }
        }

        // Page Load event
        public virtual void PageRender() {
            //Log("Page Render");
        }

        // Page Data Rendering event
        public virtual void PageDataRendering(ref string header) {
            // Example:
            //header = "your header";
        }

        // Page Data Rendered event
        public virtual void PageDataRendered(ref string footer) {
            // Example:
            //footer = "your footer";
        }

        // Page Breaking event
        public void PageBreaking(ref bool brk, ref string content) {
            // Example:
            //	brk = false; // Skip page break, or
            //	content = "<div style=\"page-break-after:always;\">&nbsp;</div>"; // Modify page break content
        }

        // Form Custom Validate event
        public virtual bool FormCustomValidate(ref string customError) {
            //Return error message in customError
            return true;
        }
    } // End page class
} // End Partial class
