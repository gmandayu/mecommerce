namespace ASPNETMaker2023.Models;

// Partial class
public partial class mecommerce {
    /// <summary>
    /// usersAdd
    /// </summary>
    public static UsersAdd usersAdd
    {
        get => HttpData.Get<UsersAdd>("usersAdd")!;
        set => HttpData["usersAdd"] = value;
    }

    /// <summary>
    /// Page class for Users
    /// </summary>
    public class UsersAdd : UsersAddBase
    {
        // Constructor
        public UsersAdd(Controller controller) : base(controller)
        {
        }

        // Constructor
        public UsersAdd() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class UsersAddBase : Users
    {
        // Page ID
        public string PageID = "add";

        // Project ID
        public string ProjectID = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}";

        // Table name
        public string TableName { get; set; } = "Users";

        // Page object name
        public string PageObjName = "usersAdd";

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
        public UsersAddBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-add-table";

            // Language object
            Language = ResolveLanguage();

            // Table object (users)
            if (users == null || users is Users)
                users = this;

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
        public string PageName => "usersadd";

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
            UserID.Visible = false;
            _Email.SetVisibility();
            MobileNumber.SetVisibility();
            _Username.SetVisibility();
            Password.SetVisibility();
            ProfilePicture.SetVisibility();
            ProfileDescription.SetVisibility();
            IsActive.SetVisibility();
            UserLevelID.SetVisibility();
            CreatedBy.SetVisibility();
            CreatedDateTime.SetVisibility();
            UpdatedBy.SetVisibility();
            UpdatedDateTime.SetVisibility();
        }

        // Constructor
        public UsersAddBase(Controller? controller = null): this() { // DN
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
                            result.Add("view", pageName == "usersview" ? "1" : "0"); // If View page, no primary button
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
                ProfilePicture.OldUploadPath = ProfilePicture.GetUploadPath();
                ProfilePicture.UploadPath = ProfilePicture.OldUploadPath;
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
            key += UrlEncode(ConvertToString(dict.ContainsKey("UserID") ? dict["UserID"] : UserID.CurrentValue));
            return key;
        }

        // Hide fields for Add/Edit
        protected void HideFieldsForAddEdit() {
            if (IsAdd || IsCopy || IsGridAdd)
                UserID.Visible = false;
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
            await SetupLookupOptions(IsActive);
            await SetupLookupOptions(UserLevelID);

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
                if (RouteValues.TryGetValue("UserID", out rv)) { // DN
                    UserID.QueryValue = UrlDecode(rv); // DN
                } else if (Get("UserID", out sv)) {
                    UserID.QueryValue = sv.ToString();
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
                        return Terminate("userslist"); // No matching record, return to List page // DN
                    }
                    break;
                case "insert": // Add new record // DN
                    SendEmail = true; // Send email on add success
                    var res = await AddRow(rsold);
                    if (res) { // Add successful
                        if (Empty(SuccessMessage) && Post("addopt") != "1") // Skip success message for addopt (done in JavaScript)
                            SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
                        string returnUrl = "";
                        returnUrl = ViewUrl;
                        if (GetPageName(returnUrl) == "userslist")
                            returnUrl = AddMasterUrl(ListUrl); // List page, return to List page with correct master key if necessary
                        else if (GetPageName(returnUrl) == "usersview")
                            returnUrl = ViewUrl; // View page, return to View page with key URL directly

                        // Handle UseAjaxActions
                        if (IsModal && UseAjaxActions) {
                            IsModal = false;
                            if (GetPageName(returnUrl) != "userslist") {
                                TempData["Return-Url"] = returnUrl; // Save return URL
                                returnUrl = "userslist"; // Return list page content
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
                usersAdd?.PageRender();
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
            ProfilePicture.Upload.Index = CurrentForm.Index;
            if (!await ProfilePicture.Upload.UploadFile()) // DN
                End(ProfilePicture.Upload.Message);
            ProfilePicture.CurrentValue = ProfilePicture.Upload.FileName;
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

            // Check field name 'Email' before field var 'x__Email'
            val = CurrentForm.HasValue("Email") ? CurrentForm.GetValue("Email") : CurrentForm.GetValue("x__Email");
            if (!_Email.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Email") && !CurrentForm.HasValue("x__Email")) // DN
                    _Email.Visible = false; // Disable update for API request
                else
                    _Email.SetFormValue(val);
            }

            // Check field name 'MobileNumber' before field var 'x_MobileNumber'
            val = CurrentForm.HasValue("MobileNumber") ? CurrentForm.GetValue("MobileNumber") : CurrentForm.GetValue("x_MobileNumber");
            if (!MobileNumber.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("MobileNumber") && !CurrentForm.HasValue("x_MobileNumber")) // DN
                    MobileNumber.Visible = false; // Disable update for API request
                else
                    MobileNumber.SetFormValue(val);
            }

            // Check field name 'Username' before field var 'x__Username'
            val = CurrentForm.HasValue("Username") ? CurrentForm.GetValue("Username") : CurrentForm.GetValue("x__Username");
            if (!_Username.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Username") && !CurrentForm.HasValue("x__Username")) // DN
                    _Username.Visible = false; // Disable update for API request
                else
                    _Username.SetFormValue(val);
            }

            // Check field name 'Password' before field var 'x_Password'
            val = CurrentForm.HasValue("Password") ? CurrentForm.GetValue("Password") : CurrentForm.GetValue("x_Password");
            if (!Password.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("Password") && !CurrentForm.HasValue("x_Password")) // DN
                    Password.Visible = false; // Disable update for API request
                else
                    Password.SetFormValue(val);
            }

            // Check field name 'ProfileDescription' before field var 'x_ProfileDescription'
            val = CurrentForm.HasValue("ProfileDescription") ? CurrentForm.GetValue("ProfileDescription") : CurrentForm.GetValue("x_ProfileDescription");
            if (!ProfileDescription.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("ProfileDescription") && !CurrentForm.HasValue("x_ProfileDescription")) // DN
                    ProfileDescription.Visible = false; // Disable update for API request
                else
                    ProfileDescription.SetFormValue(val);
            }

            // Check field name 'IsActive' before field var 'x_IsActive'
            val = CurrentForm.HasValue("IsActive") ? CurrentForm.GetValue("IsActive") : CurrentForm.GetValue("x_IsActive");
            if (!IsActive.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("IsActive") && !CurrentForm.HasValue("x_IsActive")) // DN
                    IsActive.Visible = false; // Disable update for API request
                else
                    IsActive.SetFormValue(val);
            }

