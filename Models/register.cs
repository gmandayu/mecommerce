namespace ASPNETMaker2023.Models;

// Partial class
public partial class mecommerce {
    /// <summary>
    /// register
    /// </summary>
    public static Register register
    {
        get => HttpData.Get<Register>("register")!;
        set => HttpData["register"] = value;
    }

    /// <summary>
    /// Page class (register)
    /// </summary>
    public class Register : RegisterBase
    {
        // Constructor
        public Register(Controller controller) : base(controller)
        {
        }

        // Constructor
        public Register() : base()
        {
        }

        // Server events
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class RegisterBase : Users
    {
        // Page ID
        public string PageID = "register";

        // Project ID
        public string ProjectID = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}";

        // Page object name
        public string PageObjName = "register";

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
        public RegisterBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-striped table-bordered table-hover table-sm ew-desktop-table ew-register-table";

            // Language object
            Language = ResolveLanguage();

            // Table object (users)
            if (users == null || users is Users)
                users = this;
            users = Resolve("Users")!;

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
                return "";
            }
        }

        // Page name
        public string PageName => "register";

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

        // Constructor
        public RegisterBase(Controller? controller = null): this() { // DN
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
                    // Handle modal response
                    if (IsModal) { // Show as modal
                        var result = new { url = GetUrl(url) };
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

        public string FormClassName = "ew-form ew-register-form";

        public bool IsModal = false;

        public bool IsMobileOrModal = false;

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

            // Load default values for add
            LoadDefaultValues();

            // Check modal
            if (IsModal)
                SkipHeaderFooter = true;
            IsMobileOrModal = IsMobile() || IsModal;

            // Set up Breadcrumb
            CurrentBreadcrumb = new ();
            string url = CurrentUrl(); // DN
            CurrentBreadcrumb.Add("register", "RegisterPage", url, "", "", true);
            Heading = Language.Phrase("RegisterPage");
            bool userExists = false;
            await LoadRowValues(); // Load default values

            // Get action
            string currentAction = "";
            StringValues sv;
            if (IsApi())
                currentAction = "insert";
            else if (Post("action", out sv))
                currentAction = sv.ToString();
            if (!Empty(currentAction)) {
                CurrentAction = currentAction;
                await LoadFormValues(); // Get form values

                // Validate form
                if (!await ValidateForm()) {
                    if (IsApi()) {
                        return Controller.Json(new { success = false, validation = GetValidationErrors(), error = GetFailureMessage() });
                    } else {
                        CurrentAction = "show"; // Form error, reset action
                    }
                }
            } else {
                CurrentAction = "show"; // Display blank record
            }
            var model = new LoginModel();

                // Insert record
                if (IsInsert) {
                    // Check for duplicate User ID
                    string filter = GetUserFilter(Config.LoginUsernameFieldName, _Username.CurrentValue);
                    using var rschk = await LoadReader(filter);
                    if (rschk?.HasRows ?? false) { // DN
                        userExists = true;
                        RestoreFormValues(); // Restore form values
                        FailureMessage = Language.Phrase("UserExists"); // Set user exist message
                    }
                    if (!userExists) {
                        SendEmail = true; // Send email on add success
                        var res = await AddRow(); // Add record
                        if (res) {
                            if (Empty(SuccessMessage))
                                SuccessMessage = Language.Phrase("RegisterSuccess"); // Register success

                            // Auto login user
                            model.Username = ConvertToString(_Username.CurrentValue);
                            model.Password = Password.FormValue;
                            if (!await Security.ValidateUser(model, true)) // Auto login user
                                FailureMessage = Language.Phrase("AutoLoginFailed"); // Set auto login failed message
                            if (IsApi()) { // Return to caller
                                return res;
                            } else {
                                if (Config.UseTwoFactorAuthentication && Config.ForceTwoFactorAuthentication) { // Add two factor authentication
                                    Session[Config.SessionStatus] = "loggingin2fa";
                                    Session[Config.SessionUserProfileUserName] = _Username.CurrentValue;
                                    Session[Config.SessionUserProfilePassword] = Password.FormValue;
                                    return Terminate("login2fa"); // Add two factor authentication
                                } else {
                                    return Terminate(ViewUrl); // Return
                                }
                            }
                        } else if (IsApi()) { // API request, return
                            return Terminate();
                        } else {
                            RestoreFormValues(); // Restore form values
                        }
                    } else if (IsApi()) { // API request, return
                        return Terminate();
                    }
            }

            // Render row
            if (IsConfirm) { // Confirm page
                RowType = RowType.View; // Render view
            } else {
                RowType = RowType.Add; // Render add
            }
            ResetAttributes();
            await RenderRow();

            // Set LoginStatus, Page Rendering and Page Render
            if (!IsApi() && !IsTerminated) {
                SetupLoginStatus(); // Setup login status

                // Pass login status to client side
                SetClientVar("login", LoginStatus);
            }
            return PageResult();
        }

        // Confirm page
        public bool ConfirmPage = true; // DN

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
                    MobileNumber.SetFormValue(val, true, validate);
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

            // Note: ConfirmValue will be compared with FormValue
            if (Config.EncryptedPassword) // Encrypted password, use raw value
                Password.ConfirmValue = CurrentForm.GetValue("c_Password");
            else
                Password.ConfirmValue = RemoveXss(CurrentForm.GetValue("c_Password"));

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

                // Email
                _Email.HrefValue = "";
                _Email.TooltipValue = "";

                // MobileNumber
                MobileNumber.HrefValue = "";
                MobileNumber.TooltipValue = "";

                // Username
                _Username.HrefValue = "";
                _Username.TooltipValue = "";

                // Password
                Password.HrefValue = "";
                Password.TooltipValue = "";

                // ProfilePicture
                ProfilePicture.HrefValue = "";
                ProfilePicture.ExportHrefValue = ProfilePicture.UploadPath + ProfilePicture.Upload.DbValue;
                ProfilePicture.TooltipValue = "";
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
                if (IsShow && !EventCancelled)
                    await RenderUploadField(ProfilePicture);

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
            if (!CheckInteger(MobileNumber.FormValue)) {
                MobileNumber.AddErrorMessage(MobileNumber.GetErrorMessage(false));
            }
            if (_Username.Required) {
                if (!_Username.IsDetailKey && Empty(_Username.FormValue)) {
                    _Username.AddErrorMessage(Language.Phrase("EnterUserName"));
                }
            }
            if (!_Username.Raw && Config.RemoveXss && CheckUsername(_Username.FormValue)) {
                _Username.AddErrorMessage(Language.Phrase("InvalidUsernameChars"));
            }
            if (Password.Required) {
                if (!Password.IsDetailKey && Empty(Password.FormValue)) {
                    Password.AddErrorMessage(Language.Phrase("EnterPassword"));
                }
            }
            if (!IsApi() && !SameString(Password.ConfirmValue, Password.FormValue)) { // DN
                Password.AddErrorMessage(Language.Phrase("MismatchPassword"));
            }
            if (!Password.Raw && Config.RemoveXss && CheckPassword(Password.FormValue)) {
                Password.AddErrorMessage(Language.Phrase("InvalidPasswordChars"));
            }
            if (ProfilePicture.Required) {
                if (ProfilePicture.Upload.FileName == "" && !ProfilePicture.Upload.KeepFile) {
                    ProfilePicture.AddErrorMessage(ConvertToString(ProfilePicture.RequiredErrorMessage).Replace("%s", ProfilePicture.Caption));
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

            // Call User Registered event
            if (result)
                UserRegistered(rsnew);

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

        // Form Custom Validate event
        public virtual bool FormCustomValidate(ref string customError) {
            //Return error message in customError
            return true;
        }

        // User Registered event
        public virtual void UserRegistered(Dictionary<string, object> rs) {
            //Log("User_Registered");
        }

        // User Activated event
        public virtual void UserActivated(Dictionary<string, object> rs) {
            //Log("User_Activated");
        }
    } // End page class
} // End Partial class
