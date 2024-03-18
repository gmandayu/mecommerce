namespace ASPNETMaker2023.Models;

// Partial class
public partial class mecommerce {
    /// <summary>
    /// customersAdd
    /// </summary>
    public static CustomersAdd customersAdd
    {
        get => HttpData.Get<CustomersAdd>("customersAdd")!;
        set => HttpData["customersAdd"] = value;
    }

    /// <summary>
    /// Page class for Customers
    /// </summary>
    public class CustomersAdd : CustomersAddBase
    {
        // Constructor
        public CustomersAdd(Controller controller) : base(controller)
        {
        }

        // Constructor
        public CustomersAdd() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class CustomersAddBase : Customers
    {
        // Page ID
        public string PageID = "add";

        // Project ID
        public string ProjectID = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}";

        // Table name
        public string TableName { get; set; } = "Customers";

        // Page object name
        public string PageObjName = "customersAdd";

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
        public CustomersAddBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-add-table";

            // Language object
            Language = ResolveLanguage();

            // Table object (customers)
            if (customers == null || customers is Customers)
                customers = this;

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
        public string PageName => "customersadd";

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
            CustomerID.Visible = false;
            FirstName.SetVisibility();
            MiddleName.SetVisibility();
            LastName.SetVisibility();
            Gender.SetVisibility();
            PlaceOfBirth.SetVisibility();
            DateOfBirth.SetVisibility();
            PrimaryAddress.SetVisibility();
            PrimaryAddressCity.SetVisibility();
            PrimaryAddressPostCode.SetVisibility();
            PrimaryAddressCountryID.SetVisibility();
            AlternativeAddress.SetVisibility();
            AlternativeAddressCity.SetVisibility();
            AlternativeAddressPostCode.SetVisibility();
            AlternativeAddressCountryID.SetVisibility();
            MobileNumber.SetVisibility();
            UserID.SetVisibility();
            Status.SetVisibility();
            CreatedBy.Visible = false;
            CreatedDateTime.Visible = false;
            UpdatedBy.Visible = false;
            UpdatedDateTime.Visible = false;
        }

        // Constructor
        public CustomersAddBase(Controller? controller = null): this() { // DN
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
                            result.Add("view", pageName == "customersview" ? "1" : "0"); // If View page, no primary button
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
            key += UrlEncode(ConvertToString(dict.ContainsKey("CustomerID") ? dict["CustomerID"] : CustomerID.CurrentValue));
            return key;
        }