            // Check field name 'UserLevelID' before field var 'x_UserLevelID'
            val = CurrentForm.HasValue("UserLevelID") ? CurrentForm.GetValue("UserLevelID") : CurrentForm.GetValue("x_UserLevelID");
            if (!UserLevelID.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("UserLevelID") && !CurrentForm.HasValue("x_UserLevelID")) // DN
                    UserLevelID.Visible = false; // Disable update for API request
                else
                    UserLevelID.SetFormValue(val);
            }

            // Check field name 'CreatedBy' before field var 'x_CreatedBy'
            val = CurrentForm.HasValue("CreatedBy") ? CurrentForm.GetValue("CreatedBy") : CurrentForm.GetValue("x_CreatedBy");
            if (!CreatedBy.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("CreatedBy") && !CurrentForm.HasValue("x_CreatedBy")) // DN
                    CreatedBy.Visible = false; // Disable update for API request
                else
                    CreatedBy.SetFormValue(val, true, validate);
            }

            // Check field name 'CreatedDateTime' before field var 'x_CreatedDateTime'
            val = CurrentForm.HasValue("CreatedDateTime") ? CurrentForm.GetValue("CreatedDateTime") : CurrentForm.GetValue("x_CreatedDateTime");
            if (!CreatedDateTime.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("CreatedDateTime") && !CurrentForm.HasValue("x_CreatedDateTime")) // DN
                    CreatedDateTime.Visible = false; // Disable update for API request
                else
                    CreatedDateTime.SetFormValue(val, true, validate);
                CreatedDateTime.CurrentValue = UnformatDateTime(CreatedDateTime.CurrentValue, CreatedDateTime.FormatPattern);
            }

            // Check field name 'UpdatedBy' before field var 'x_UpdatedBy'
            val = CurrentForm.HasValue("UpdatedBy") ? CurrentForm.GetValue("UpdatedBy") : CurrentForm.GetValue("x_UpdatedBy");
            if (!UpdatedBy.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("UpdatedBy") && !CurrentForm.HasValue("x_UpdatedBy")) // DN
                    UpdatedBy.Visible = false; // Disable update for API request
                else
                    UpdatedBy.SetFormValue(val, true, validate);
            }

            // Check field name 'UpdatedDateTime' before field var 'x_UpdatedDateTime'
            val = CurrentForm.HasValue("UpdatedDateTime") ? CurrentForm.GetValue("UpdatedDateTime") : CurrentForm.GetValue("x_UpdatedDateTime");
            if (!UpdatedDateTime.IsDetailKey) {
                if (IsApi() && !CurrentForm.HasValue("UpdatedDateTime") && !CurrentForm.HasValue("x_UpdatedDateTime")) // DN
                    UpdatedDateTime.Visible = false; // Disable update for API request
                else
                    UpdatedDateTime.SetFormValue(val, true, validate);
                UpdatedDateTime.CurrentValue = UnformatDateTime(UpdatedDateTime.CurrentValue, UpdatedDateTime.FormatPattern);
            }

            // Check field name 'UserID' before field var 'x_UserID'
            val = CurrentForm.HasValue("UserID") ? CurrentForm.GetValue("UserID") : CurrentForm.GetValue("x_UserID");
            ProfilePicture.OldUploadPath = ProfilePicture.GetUploadPath();
            ProfilePicture.UploadPath = ProfilePicture.OldUploadPath;
            await GetUploadFiles(); // Get upload files
        }
        #pragma warning restore 1998

        // Restore form values
        public void RestoreFormValues()
        {
            _Email.CurrentValue = _Email.FormValue;
            MobileNumber.CurrentValue = MobileNumber.FormValue;
            _Username.CurrentValue = _Username.FormValue;
            Password.CurrentValue = Password.FormValue;
            ProfileDescription.CurrentValue = ProfileDescription.FormValue;
            IsActive.CurrentValue = IsActive.FormValue;
            UserLevelID.CurrentValue = UserLevelID.FormValue;
            CreatedBy.CurrentValue = CreatedBy.FormValue;
            CreatedDateTime.CurrentValue = CreatedDateTime.FormValue;
            CreatedDateTime.CurrentValue = UnformatDateTime(CreatedDateTime.CurrentValue, CreatedDateTime.FormatPattern);
            UpdatedBy.CurrentValue = UpdatedBy.FormValue;
            UpdatedDateTime.CurrentValue = UpdatedDateTime.FormValue;
            UpdatedDateTime.CurrentValue = UnformatDateTime(UpdatedDateTime.CurrentValue, UpdatedDateTime.FormatPattern);
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

            // Check if valid User ID
            if (res) {
                res = ShowOptionLink("add");
                if (!res)
                    FailureMessage = DeniedMessage();
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
            UserID.SetDbValue(row["UserID"]);
            _Email.SetDbValue(row["Email"]);
            MobileNumber.SetDbValue(row["MobileNumber"]);
            _Username.SetDbValue(row["Username"]);
            Password.SetDbValue(row["Password"]);
            ProfilePicture.Upload.DbValue = row["ProfilePicture"];
            ProfilePicture.SetDbValue(ProfilePicture.Upload.DbValue);
            ProfileDescription.SetDbValue(row["ProfileDescription"]);
            IsActive.SetDbValue((ConvertToBool(row["IsActive"]) ? "1" : "0"));
            UserLevelID.SetDbValue(row["UserLevelID"]);
            CreatedBy.SetDbValue(row["CreatedBy"]);
            CreatedDateTime.SetDbValue(row["CreatedDateTime"]);
            UpdatedBy.SetDbValue(row["UpdatedBy"]);
            UpdatedDateTime.SetDbValue(row["UpdatedDateTime"]);
        }
        #pragma warning restore 162, 168, 1998, 4014

        // Return a row with default values
        protected Dictionary<string, object> NewRow() {
            var row = new Dictionary<string, object>();
            row.Add("UserID", UserID.DefaultValue ?? DbNullValue); // DN
            row.Add("Email", _Email.DefaultValue ?? DbNullValue); // DN
            row.Add("MobileNumber", MobileNumber.DefaultValue ?? DbNullValue); // DN
            row.Add("Username", _Username.DefaultValue ?? DbNullValue); // DN
            row.Add("Password", Password.DefaultValue ?? DbNullValue); // DN
            row.Add("ProfilePicture", ProfilePicture.DefaultValue ?? DbNullValue); // DN
            row.Add("ProfileDescription", ProfileDescription.DefaultValue ?? DbNullValue); // DN
            row.Add("IsActive", IsActive.DefaultValue ?? DbNullValue); // DN
            row.Add("UserLevelID", UserLevelID.DefaultValue ?? DbNullValue); // DN
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

            // UserID
            UserID.RowCssClass = "row";

            // Email
            _Email.RowCssClass = "row";

            // MobileNumber
            MobileNumber.RowCssClass = "row";

            // Username
            _Username.RowCssClass = "row";

            // Password
            Password.RowCssClass = "row";

            // ProfilePicture
            ProfilePicture.RowCssClass = "row";

            // ProfileDescription
            ProfileDescription.RowCssClass = "row";

            // IsActive
            IsActive.RowCssClass = "row";

            // UserLevelID
            UserLevelID.RowCssClass = "row";

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
                // UserID
                UserID.ViewValue = UserID.CurrentValue;
                UserID.ViewCustomAttributes = "";

                // Email
                _Email.ViewValue = ConvertToString(_Email.CurrentValue); // DN
                _Email.ViewCustomAttributes = "";

                // MobileNumber
                MobileNumber.ViewValue = ConvertToString(MobileNumber.CurrentValue); // DN
                MobileNumber.ViewCustomAttributes = "";

                // Username
                _Username.ViewValue = ConvertToString(_Username.CurrentValue); // DN
                _Username.ViewCustomAttributes = "";

                // Password
                Password.ViewValue = Language.Phrase("PasswordMask");
                Password.ViewCustomAttributes = "";

                // ProfilePicture
                ProfilePicture.UploadPath = ProfilePicture.GetUploadPath();
                if (!IsNull(ProfilePicture.Upload.DbValue)) {
                    ProfilePicture.ViewValue = ProfilePicture.Upload.DbValue;
                } else {
                    ProfilePicture.ViewValue = "";
                }
                ProfilePicture.ViewCustomAttributes = "";

                // ProfileDescription
                ProfileDescription.ViewValue = ProfileDescription.CurrentValue;
                ProfileDescription.ViewCustomAttributes = "";

                // IsActive
                if (ConvertToBool(IsActive.CurrentValue)) {
                    IsActive.ViewValue = IsActive.TagCaption(1) != "" ? IsActive.TagCaption(1) : "Yes";
                } else {
                    IsActive.ViewValue = IsActive.TagCaption(2) != "" ? IsActive.TagCaption(2) : "No";
                }
                IsActive.ViewCustomAttributes = "";

                // UserLevelID
                if (Security.CanAdmin) { // System admin
                    curVal = ConvertToString(UserLevelID.CurrentValue);
                    if (!Empty(curVal)) {
                        if (UserLevelID.Lookup != null && IsDictionary(UserLevelID.Lookup?.Options) && UserLevelID.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                            UserLevelID.ViewValue = UserLevelID.LookupCacheOption(curVal);
                        } else { // Lookup from database // DN
                            filterWrk = SearchFilter("[UserLevelID]", "=", UserLevelID.CurrentValue, DataType.Number, "");
                            sqlWrk = UserLevelID.Lookup?.GetSql(false, filterWrk, null, this, true, true);
                            rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                            if (rswrk?.Count > 0 && UserLevelID.Lookup != null) { // Lookup values found
                                var listwrk = UserLevelID.Lookup?.RenderViewRow(rswrk[0]);
                                UserLevelID.ViewValue = UserLevelID.HighlightLookup(ConvertToString(rswrk[0]), UserLevelID.DisplayValue(listwrk));
                            } else {
                                UserLevelID.ViewValue = FormatNumber(UserLevelID.CurrentValue, UserLevelID.FormatPattern);
                            }
                        }
                    } else {
                        UserLevelID.ViewValue = DbNullValue;
                    }
                } else {
                    UserLevelID.ViewValue = Language.Phrase("PasswordMask");
                }
                UserLevelID.ViewCustomAttributes = "";

                // CreatedBy
                CreatedBy.ViewValue = CreatedBy.CurrentValue;
                CreatedBy.ViewValue = FormatNumber(CreatedBy.ViewValue, CreatedBy.FormatPattern);
                CreatedBy.ViewCustomAttributes = "";

                // CreatedDateTime
                CreatedDateTime.ViewValue = CreatedDateTime.CurrentValue;
                CreatedDateTime.ViewValue = FormatDateTime(CreatedDateTime.ViewValue, CreatedDateTime.FormatPattern);
                CreatedDateTime.ViewCustomAttributes = "";

                // UpdatedBy
                UpdatedBy.ViewValue = UpdatedBy.CurrentValue;
                UpdatedBy.ViewValue = FormatNumber(UpdatedBy.ViewValue, UpdatedBy.FormatPattern);
                UpdatedBy.ViewCustomAttributes = "";

                // UpdatedDateTime
                UpdatedDateTime.ViewValue = UpdatedDateTime.CurrentValue;
                UpdatedDateTime.ViewValue = FormatDateTime(UpdatedDateTime.ViewValue, UpdatedDateTime.FormatPattern);
                UpdatedDateTime.ViewCustomAttributes = "";

                // Email
                _Email.HrefValue = "";

                // MobileNumber
                MobileNumber.HrefValue = "";

                // Username
                _Username.HrefValue = "";

                // Password
                Password.HrefValue = "";

                // ProfilePicture
                ProfilePicture.HrefValue = "";
                ProfilePicture.ExportHrefValue = ProfilePicture.UploadPath + ProfilePicture.Upload.DbValue;

                // ProfileDescription
                ProfileDescription.HrefValue = "";

                // IsActive
                IsActive.HrefValue = "";

                // UserLevelID
                UserLevelID.HrefValue = "";

                // CreatedBy
                CreatedBy.HrefValue = "";

                // CreatedDateTime
                CreatedDateTime.HrefValue = "";

                // UpdatedBy
                UpdatedBy.HrefValue = "";

                // UpdatedDateTime
                UpdatedDateTime.HrefValue = "";
            } else if (RowType == RowType.Add) {
                // Email
                _Email.SetupEditAttributes();
                if (!_Email.Raw)
                    _Email.CurrentValue = HtmlDecode(_Email.CurrentValue);
                _Email.EditValue = HtmlEncode(_Email.CurrentValue);
                _Email.PlaceHolder = RemoveHtml(_Email.Caption);

                // MobileNumber
                MobileNumber.SetupEditAttributes();
                if (!MobileNumber.Raw)
                    MobileNumber.CurrentValue = HtmlDecode(MobileNumber.CurrentValue);
                MobileNumber.EditValue = HtmlEncode(MobileNumber.CurrentValue);
                MobileNumber.PlaceHolder = RemoveHtml(MobileNumber.Caption);

                // Username
                _Username.SetupEditAttributes();
                if (!_Username.Raw)
                    _Username.CurrentValue = HtmlDecode(_Username.CurrentValue);
                _Username.EditValue = HtmlEncode(_Username.CurrentValue);
                _Username.PlaceHolder = RemoveHtml(_Username.Caption);

                // Password
                Password.SetupEditAttributes(new () { { "class", "ew-password-strength" } } );
                Password.EditValue = Language.Phrase("PasswordMask"); // Show as masked password
                Password.PlaceHolder = RemoveHtml(Password.Caption);

                // ProfilePicture
                ProfilePicture.SetupEditAttributes();
                ProfilePicture.EditAttrs["accept"] = "jpeg,png,jpg";
                ProfilePicture.UploadPath = ProfilePicture.GetUploadPath();
                if (!IsNull(ProfilePicture.Upload.DbValue)) {
                    ProfilePicture.EditValue = ProfilePicture.Upload.DbValue;
                } else {
                    ProfilePicture.EditValue = "";
                }
                if (!Empty(ProfilePicture.CurrentValue))
                        ProfilePicture.Upload.FileName = ConvertToString(ProfilePicture.CurrentValue);
                ProfilePicture.Upload.DbValue = DbNullValue;
                if ((IsShow || IsCopy) && !EventCancelled)
                    await RenderUploadField(ProfilePicture);

                // ProfileDescription
                ProfileDescription.SetupEditAttributes();
                ProfileDescription.EditValue = ProfileDescription.CurrentValue; // DN
                ProfileDescription.PlaceHolder = RemoveHtml(ProfileDescription.Caption);

                // IsActive
                IsActive.EditValue = IsActive.Options(false);
                IsActive.PlaceHolder = RemoveHtml(IsActive.Caption);

                // UserLevelID
                UserLevelID.SetupEditAttributes();
                if (!Security.CanAdmin) { // System admin
                    UserLevelID.EditValue = Language.Phrase("PasswordMask");
                } else {
                    curVal = ConvertToString(UserLevelID.CurrentValue)?.Trim() ?? "";
                    if (UserLevelID.Lookup != null && IsDictionary(UserLevelID.Lookup?.Options) && UserLevelID.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                        UserLevelID.EditValue = UserLevelID.Lookup?.Options.Values.ToList();
                    } else { // Lookup from database
                        if (curVal == "") {
                            filterWrk = "0=1";
                        } else {
                            filterWrk = SearchFilter("[UserLevelID]", "=", UserLevelID.CurrentValue, DataType.Number, "");
                        }
                        sqlWrk = UserLevelID.Lookup?.GetSql(true, filterWrk, null, this, false, true);
                        rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                        UserLevelID.EditValue = rswrk;
                    }
                    UserLevelID.PlaceHolder = RemoveHtml(UserLevelID.Caption);
                    if (!Empty(UserLevelID.EditValue) && IsNumeric(UserLevelID.EditValue))
                        UserLevelID.EditValue = FormatNumber(UserLevelID.EditValue, UserLevelID.FormatPattern);
                }

                // CreatedBy
                CreatedBy.SetupEditAttributes();
                CreatedBy.EditValue = CreatedBy.CurrentValue; // DN
                CreatedBy.PlaceHolder = RemoveHtml(CreatedBy.Caption);
                if (!Empty(CreatedBy.EditValue) && IsNumeric(CreatedBy.EditValue))
                    CreatedBy.EditValue = FormatNumber(CreatedBy.EditValue, CreatedBy.FormatPattern);

                // CreatedDateTime
                CreatedDateTime.SetupEditAttributes();
                CreatedDateTime.EditValue = FormatDateTime(CreatedDateTime.CurrentValue, CreatedDateTime.FormatPattern); // DN
                CreatedDateTime.PlaceHolder = RemoveHtml(CreatedDateTime.Caption);

                // UpdatedBy
                UpdatedBy.SetupEditAttributes();
                UpdatedBy.EditValue = UpdatedBy.CurrentValue; // DN
                UpdatedBy.PlaceHolder = RemoveHtml(UpdatedBy.Caption);
                if (!Empty(UpdatedBy.EditValue) && IsNumeric(UpdatedBy.EditValue))
                    UpdatedBy.EditValue = FormatNumber(UpdatedBy.EditValue, UpdatedBy.FormatPattern);

                // UpdatedDateTime
                UpdatedDateTime.SetupEditAttributes();
                UpdatedDateTime.EditValue = FormatDateTime(UpdatedDateTime.CurrentValue, UpdatedDateTime.FormatPattern); // DN
                UpdatedDateTime.PlaceHolder = RemoveHtml(UpdatedDateTime.Caption);

                // Add refer script

                // Email
                _Email.HrefValue = "";

                // MobileNumber
                MobileNumber.HrefValue = "";

                // Username
                _Username.HrefValue = "";

                // Password
                Password.HrefValue = "";

                // ProfilePicture
                ProfilePicture.HrefValue = "";
                ProfilePicture.ExportHrefValue = ProfilePicture.UploadPath + ProfilePicture.Upload.DbValue;

                // ProfileDescription
                ProfileDescription.HrefValue = "";

                // IsActive
                IsActive.HrefValue = "";

                // UserLevelID
                UserLevelID.HrefValue = "";

                // CreatedBy
                CreatedBy.HrefValue = "";

                // CreatedDateTime
                CreatedDateTime.HrefValue = "";

                // UpdatedBy
                UpdatedBy.HrefValue = "";

                // UpdatedDateTime
                UpdatedDateTime.HrefValue = "";
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
            if (_Email.Required) {
                if (!_Email.IsDetailKey && Empty(_Email.FormValue)) {
                    _Email.AddErrorMessage(ConvertToString(_Email.RequiredErrorMessage).Replace("%s", _Email.Caption));
                }
            }
            if (MobileNumber.Required) {
                if (!MobileNumber.IsDetailKey && Empty(MobileNumber.FormValue)) {
                    MobileNumber.AddErrorMessage(ConvertToString(MobileNumber.RequiredErrorMessage).Replace("%s", MobileNumber.Caption));
                }
            }
            if (_Username.Required) {
                if (!_Username.IsDetailKey && Empty(_Username.FormValue)) {
                    _Username.AddErrorMessage(ConvertToString(_Username.RequiredErrorMessage).Replace("%s", _Username.Caption));
                }
            }
            if (!_Username.Raw && Config.RemoveXss && CheckUsername(_Username.FormValue)) {
                _Username.AddErrorMessage(Language.Phrase("InvalidUsernameChars"));
            }
            if (Password.Required) {
                if (!Password.IsDetailKey && Empty(Password.FormValue)) {
                    Password.AddErrorMessage(ConvertToString(Password.RequiredErrorMessage).Replace("%s", Password.Caption));
                }
            }
            if (!Password.Raw && Config.RemoveXss && CheckPassword(Password.FormValue)) {
                Password.AddErrorMessage(Language.Phrase("InvalidPasswordChars"));
            }
            if (ProfilePicture.Required) {
                if (ProfilePicture.Upload.FileName == "" && !ProfilePicture.Upload.KeepFile) {
                    ProfilePicture.AddErrorMessage(ConvertToString(ProfilePicture.RequiredErrorMessage).Replace("%s", ProfilePicture.Caption));
                }
            }
            if (ProfileDescription.Required) {
                if (!ProfileDescription.IsDetailKey && Empty(ProfileDescription.FormValue)) {
                    ProfileDescription.AddErrorMessage(ConvertToString(ProfileDescription.RequiredErrorMessage).Replace("%s", ProfileDescription.Caption));
                }
            }
            if (IsActive.Required) {
                if (Empty(IsActive.FormValue)) {
                    IsActive.AddErrorMessage(ConvertToString(IsActive.RequiredErrorMessage).Replace("%s", IsActive.Caption));
                }
            }
            if (UserLevelID.Required) {
                if (Security.CanAdmin && !UserLevelID.IsDetailKey && Empty(UserLevelID.FormValue)) {
                    UserLevelID.AddErrorMessage(ConvertToString(UserLevelID.RequiredErrorMessage).Replace("%s", UserLevelID.Caption));
                }
            }
            if (CreatedBy.Required) {
                if (!CreatedBy.IsDetailKey && Empty(CreatedBy.FormValue)) {
                    CreatedBy.AddErrorMessage(ConvertToString(CreatedBy.RequiredErrorMessage).Replace("%s", CreatedBy.Caption));
                }
            }
            if (!CheckInteger(CreatedBy.FormValue)) {
                CreatedBy.AddErrorMessage(CreatedBy.GetErrorMessage(false));
            }
            if (CreatedDateTime.Required) {
                if (!CreatedDateTime.IsDetailKey && Empty(CreatedDateTime.FormValue)) {
                    CreatedDateTime.AddErrorMessage(ConvertToString(CreatedDateTime.RequiredErrorMessage).Replace("%s", CreatedDateTime.Caption));
                }
            }
            if (!CheckDate(CreatedDateTime.FormValue, CreatedDateTime.FormatPattern)) {
                CreatedDateTime.AddErrorMessage(CreatedDateTime.GetErrorMessage(false));
            }
            if (UpdatedBy.Required) {
                if (!UpdatedBy.IsDetailKey && Empty(UpdatedBy.FormValue)) {
                    UpdatedBy.AddErrorMessage(ConvertToString(UpdatedBy.RequiredErrorMessage).Replace("%s", UpdatedBy.Caption));
                }
            }
            if (!CheckInteger(UpdatedBy.FormValue)) {
                UpdatedBy.AddErrorMessage(UpdatedBy.GetErrorMessage(false));
            }
            if (UpdatedDateTime.Required) {
                if (!UpdatedDateTime.IsDetailKey && Empty(UpdatedDateTime.FormValue)) {
                    UpdatedDateTime.AddErrorMessage(ConvertToString(UpdatedDateTime.RequiredErrorMessage).Replace("%s", UpdatedDateTime.Caption));
                }
            }
            if (!CheckDate(UpdatedDateTime.FormValue, UpdatedDateTime.FormatPattern)) {
                UpdatedDateTime.AddErrorMessage(UpdatedDateTime.GetErrorMessage(false));
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
                // Email
                _Email.SetDbValue(rsnew, _Email.CurrentValue);

                // MobileNumber
                MobileNumber.SetDbValue(rsnew, MobileNumber.CurrentValue);

                // Username
                _Username.SetDbValue(rsnew, _Username.CurrentValue);

                // Password
                if (Config.EncryptedPassword && !IsMaskedPassword(Password.CurrentValue)) {
                    Password.CurrentValue = EncryptPassword(Config.CaseSensitivePassword ? ConvertToString(Password.CurrentValue) : ConvertToString(Password.CurrentValue).ToLower());
                }
                Password.SetDbValue(rsnew, Password.CurrentValue);

                // ProfilePicture
                if (ProfilePicture.Visible && !ProfilePicture.Upload.KeepFile) {
                    ProfilePicture.Upload.DbValue = ""; // No need to delete old file
                    if (Empty(ProfilePicture.Upload.FileName)) {
                        rsnew["ProfilePicture"] = DbNullValue;
                    } else {
                        FixUploadTempFileNames(ProfilePicture);
                        rsnew["ProfilePicture"] = ProfilePicture.Upload.FileName;
                    }
                }

                // ProfileDescription
                ProfileDescription.SetDbValue(rsnew, ProfileDescription.CurrentValue);

                // IsActive
                IsActive.SetDbValue(rsnew, ConvertToBool(IsActive.CurrentValue));

                // UserLevelID
                if (Security.CanAdmin) { // System admin
                UserLevelID.SetDbValue(rsnew, UserLevelID.CurrentValue);
                }

                // CreatedBy
                CreatedBy.SetDbValue(rsnew, CreatedBy.CurrentValue);

                // CreatedDateTime
                CreatedDateTime.SetDbValue(rsnew, ConvertToDateTimeOffset(CreatedDateTime.CurrentValue, DateTimeStyles.AssumeUniversal));

                // UpdatedBy
                UpdatedBy.SetDbValue(rsnew, UpdatedBy.CurrentValue);

                // UpdatedDateTime
                UpdatedDateTime.SetDbValue(rsnew, ConvertToDateTimeOffset(UpdatedDateTime.CurrentValue, DateTimeStyles.AssumeUniversal));

                // UserID
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }
            if (ProfilePicture.Visible && !ProfilePicture.Upload.KeepFile) {
                ProfilePicture.UploadPath = ProfilePicture.GetUploadPath();
                if (!Empty(ProfilePicture.Upload.FileName)) {
                    ProfilePicture.Upload.DbValue = DbNullValue;
                    FixUploadFileNames(ProfilePicture);
                    ProfilePicture.SetDbValue(rsnew, ProfilePicture.Upload.FileName);
                }
            }

            // Update current values
            SetCurrentValues(rsnew);

            // Check if valid User ID
            bool validUser = false;
            string userIdMsg;
            if (!Empty(Security.CurrentUserID) && !Security.IsAdmin) { // Non system admin
                validUser = Security.IsValidUserID(UserID.CurrentValue);
                if (!validUser) {
                    userIdMsg = Language.Phrase("UnAuthorizedUserID").Replace("%c", ConvertToString(Security.CurrentUserID));
                    userIdMsg = userIdMsg.Replace("%u", ConvertToString(UserID.CurrentValue));
                    FailureMessage = userIdMsg;
                    return JsonBoolResult.FalseResult;
                }
            }
            if (!Empty(_Email.CurrentValue)) { // Check field with unique index
                var filter = "(Email = '" + AdjustSql(_Email.CurrentValue, DbId) + "')";
                using var rschk = await LoadReader(filter);
                if (rschk?.HasRows ?? false) { // DN
                    FailureMessage = Language.Phrase("DupIndex").Replace("%f", _Email.Caption).Replace("%v", ConvertToString(_Email.CurrentValue));
                    return JsonBoolResult.FalseResult;
                }
            }
            if (!Empty(MobileNumber.CurrentValue)) { // Check field with unique index
                var filter = "(MobileNumber = '" + AdjustSql(MobileNumber.CurrentValue, DbId) + "')";
                using var rschk = await LoadReader(filter);
                if (rschk?.HasRows ?? false) { // DN
                    FailureMessage = Language.Phrase("DupIndex").Replace("%f", MobileNumber.Caption).Replace("%v", ConvertToString(MobileNumber.CurrentValue));
                    return JsonBoolResult.FalseResult;
                }
            }

            // Load db values from rsold
            LoadDbValues(rsold);
            if (rsold != null) {
                ProfilePicture.OldUploadPath = ProfilePicture.GetUploadPath();
                ProfilePicture.UploadPath = ProfilePicture.OldUploadPath;
            } else {
                ProfilePicture.UploadPath = ProfilePicture.GetUploadPath();
            }

            // Call Row Inserting event
            bool insertRow = RowInserting(rsold, rsnew);
            if (insertRow) {
                try {
                    result = ConvertToBool(await InsertAsync(rsnew));
                    rsnew["UserID"] = UserID.CurrentValue!;
                } catch (Exception e) {
                    if (Config.Debug)
                        throw;
                    FailureMessage = e.Message;
                    result = false;
                }
                if (result) {
                    if (ProfilePicture.Visible && !ProfilePicture.Upload.KeepFile) {
                        ProfilePicture.Upload.DbValue = DbNullValue;
                        if (!await SaveUploadFiles(ProfilePicture, ConvertToString(rsnew["ProfilePicture"]), false))
                        {
                            FailureMessage = Language.Phrase("UploadErrorFailedToWrite");
                            return JsonBoolResult.FalseResult;
                        }
                    }
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

        // Show link optionally based on User ID
        protected bool ShowOptionLink(string pageId = "") { // DN
            if (Security.IsLoggedIn && !Security.IsAdmin && !UserIDAllow(pageId))
                return Security.IsValidUserID(UserID.CurrentValue);
            return true;
        }

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("userslist")), "", TableVar, true);
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