        // Hide fields for Add/Edit
        protected void HideFieldsForAddEdit() {
            if (IsAdd || IsCopy || IsGridAdd)
                CustomerID.Visible = false;
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

        // Properties
        public string FormClassName = "ew-form ew-add-form";

        public bool IsModal = false;

        public bool IsMobileOrModal = false;

        public string DbMasterFilter = "";

        public string DbDetailFilter = "";

        public int StartRecord;

        public DbDataReader? Recordset = null; // Reserved // DN

        public bool CopyRecord;

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

            // Set up lookup cache
            await SetupLookupOptions(Gender);
            await SetupLookupOptions(PrimaryAddressCountryID);
            await SetupLookupOptions(AlternativeAddressCountryID);

            // Load default values for add
            LoadDefaultValues();

            // Check modal
            if (IsModal)
                SkipHeaderFooter = true;
            IsMobileOrModal = IsMobile() || IsModal;
            bool postBack = false;
            StringValues sv;

            // Set up current action
            if (IsApi()) {
                CurrentAction = "insert"; // Add record directly
                postBack = true;
            } else if (!Empty(Post("action"))) {
                CurrentAction = Post("action"); // Get form action
                if (Post(OldKeyName, out sv))
                    SetKey(sv.ToString());
                postBack = true;
            } else {
                // Load key from QueryString
                string[] keyValues = {};
                object? rv;
                if (RouteValues.TryGetValue("key", out object? k))
                    keyValues = ConvertToString(k).Split('/'); // For Copy page
                if (RouteValues.TryGetValue("CustomerID", out rv)) { // DN
                    CustomerID.QueryValue = UrlDecode(rv); // DN
                } else if (Get("CustomerID", out sv)) {
                    CustomerID.QueryValue = sv.ToString();
                }
                OldKey = GetKey(true); // Get from CurrentValue
                CopyRecord = !Empty(OldKey);
                if (CopyRecord) {
                    CurrentAction = "copy"; // Copy record
                    SetKey(OldKey); // Set up record key
                } else {
                    CurrentAction = "show"; // Display blank record
                }
            }

            // Load old record / default values
            var rsold = await LoadOldRecord();

            // Load form values
            if (postBack) {
                await LoadFormValues(); // Load form values
            }

            // Validate form if post back
            if (postBack) {
                if (!await ValidateForm()) {
                    EventCancelled = true; // Event cancelled
                    RestoreFormValues(); // Restore form values
                    if (IsApi())
                        return Terminate();
                    else
                        CurrentAction = "show"; // Form error, reset action
                }
            }

            // Perform current action
            switch (CurrentAction) {
                case "copy": // Copy an existing record
                    if (rsold == null) { // Record not loaded
                        if (Empty(FailureMessage))
                            FailureMessage = Language.Phrase("NoRecord"); // No record found
                        return Terminate("customerslist"); // No matching record, return to List page // DN
                    }
                    break;
                case "insert": // Add new record // DN
                    SendEmail = true; // Send email on add success
                    var res = await AddRow(rsold);
                    if (res) { // Add successful
                        if (Empty(SuccessMessage) && Post("addopt") != "1") // Skip success message for addopt (done in JavaScript)
                            SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
                        string returnUrl = "";
                        returnUrl = ReturnUrl;
                        if (GetPageName(returnUrl) == "customerslist")
                            returnUrl = AddMasterUrl(ListUrl); // List page, return to List page with correct master key if necessary
                        else if (GetPageName(returnUrl) == "customersview")
                            returnUrl = ViewUrl; // View page, return to View page with key URL directly

                        // Handle UseAjaxActions
                        if (IsModal && UseAjaxActions) {
                            IsModal = false;
                            if (GetPageName(returnUrl) != "customerslist") {
                                TempData["Return-Url"] = returnUrl; // Save return URL
                                returnUrl = "customerslist"; // Return list page content
                            }
                        }
                        if (IsJsonResponse()) { // Return to caller
                            ClearMessages(); // Clear messages for Json response
                            return res;
                        } else {
                            return Terminate(returnUrl);
                        }
                    } else if (IsApi()) { // API request, return
                        return Terminate();
                    } else {
                        EventCancelled = true; // Event cancelled
                        RestoreFormValues(); // Add failed, restore form values
                    }
                    break;
            }

            // Set up Breadcrumb
            SetupBreadcrumb();

            // Render row based on row type
            RowType = RowType.Add; // Render add type

            // Render row
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
                customersAdd?.PageRender();
            }
            return PageResult();
        }

        // Confirm page
        public bool ConfirmPage = false; // DN

        #pragma warning disable 1998
        // Get upload files
        public async Task GetUploadFiles()
        {
            // Get upload data
        }
        #pragma warning restore 1998

        // Load default values
        protected void LoadDefaultValues() {
        }

        #pragma warning disable 1998
        // Load form values
        protected async Task LoadFormValues() {
            if (CurrentForm == null)
                return;
            bool validate = !Config.ServerValidate;
            string val;

            // Check field name 'FirstName' before field var 'x_FirstName'
            val = CurrentForm.HasValue("FirstName") ? CurrentForm.GetValue("FirstName") : CurrentForm.GetValue("x_FirstName");
            if (!FirstName.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("FirstName") && !CurrentForm.HasValue("x_FirstName")) // DN
                    FirstName.Visible = false; // Disable update for API request
                else
                    FirstName.SetFormValue(val);
            }

            // Check field name 'MiddleName' before field var 'x_MiddleName'
            val = CurrentForm.HasValue("MiddleName") ? CurrentForm.GetValue("MiddleName") : CurrentForm.GetValue("x_MiddleName");
            if (!MiddleName.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("MiddleName") && !CurrentForm.HasValue("x_MiddleName")) // DN
                    MiddleName.Visible = false; // Disable update for API request
                else
                    MiddleName.SetFormValue(val);
            }

            // Check field name 'LastName' before field var 'x_LastName'
            val = CurrentForm.HasValue("LastName") ? CurrentForm.GetValue("LastName") : CurrentForm.GetValue("x_LastName");
            if (!LastName.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("LastName") && !CurrentForm.HasValue("x_LastName")) // DN
                    LastName.Visible = false; // Disable update for API request
                else
                    LastName.SetFormValue(val);
            }

            // Check field name 'Gender' before field var 'x_Gender'
            val = CurrentForm.HasValue("Gender") ? CurrentForm.GetValue("Gender") : CurrentForm.GetValue("x_Gender");
            if (!Gender.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Gender") && !CurrentForm.HasValue("x_Gender")) // DN
                    Gender.Visible = false; // Disable update for API request
                else
                    Gender.SetFormValue(val);
            }

            // Check field name 'PlaceOfBirth' before field var 'x_PlaceOfBirth'
            val = CurrentForm.HasValue("PlaceOfBirth") ? CurrentForm.GetValue("PlaceOfBirth") : CurrentForm.GetValue("x_PlaceOfBirth");
            if (!PlaceOfBirth.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("PlaceOfBirth") && !CurrentForm.HasValue("x_PlaceOfBirth")) // DN
                    PlaceOfBirth.Visible = false; // Disable update for API request
                else
                    PlaceOfBirth.SetFormValue(val);
            }

            // Check field name 'DateOfBirth' before field var 'x_DateOfBirth'
            val = CurrentForm.HasValue("DateOfBirth") ? CurrentForm.GetValue("DateOfBirth") : CurrentForm.GetValue("x_DateOfBirth");
            if (!DateOfBirth.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("DateOfBirth") && !CurrentForm.HasValue("x_DateOfBirth")) // DN
                    DateOfBirth.Visible = false; // Disable update for API request
                else
                    DateOfBirth.SetFormValue(val, true, validate);
                DateOfBirth.CurrentValue = UnformatDateTime(DateOfBirth.CurrentValue, DateOfBirth.FormatPattern);
            }

            // Check field name 'PrimaryAddress' before field var 'x_PrimaryAddress'
            val = CurrentForm.HasValue("PrimaryAddress") ? CurrentForm.GetValue("PrimaryAddress") : CurrentForm.GetValue("x_PrimaryAddress");
            if (!PrimaryAddress.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("PrimaryAddress") && !CurrentForm.HasValue("x_PrimaryAddress")) // DN
                    PrimaryAddress.Visible = false; // Disable update for API request
                else
                    PrimaryAddress.SetFormValue(val);
            }

            // Check field name 'PrimaryAddressCity' before field var 'x_PrimaryAddressCity'
            val = CurrentForm.HasValue("PrimaryAddressCity") ? CurrentForm.GetValue("PrimaryAddressCity") : CurrentForm.GetValue("x_PrimaryAddressCity");
            if (!PrimaryAddressCity.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("PrimaryAddressCity") && !CurrentForm.HasValue("x_PrimaryAddressCity")) // DN
                    PrimaryAddressCity.Visible = false; // Disable update for API request
                else
                    PrimaryAddressCity.SetFormValue(val);
            }

            // Check field name 'PrimaryAddressPostCode' before field var 'x_PrimaryAddressPostCode'
            val = CurrentForm.HasValue("PrimaryAddressPostCode") ? CurrentForm.GetValue("PrimaryAddressPostCode") : CurrentForm.GetValue("x_PrimaryAddressPostCode");
            if (!PrimaryAddressPostCode.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("PrimaryAddressPostCode") && !CurrentForm.HasValue("x_PrimaryAddressPostCode")) // DN
                    PrimaryAddressPostCode.Visible = false; // Disable update for API request
                else
                    PrimaryAddressPostCode.SetFormValue(val);
            }

            // Check field name 'PrimaryAddressCountryID' before field var 'x_PrimaryAddressCountryID'
            val = CurrentForm.HasValue("PrimaryAddressCountryID") ? CurrentForm.GetValue("PrimaryAddressCountryID") : CurrentForm.GetValue("x_PrimaryAddressCountryID");
            if (!PrimaryAddressCountryID.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("PrimaryAddressCountryID") && !CurrentForm.HasValue("x_PrimaryAddressCountryID")) // DN
                    PrimaryAddressCountryID.Visible = false; // Disable update for API request
                else
                    PrimaryAddressCountryID.SetFormValue(val);
            }

            // Check field name 'AlternativeAddress' before field var 'x_AlternativeAddress'
            val = CurrentForm.HasValue("AlternativeAddress") ? CurrentForm.GetValue("AlternativeAddress") : CurrentForm.GetValue("x_AlternativeAddress");
            if (!AlternativeAddress.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("AlternativeAddress") && !CurrentForm.HasValue("x_AlternativeAddress")) // DN
                    AlternativeAddress.Visible = false; // Disable update for API request
                else
                    AlternativeAddress.SetFormValue(val);
            }

            // Check field name 'AlternativeAddressCity' before field var 'x_AlternativeAddressCity'
            val = CurrentForm.HasValue("AlternativeAddressCity") ? CurrentForm.GetValue("AlternativeAddressCity") : CurrentForm.GetValue("x_AlternativeAddressCity");
            if (!AlternativeAddressCity.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("AlternativeAddressCity") && !CurrentForm.HasValue("x_AlternativeAddressCity")) // DN
                    AlternativeAddressCity.Visible = false; // Disable update for API request
                else
                    AlternativeAddressCity.SetFormValue(val);
            }

            // Check field name 'AlternativeAddressPostCode' before field var 'x_AlternativeAddressPostCode'
            val = CurrentForm.HasValue("AlternativeAddressPostCode") ? CurrentForm.GetValue("AlternativeAddressPostCode") : CurrentForm.GetValue("x_AlternativeAddressPostCode");
            if (!AlternativeAddressPostCode.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("AlternativeAddressPostCode") && !CurrentForm.HasValue("x_AlternativeAddressPostCode")) // DN
                    AlternativeAddressPostCode.Visible = false; // Disable update for API request
                else
                    AlternativeAddressPostCode.SetFormValue(val);
            }

            // Check field name 'AlternativeAddressCountryID' before field var 'x_AlternativeAddressCountryID'
            val = CurrentForm.HasValue("AlternativeAddressCountryID") ? CurrentForm.GetValue("AlternativeAddressCountryID") : CurrentForm.GetValue("x_AlternativeAddressCountryID");
            if (!AlternativeAddressCountryID.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("AlternativeAddressCountryID") && !CurrentForm.HasValue("x_AlternativeAddressCountryID")) // DN
                    AlternativeAddressCountryID.Visible = false; // Disable update for API request
                else
                    AlternativeAddressCountryID.SetFormValue(val);
            }

            // Check field name 'MobileNumber' before field var 'x_MobileNumber'
            val = CurrentForm.HasValue("MobileNumber") ? CurrentForm.GetValue("MobileNumber") : CurrentForm.GetValue("x_MobileNumber");
            if (!MobileNumber.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("MobileNumber") && !CurrentForm.HasValue("x_MobileNumber")) // DN
                    MobileNumber.Visible = false; // Disable update for API request
                else
                    MobileNumber.SetFormValue(val);
            }

            // Check field name 'UserID' before field var 'x_UserID'
            val = CurrentForm.HasValue("UserID") ? CurrentForm.GetValue("UserID") : CurrentForm.GetValue("x_UserID");
            if (!UserID.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("UserID") && !CurrentForm.HasValue("x_UserID")) // DN
                    UserID.Visible = false; // Disable update for API request
                else
                    UserID.SetFormValue(val);
            }

            // Check field name 'Status' before field var 'x_Status'
            val = CurrentForm.HasValue("Status") ? CurrentForm.GetValue("Status") : CurrentForm.GetValue("x_Status");
            if (!Status.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Status") && !CurrentForm.HasValue("x_Status")) // DN
                    Status.Visible = false; // Disable update for API request
                else
                    Status.SetFormValue(val);
            }

            // Check field name 'CustomerID' before field var 'x_CustomerID'
            val = CurrentForm.HasValue("CustomerID") ? CurrentForm.GetValue("CustomerID") : CurrentForm.GetValue("x_CustomerID");
        }
        #pragma warning restore 1998

        // Restore form values
        public void RestoreFormValues()
        {
            FirstName.CurrentValue = FirstName.FormValue;
            MiddleName.CurrentValue = MiddleName.FormValue;
            LastName.CurrentValue = LastName.FormValue;
            Gender.CurrentValue = Gender.FormValue;
            PlaceOfBirth.CurrentValue = PlaceOfBirth.FormValue;
            DateOfBirth.CurrentValue = DateOfBirth.FormValue;
            DateOfBirth.CurrentValue = UnformatDateTime(DateOfBirth.CurrentValue, DateOfBirth.FormatPattern);
            PrimaryAddress.CurrentValue = PrimaryAddress.FormValue;
            PrimaryAddressCity.CurrentValue = PrimaryAddressCity.FormValue;
            PrimaryAddressPostCode.CurrentValue = PrimaryAddressPostCode.FormValue;
            PrimaryAddressCountryID.CurrentValue = PrimaryAddressCountryID.FormValue;
            AlternativeAddress.CurrentValue = AlternativeAddress.FormValue;
            AlternativeAddressCity.CurrentValue = AlternativeAddressCity.FormValue;
            AlternativeAddressPostCode.CurrentValue = AlternativeAddressPostCode.FormValue;
            AlternativeAddressCountryID.CurrentValue = AlternativeAddressCountryID.FormValue;
            MobileNumber.CurrentValue = MobileNumber.FormValue;
            UserID.CurrentValue = UserID.FormValue;
            Status.CurrentValue = Status.FormValue;
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
            CustomerID.SetDbValue(row["CustomerID"]);
            FirstName.SetDbValue(row["FirstName"]);
            MiddleName.SetDbValue(row["MiddleName"]);
            LastName.SetDbValue(row["LastName"]);
            Gender.SetDbValue(row["Gender"]);
            PlaceOfBirth.SetDbValue(row["PlaceOfBirth"]);
            DateOfBirth.SetDbValue(row["DateOfBirth"]);
            PrimaryAddress.SetDbValue(row["PrimaryAddress"]);
            PrimaryAddressCity.SetDbValue(row["PrimaryAddressCity"]);
            PrimaryAddressPostCode.SetDbValue(row["PrimaryAddressPostCode"]);
            PrimaryAddressCountryID.SetDbValue(row["PrimaryAddressCountryID"]);
            AlternativeAddress.SetDbValue(row["AlternativeAddress"]);
            AlternativeAddressCity.SetDbValue(row["AlternativeAddressCity"]);
            AlternativeAddressPostCode.SetDbValue(row["AlternativeAddressPostCode"]);
            AlternativeAddressCountryID.SetDbValue(row["AlternativeAddressCountryID"]);
            MobileNumber.SetDbValue(row["MobileNumber"]);
            UserID.SetDbValue(row["UserID"]);
            Status.SetDbValue(row["Status"]);
            CreatedBy.SetDbValue(row["CreatedBy"]);
            CreatedDateTime.SetDbValue(row["CreatedDateTime"]);
            UpdatedBy.SetDbValue(row["UpdatedBy"]);
            UpdatedDateTime.SetDbValue(row["UpdatedDateTime"]);
        }
        #pragma warning restore 162, 168, 1998, 4014

        // Return a row with default values
        protected Dictionary<string, object> NewRow() {
            var row = new Dictionary<string, object>();
            row.Add("CustomerID", CustomerID.DefaultValue ?? DbNullValue); // DN
            row.Add("FirstName", FirstName.DefaultValue ?? DbNullValue); // DN
            row.Add("MiddleName", MiddleName.DefaultValue ?? DbNullValue); // DN
            row.Add("LastName", LastName.DefaultValue ?? DbNullValue); // DN
            row.Add("Gender", Gender.DefaultValue ?? DbNullValue); // DN
            row.Add("PlaceOfBirth", PlaceOfBirth.DefaultValue ?? DbNullValue); // DN
            row.Add("DateOfBirth", DateOfBirth.DefaultValue ?? DbNullValue); // DN
            row.Add("PrimaryAddress", PrimaryAddress.DefaultValue ?? DbNullValue); // DN
            row.Add("PrimaryAddressCity", PrimaryAddressCity.DefaultValue ?? DbNullValue); // DN
            row.Add("PrimaryAddressPostCode", PrimaryAddressPostCode.DefaultValue ?? DbNullValue); // DN
            row.Add("PrimaryAddressCountryID", PrimaryAddressCountryID.DefaultValue ?? DbNullValue); // DN
            row.Add("AlternativeAddress", AlternativeAddress.DefaultValue ?? DbNullValue); // DN
            row.Add("AlternativeAddressCity", AlternativeAddressCity.DefaultValue ?? DbNullValue); // DN
            row.Add("AlternativeAddressPostCode", AlternativeAddressPostCode.DefaultValue ?? DbNullValue); // DN
            row.Add("AlternativeAddressCountryID", AlternativeAddressCountryID.DefaultValue ?? DbNullValue); // DN
            row.Add("MobileNumber", MobileNumber.DefaultValue ?? DbNullValue); // DN
            row.Add("UserID", UserID.DefaultValue ?? DbNullValue); // DN
            row.Add("Status", Status.DefaultValue ?? DbNullValue); // DN
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

            // CustomerID
            CustomerID.RowCssClass = "row";

            // FirstName
            FirstName.RowCssClass = "row";

            // MiddleName
            MiddleName.RowCssClass = "row";

            // LastName
            LastName.RowCssClass = "row";

            // Gender
            Gender.RowCssClass = "row";

            // PlaceOfBirth
            PlaceOfBirth.RowCssClass = "row";

            // DateOfBirth
            DateOfBirth.RowCssClass = "row";

            // PrimaryAddress
            PrimaryAddress.RowCssClass = "row";

            // PrimaryAddressCity
            PrimaryAddressCity.RowCssClass = "row";

            // PrimaryAddressPostCode
            PrimaryAddressPostCode.RowCssClass = "row";

            // PrimaryAddressCountryID
            PrimaryAddressCountryID.RowCssClass = "row";

            // AlternativeAddress
            AlternativeAddress.RowCssClass = "row";

            // AlternativeAddressCity
            AlternativeAddressCity.RowCssClass = "row";

            // AlternativeAddressPostCode
            AlternativeAddressPostCode.RowCssClass = "row";

            // AlternativeAddressCountryID
            AlternativeAddressCountryID.RowCssClass = "row";

            // MobileNumber
            MobileNumber.RowCssClass = "row";

            // UserID
            UserID.RowCssClass = "row";

            // Status
            Status.RowCssClass = "row";

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
                // FirstName
                FirstName.ViewValue = ConvertToString(FirstName.CurrentValue); // DN
                FirstName.ViewCustomAttributes = "";

                // MiddleName
                MiddleName.ViewValue = ConvertToString(MiddleName.CurrentValue); // DN
                MiddleName.ViewCustomAttributes = "";

                // LastName
                LastName.ViewValue = ConvertToString(LastName.CurrentValue); // DN
                LastName.ViewCustomAttributes = "";

                // Gender
                if (!Empty(Gender.CurrentValue)) {
                    Gender.ViewValue = Gender.HighlightLookup(ConvertToString(Gender.CurrentValue), Gender.OptionCaption(ConvertToString(Gender.CurrentValue)));
                } else {
                    Gender.ViewValue = DbNullValue;
                }
                Gender.ViewCustomAttributes = "";

                // PlaceOfBirth
                PlaceOfBirth.ViewValue = ConvertToString(PlaceOfBirth.CurrentValue); // DN
                PlaceOfBirth.ViewCustomAttributes = "";

                // DateOfBirth
                DateOfBirth.ViewValue = DateOfBirth.CurrentValue;
                DateOfBirth.ViewValue = FormatDateTime(DateOfBirth.ViewValue, DateOfBirth.FormatPattern);
                DateOfBirth.ViewCustomAttributes = "";

                // PrimaryAddress
                PrimaryAddress.ViewValue = PrimaryAddress.CurrentValue;
                PrimaryAddress.ViewCustomAttributes = "";

                // PrimaryAddressCity
                PrimaryAddressCity.ViewValue = ConvertToString(PrimaryAddressCity.CurrentValue); // DN
                PrimaryAddressCity.ViewCustomAttributes = "";

                // PrimaryAddressPostCode
                PrimaryAddressPostCode.ViewValue = ConvertToString(PrimaryAddressPostCode.CurrentValue); // DN
                PrimaryAddressPostCode.ViewCustomAttributes = "";

                // PrimaryAddressCountryID
                curVal = ConvertToString(PrimaryAddressCountryID.CurrentValue);
                if (!Empty(curVal)) {
                    if (PrimaryAddressCountryID.Lookup != null && IsDictionary(PrimaryAddressCountryID.Lookup?.Options) && PrimaryAddressCountryID.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                        PrimaryAddressCountryID.ViewValue = PrimaryAddressCountryID.LookupCacheOption(curVal);
                    } else { // Lookup from database // DN
                        filterWrk = SearchFilter("[CountryID]", "=", PrimaryAddressCountryID.CurrentValue, DataType.Number, "");
                        sqlWrk = PrimaryAddressCountryID.Lookup?.GetSql(false, filterWrk, null, this, true, true);
                        rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                        if (rswrk?.Count > 0 && PrimaryAddressCountryID.Lookup != null) { // Lookup values found
                            var listwrk = PrimaryAddressCountryID.Lookup?.RenderViewRow(rswrk[0]);
                            PrimaryAddressCountryID.ViewValue = PrimaryAddressCountryID.HighlightLookup(ConvertToString(rswrk[0]), PrimaryAddressCountryID.DisplayValue(listwrk));
                        } else {
                            PrimaryAddressCountryID.ViewValue = FormatNumber(PrimaryAddressCountryID.CurrentValue, PrimaryAddressCountryID.FormatPattern);
                        }
                    }
                } else {
                    PrimaryAddressCountryID.ViewValue = DbNullValue;
                }
                PrimaryAddressCountryID.ViewCustomAttributes = "";

                // AlternativeAddress
                AlternativeAddress.ViewValue = AlternativeAddress.CurrentValue;
                AlternativeAddress.ViewCustomAttributes = "";

                // AlternativeAddressCity
                AlternativeAddressCity.ViewValue = ConvertToString(AlternativeAddressCity.CurrentValue); // DN
                AlternativeAddressCity.ViewCustomAttributes = "";

                // AlternativeAddressPostCode
                AlternativeAddressPostCode.ViewValue = ConvertToString(AlternativeAddressPostCode.CurrentValue); // DN
                AlternativeAddressPostCode.ViewCustomAttributes = "";

                // AlternativeAddressCountryID
                curVal = ConvertToString(AlternativeAddressCountryID.CurrentValue);
                if (!Empty(curVal)) {
                    if (AlternativeAddressCountryID.Lookup != null && IsDictionary(AlternativeAddressCountryID.Lookup?.Options) && AlternativeAddressCountryID.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                        AlternativeAddressCountryID.ViewValue = AlternativeAddressCountryID.LookupCacheOption(curVal);
                    } else { // Lookup from database // DN
                        filterWrk = SearchFilter("[CountryID]", "=", AlternativeAddressCountryID.CurrentValue, DataType.Number, "");
                        sqlWrk = AlternativeAddressCountryID.Lookup?.GetSql(false, filterWrk, null, this, true, true);
                        rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                        if (rswrk?.Count > 0 && AlternativeAddressCountryID.Lookup != null) { // Lookup values found
                            var listwrk = AlternativeAddressCountryID.Lookup?.RenderViewRow(rswrk[0]);
                            AlternativeAddressCountryID.ViewValue = AlternativeAddressCountryID.HighlightLookup(ConvertToString(rswrk[0]), AlternativeAddressCountryID.DisplayValue(listwrk));
                        } else {
                            AlternativeAddressCountryID.ViewValue = FormatNumber(AlternativeAddressCountryID.CurrentValue, AlternativeAddressCountryID.FormatPattern);
                        }
                    }
                } else {
                    AlternativeAddressCountryID.ViewValue = DbNullValue;
                }
                AlternativeAddressCountryID.ViewCustomAttributes = "";

                // MobileNumber
                MobileNumber.ViewValue = ConvertToString(MobileNumber.CurrentValue); // DN
                MobileNumber.ViewCustomAttributes = "";

                // UserID
                curVal = ConvertToString(UserID.CurrentValue);
                if (!Empty(curVal)) {
                    if (UserID.Lookup != null && IsDictionary(UserID.Lookup?.Options) && UserID.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                        UserID.ViewValue = UserID.LookupCacheOption(curVal);
                    } else { // Lookup from database // DN
                        filterWrk = SearchFilter("[UserID]", "=", UserID.CurrentValue, DataType.Number, "");
                        sqlWrk = UserID.Lookup?.GetSql(false, filterWrk, null, this, true, true);
                        rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                        if (rswrk?.Count > 0 && UserID.Lookup != null) { // Lookup values found
                            var listwrk = UserID.Lookup?.RenderViewRow(rswrk[0]);
                            UserID.ViewValue = UserID.HighlightLookup(ConvertToString(rswrk[0]), UserID.DisplayValue(listwrk));
                        } else {
                            UserID.ViewValue = FormatNumber(UserID.CurrentValue, UserID.FormatPattern);
                        }
                    }
                } else {
                    UserID.ViewValue = DbNullValue;
                }
                UserID.ViewCustomAttributes = "";

                // Status
                Status.ViewValue = ConvertToString(Status.CurrentValue); // DN
                Status.ViewCustomAttributes = "";

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

                // FirstName
                FirstName.HrefValue = "";

                // MiddleName
                MiddleName.HrefValue = "";

                // LastName
                LastName.HrefValue = "";

                // Gender
                Gender.HrefValue = "";

                // PlaceOfBirth
                PlaceOfBirth.HrefValue = "";

                // DateOfBirth
                DateOfBirth.HrefValue = "";

                // PrimaryAddress
                PrimaryAddress.HrefValue = "";

                // PrimaryAddressCity
                PrimaryAddressCity.HrefValue = "";

                // PrimaryAddressPostCode
                PrimaryAddressPostCode.HrefValue = "";

                // PrimaryAddressCountryID
                PrimaryAddressCountryID.HrefValue = "";

                // AlternativeAddress
                AlternativeAddress.HrefValue = "";

                // AlternativeAddressCity
                AlternativeAddressCity.HrefValue = "";

                // AlternativeAddressPostCode
                AlternativeAddressPostCode.HrefValue = "";

                // AlternativeAddressCountryID
                AlternativeAddressCountryID.HrefValue = "";

                // MobileNumber
                MobileNumber.HrefValue = "";

                // UserID
                UserID.HrefValue = "";

                // Status
                Status.HrefValue = "";
            } else if (RowType == RowType.Add) {
                // FirstName
                FirstName.SetupEditAttributes();
                if (!FirstName.Raw)
                    FirstName.CurrentValue = HtmlDecode(FirstName.CurrentValue);
                FirstName.EditValue = HtmlEncode(FirstName.CurrentValue);
                FirstName.PlaceHolder = RemoveHtml(FirstName.Caption);

                // MiddleName
                MiddleName.SetupEditAttributes();
                if (!MiddleName.Raw)
                    MiddleName.CurrentValue = HtmlDecode(MiddleName.CurrentValue);
                MiddleName.EditValue = HtmlEncode(MiddleName.CurrentValue);
                MiddleName.PlaceHolder = RemoveHtml(MiddleName.Caption);

                // LastName
                LastName.SetupEditAttributes();
                if (!LastName.Raw)
                    LastName.CurrentValue = HtmlDecode(LastName.CurrentValue);
                LastName.EditValue = HtmlEncode(LastName.CurrentValue);
                LastName.PlaceHolder = RemoveHtml(LastName.Caption);

                // Gender
                Gender.SetupEditAttributes();
                Gender.EditValue = Gender.Options(true);
                Gender.PlaceHolder = RemoveHtml(Gender.Caption);

                // PlaceOfBirth
                PlaceOfBirth.SetupEditAttributes();
                if (!PlaceOfBirth.Raw)
                    PlaceOfBirth.CurrentValue = HtmlDecode(PlaceOfBirth.CurrentValue);
                PlaceOfBirth.EditValue = HtmlEncode(PlaceOfBirth.CurrentValue);
                PlaceOfBirth.PlaceHolder = RemoveHtml(PlaceOfBirth.Caption);

                // DateOfBirth
                DateOfBirth.SetupEditAttributes();
                DateOfBirth.EditValue = FormatDateTime(DateOfBirth.CurrentValue, DateOfBirth.FormatPattern); // DN
                DateOfBirth.PlaceHolder = RemoveHtml(DateOfBirth.Caption);

                // PrimaryAddress
                PrimaryAddress.SetupEditAttributes();
                PrimaryAddress.EditValue = PrimaryAddress.CurrentValue; // DN
                PrimaryAddress.PlaceHolder = RemoveHtml(PrimaryAddress.Caption);

                // PrimaryAddressCity
                PrimaryAddressCity.SetupEditAttributes();
                if (!PrimaryAddressCity.Raw)
                    PrimaryAddressCity.CurrentValue = HtmlDecode(PrimaryAddressCity.CurrentValue);
                PrimaryAddressCity.EditValue = HtmlEncode(PrimaryAddressCity.CurrentValue);
                PrimaryAddressCity.PlaceHolder = RemoveHtml(PrimaryAddressCity.Caption);

                // PrimaryAddressPostCode
                PrimaryAddressPostCode.SetupEditAttributes();
                if (!PrimaryAddressPostCode.Raw)
                    PrimaryAddressPostCode.CurrentValue = HtmlDecode(PrimaryAddressPostCode.CurrentValue);
                PrimaryAddressPostCode.EditValue = HtmlEncode(PrimaryAddressPostCode.CurrentValue);
                PrimaryAddressPostCode.PlaceHolder = RemoveHtml(PrimaryAddressPostCode.Caption);

                // PrimaryAddressCountryID
                PrimaryAddressCountryID.SetupEditAttributes();
                curVal = ConvertToString(PrimaryAddressCountryID.CurrentValue)?.Trim() ?? "";
                if (PrimaryAddressCountryID.Lookup != null && IsDictionary(PrimaryAddressCountryID.Lookup?.Options) && PrimaryAddressCountryID.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                    PrimaryAddressCountryID.EditValue = PrimaryAddressCountryID.Lookup?.Options.Values.ToList();
                } else { // Lookup from database
                    if (curVal == "") {
                        filterWrk = "0=1";
                    } else {
                        filterWrk = SearchFilter("[CountryID]", "=", PrimaryAddressCountryID.CurrentValue, DataType.Number, "");
                    }
                    sqlWrk = PrimaryAddressCountryID.Lookup?.GetSql(true, filterWrk, null, this, false, true);
                    rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                    PrimaryAddressCountryID.EditValue = rswrk;
                }
                PrimaryAddressCountryID.PlaceHolder = RemoveHtml(PrimaryAddressCountryID.Caption);
                if (!Empty(PrimaryAddressCountryID.EditValue) && IsNumeric(PrimaryAddressCountryID.EditValue))
                    PrimaryAddressCountryID.EditValue = FormatNumber(PrimaryAddressCountryID.EditValue, PrimaryAddressCountryID.FormatPattern);

                // AlternativeAddress
                AlternativeAddress.SetupEditAttributes();
                AlternativeAddress.EditValue = AlternativeAddress.CurrentValue; // DN
                AlternativeAddress.PlaceHolder = RemoveHtml(AlternativeAddress.Caption);

                // AlternativeAddressCity
                AlternativeAddressCity.SetupEditAttributes();
                if (!AlternativeAddressCity.Raw)
                    AlternativeAddressCity.CurrentValue = HtmlDecode(AlternativeAddressCity.CurrentValue);
                AlternativeAddressCity.EditValue = HtmlEncode(AlternativeAddressCity.CurrentValue);
                AlternativeAddressCity.PlaceHolder = RemoveHtml(AlternativeAddressCity.Caption);

                // AlternativeAddressPostCode
                AlternativeAddressPostCode.SetupEditAttributes();
                if (!AlternativeAddressPostCode.Raw)
                    AlternativeAddressPostCode.CurrentValue = HtmlDecode(AlternativeAddressPostCode.CurrentValue);
                AlternativeAddressPostCode.EditValue = HtmlEncode(AlternativeAddressPostCode.CurrentValue);
                AlternativeAddressPostCode.PlaceHolder = RemoveHtml(AlternativeAddressPostCode.Caption);

                // AlternativeAddressCountryID
                AlternativeAddressCountryID.SetupEditAttributes();
                curVal = ConvertToString(AlternativeAddressCountryID.CurrentValue)?.Trim() ?? "";
                if (AlternativeAddressCountryID.Lookup != null && IsDictionary(AlternativeAddressCountryID.Lookup?.Options) && AlternativeAddressCountryID.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                    AlternativeAddressCountryID.EditValue = AlternativeAddressCountryID.Lookup?.Options.Values.ToList();
                } else { // Lookup from database
                    if (curVal == "") {
                        filterWrk = "0=1";
                    } else {
                        filterWrk = SearchFilter("[CountryID]", "=", AlternativeAddressCountryID.CurrentValue, DataType.Number, "");
                    }
                    sqlWrk = AlternativeAddressCountryID.Lookup?.GetSql(true, filterWrk, null, this, false, true);
                    rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                    AlternativeAddressCountryID.EditValue = rswrk;
                }
                AlternativeAddressCountryID.PlaceHolder = RemoveHtml(AlternativeAddressCountryID.Caption);
                if (!Empty(AlternativeAddressCountryID.EditValue) && IsNumeric(AlternativeAddressCountryID.EditValue))
                    AlternativeAddressCountryID.EditValue = FormatNumber(AlternativeAddressCountryID.EditValue, AlternativeAddressCountryID.FormatPattern);

                // MobileNumber
                MobileNumber.SetupEditAttributes();
                if (!MobileNumber.Raw)
                    MobileNumber.CurrentValue = HtmlDecode(MobileNumber.CurrentValue);
                MobileNumber.EditValue = HtmlEncode(MobileNumber.CurrentValue);
                MobileNumber.PlaceHolder = RemoveHtml(MobileNumber.Caption);

                // UserID
                UserID.SetupEditAttributes();
                curVal = ConvertToString(UserID.CurrentValue)?.Trim() ?? "";
                if (UserID.Lookup != null && IsDictionary(UserID.Lookup?.Options) && UserID.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                    UserID.EditValue = UserID.Lookup?.Options.Values.ToList();
                } else { // Lookup from database
                    if (curVal == "") {
                        filterWrk = "0=1";
                    } else {
                        filterWrk = SearchFilter("[UserID]", "=", UserID.CurrentValue, DataType.Number, "");
                    }
                    sqlWrk = UserID.Lookup?.GetSql(true, filterWrk, null, this, false, true);
                    rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                    UserID.EditValue = rswrk;
                }
                UserID.PlaceHolder = RemoveHtml(UserID.Caption);
                if (!Empty(UserID.EditValue) && IsNumeric(UserID.EditValue))
                    UserID.EditValue = FormatNumber(UserID.EditValue, UserID.FormatPattern);

                // Status
                Status.SetupEditAttributes();
                if (!Status.Raw)
                    Status.CurrentValue = HtmlDecode(Status.CurrentValue);
                Status.EditValue = HtmlEncode(Status.CurrentValue);
                Status.PlaceHolder = RemoveHtml(Status.Caption);

                // Add refer script

                // FirstName
                FirstName.HrefValue = "";

                // MiddleName
                MiddleName.HrefValue = "";

                // LastName
                LastName.HrefValue = "";

                // Gender
                Gender.HrefValue = "";

                // PlaceOfBirth
                PlaceOfBirth.HrefValue = "";

                // DateOfBirth
                DateOfBirth.HrefValue = "";

                // PrimaryAddress
                PrimaryAddress.HrefValue = "";

                // PrimaryAddressCity
                PrimaryAddressCity.HrefValue = "";

                // PrimaryAddressPostCode
                PrimaryAddressPostCode.HrefValue = "";

                // PrimaryAddressCountryID
                PrimaryAddressCountryID.HrefValue = "";

                // AlternativeAddress
                AlternativeAddress.HrefValue = "";

                // AlternativeAddressCity
                AlternativeAddressCity.HrefValue = "";

                // AlternativeAddressPostCode
                AlternativeAddressPostCode.HrefValue = "";

                // AlternativeAddressCountryID
                AlternativeAddressCountryID.HrefValue = "";

                // MobileNumber
                MobileNumber.HrefValue = "";

                // UserID
                UserID.HrefValue = "";

                // Status
                Status.HrefValue = "";
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
            if (FirstName.Required) {
                if (!FirstName.IsDetailKey && Empty(FirstName.FormValue)) {
                    FirstName.AddErrorMessage(ConvertToString(FirstName.RequiredErrorMessage).Replace("%s", FirstName.Caption));
                }
            }
            if (MiddleName.Required) {
                if (!MiddleName.IsDetailKey && Empty(MiddleName.FormValue)) {
                    MiddleName.AddErrorMessage(ConvertToString(MiddleName.RequiredErrorMessage).Replace("%s", MiddleName.Caption));
                }
            }
            if (LastName.Required) {
                if (!LastName.IsDetailKey && Empty(LastName.FormValue)) {
                    LastName.AddErrorMessage(ConvertToString(LastName.RequiredErrorMessage).Replace("%s", LastName.Caption));
                }
            }
            if (Gender.Required) {
                if (!Gender.IsDetailKey && Empty(Gender.FormValue)) {
                    Gender.AddErrorMessage(ConvertToString(Gender.RequiredErrorMessage).Replace("%s", Gender.Caption));
                }
            }
            if (PlaceOfBirth.Required) {
                if (!PlaceOfBirth.IsDetailKey && Empty(PlaceOfBirth.FormValue)) {
                    PlaceOfBirth.AddErrorMessage(ConvertToString(PlaceOfBirth.RequiredErrorMessage).Replace("%s", PlaceOfBirth.Caption));
                }
            }
            if (DateOfBirth.Required) {
                if (!DateOfBirth.IsDetailKey && Empty(DateOfBirth.FormValue)) {
                    DateOfBirth.AddErrorMessage(ConvertToString(DateOfBirth.RequiredErrorMessage).Replace("%s", DateOfBirth.Caption));
                }
            }
            if (!CheckDate(DateOfBirth.FormValue, DateOfBirth.FormatPattern)) {
                DateOfBirth.AddErrorMessage(DateOfBirth.GetErrorMessage(false));
            }
            if (PrimaryAddress.Required) {
                if (!PrimaryAddress.IsDetailKey && Empty(PrimaryAddress.FormValue)) {
                    PrimaryAddress.AddErrorMessage(ConvertToString(PrimaryAddress.RequiredErrorMessage).Replace("%s", PrimaryAddress.Caption));
                }
            }
            if (PrimaryAddressCity.Required) {
                if (!PrimaryAddressCity.IsDetailKey && Empty(PrimaryAddressCity.FormValue)) {
                    PrimaryAddressCity.AddErrorMessage(ConvertToString(PrimaryAddressCity.RequiredErrorMessage).Replace("%s", PrimaryAddressCity.Caption));
                }
            }
            if (PrimaryAddressPostCode.Required) {
                if (!PrimaryAddressPostCode.IsDetailKey && Empty(PrimaryAddressPostCode.FormValue)) {
                    PrimaryAddressPostCode.AddErrorMessage(ConvertToString(PrimaryAddressPostCode.RequiredErrorMessage).Replace("%s", PrimaryAddressPostCode.Caption));
                }
            }
            if (PrimaryAddressCountryID.Required) {
                if (!PrimaryAddressCountryID.IsDetailKey && Empty(PrimaryAddressCountryID.FormValue)) {
                    PrimaryAddressCountryID.AddErrorMessage(ConvertToString(PrimaryAddressCountryID.RequiredErrorMessage).Replace("%s", PrimaryAddressCountryID.Caption));
                }
            }
            if (AlternativeAddress.Required) {
                if (!AlternativeAddress.IsDetailKey && Empty(AlternativeAddress.FormValue)) {
                    AlternativeAddress.AddErrorMessage(ConvertToString(AlternativeAddress.RequiredErrorMessage).Replace("%s", AlternativeAddress.Caption));
                }
            }
            if (AlternativeAddressCity.Required) {
                if (!AlternativeAddressCity.IsDetailKey && Empty(AlternativeAddressCity.FormValue)) {
                    AlternativeAddressCity.AddErrorMessage(ConvertToString(AlternativeAddressCity.RequiredErrorMessage).Replace("%s", AlternativeAddressCity.Caption));
                }
            }
            if (AlternativeAddressPostCode.Required) {
                if (!AlternativeAddressPostCode.IsDetailKey && Empty(AlternativeAddressPostCode.FormValue)) {
                    AlternativeAddressPostCode.AddErrorMessage(ConvertToString(AlternativeAddressPostCode.RequiredErrorMessage).Replace("%s", AlternativeAddressPostCode.Caption));
                }
            }
            if (AlternativeAddressCountryID.Required) {
                if (!AlternativeAddressCountryID.IsDetailKey && Empty(AlternativeAddressCountryID.FormValue)) {
                    AlternativeAddressCountryID.AddErrorMessage(ConvertToString(AlternativeAddressCountryID.RequiredErrorMessage).Replace("%s", AlternativeAddressCountryID.Caption));
                }
            }
            if (MobileNumber.Required) {
                if (!MobileNumber.IsDetailKey && Empty(MobileNumber.FormValue)) {
                    MobileNumber.AddErrorMessage(ConvertToString(MobileNumber.RequiredErrorMessage).Replace("%s", MobileNumber.Caption));
                }
            }
            if (UserID.Required) {
                if (!UserID.IsDetailKey && Empty(UserID.FormValue)) {
                    UserID.AddErrorMessage(ConvertToString(UserID.RequiredErrorMessage).Replace("%s", UserID.Caption));
                }
            }
            if (Status.Required) {
                if (!Status.IsDetailKey && Empty(Status.FormValue)) {
                    Status.AddErrorMessage(ConvertToString(Status.RequiredErrorMessage).Replace("%s", Status.Caption));
                }
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

        // Add record
        #pragma warning disable 168, 219

        protected async Task<JsonBoolResult> AddRow(Dictionary<string, object>? rsold = null) { // DN
            bool result = false;

            // Set new row
            Dictionary<string, object> rsnew = new ();
            try {
                // FirstName
                FirstName.SetDbValue(rsnew, FirstName.CurrentValue);

                // MiddleName
                MiddleName.SetDbValue(rsnew, MiddleName.CurrentValue);

                // LastName
                LastName.SetDbValue(rsnew, LastName.CurrentValue);

                // Gender
                Gender.SetDbValue(rsnew, Gender.CurrentValue);

                // PlaceOfBirth
                PlaceOfBirth.SetDbValue(rsnew, PlaceOfBirth.CurrentValue);

                // DateOfBirth
                DateOfBirth.SetDbValue(rsnew, ConvertToDateTime(DateOfBirth.CurrentValue, DateOfBirth.FormatPattern));

                // PrimaryAddress
                PrimaryAddress.SetDbValue(rsnew, PrimaryAddress.CurrentValue);

                // PrimaryAddressCity
                PrimaryAddressCity.SetDbValue(rsnew, PrimaryAddressCity.CurrentValue);

                // PrimaryAddressPostCode
                PrimaryAddressPostCode.SetDbValue(rsnew, PrimaryAddressPostCode.CurrentValue);

                // PrimaryAddressCountryID
                PrimaryAddressCountryID.SetDbValue(rsnew, PrimaryAddressCountryID.CurrentValue);

                // AlternativeAddress
                AlternativeAddress.SetDbValue(rsnew, AlternativeAddress.CurrentValue);

                // AlternativeAddressCity
                AlternativeAddressCity.SetDbValue(rsnew, AlternativeAddressCity.CurrentValue);

                // AlternativeAddressPostCode
                AlternativeAddressPostCode.SetDbValue(rsnew, AlternativeAddressPostCode.CurrentValue);

                // AlternativeAddressCountryID
                AlternativeAddressCountryID.SetDbValue(rsnew, AlternativeAddressCountryID.CurrentValue);

                // MobileNumber
                MobileNumber.SetDbValue(rsnew, MobileNumber.CurrentValue);

                // UserID
                UserID.SetDbValue(rsnew, UserID.CurrentValue);

                // Status
                Status.SetDbValue(rsnew, Status.CurrentValue);
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }

            // Update current values
            SetCurrentValues(rsnew);

            // Load db values from rsold
            LoadDbValues(rsold);

            // Call Row Inserting event
            bool insertRow = RowInserting(rsold, rsnew);
            if (insertRow) {
                try {
                    result = ConvertToBool(await InsertAsync(rsnew));
                    rsnew["CustomerID"] = CustomerID.CurrentValue!;
                } catch (Exception e) {
                    if (Config.Debug)
                        throw;
                    FailureMessage = e.Message;
                    result = false;
                }
            } else {
                if (SuccessMessage != "" || FailureMessage != "") {
                    // Use the message, do nothing
                } else if (CancelMessage != "") {
                    FailureMessage = CancelMessage;
                    CancelMessage = "";
                } else {
                    FailureMessage = Language.Phrase("InsertCancelled");
                }
                result = false;
            }

            // Call Row Inserted event
            if (result)
                RowInserted(rsold, rsnew);

            // Write JSON for API request
            Dictionary<string, object> d = new ();
            d.Add("success", result);
            if (IsJsonResponse() && result) {
                if (GetRecordFromDictionary(rsnew) is var row && row != null) {
                    string table = TableVar;
                    d.Add(table, row);
                }
                d.Add("action", Config.ApiAddAction);
                d.Add("version", Config.ProductVersion);
                return new JsonBoolResult(d, result);
            }
            return new JsonBoolResult(d, result);
        }

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("customerslist")), "", TableVar, true);
            string pageId = IsCopy ? "Copy" : "Add";
            breadcrumb.Add("add", pageId, url);
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
