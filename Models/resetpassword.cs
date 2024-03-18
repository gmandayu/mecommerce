namespace ASPNETMaker2023.Models;

// Partial class
public partial class mecommerce {
    /// <summary>
    /// resetPassword
    /// </summary>
    public static ResetPassword resetPassword
    {
        get => HttpData.Get<ResetPassword>("resetPassword")!;
        set => HttpData["resetPassword"] = value;
    }

    /// <summary>
    /// Page class (reset_password)
    /// </summary>
    public class ResetPassword : ResetPasswordBase
    {
        // Constructor
        public ResetPassword(Controller controller) : base(controller)
        {
        }

        // Constructor
        public ResetPassword() : base()
        {
        }

        // Server events

        // Email Sending event
        public override bool EmailSending(Email email, dynamic? args) {
            //Log(email);
            if (Email.Mailer == null)
            {
                var smtpClient = new SmtpClient();
                smtpClient.CheckCertificateRevocation = false;
                Email.Mailer = smtpClient;
            }
            else { Email.Mailer.CheckCertificateRevocation = false; }
            return true;
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class ResetPasswordBase : Users
    {
        // Page ID
        public string PageID = "reset_password";

        // Project ID
        public string ProjectID = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}";

        // Page object name
        public string PageObjName = "resetPassword";

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
        public ResetPasswordBase()
        {
            // Initialize
            CurrentPage = this;

            // Language object
            Language = ResolveLanguage();

            // Table object (users)
            if (users == null || users is Users)
                users = this;
            users ??= this;

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
        public string PageName => "resetpassword";

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
        public ResetPasswordBase(Controller? controller = null): this() { // DN
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

        public DbField<SqlDbType> EmailAddress = new ("Users", "email", 202, SqlDbType.NVarChar) {
                TableName = "Users",
                Name = "email",
                Expression = "email"
            };

        public bool IsModal = false;

        /// <summary>
        /// Page run
        /// </summary>
        /// <returns>Page result</returns>
        public override async Task<IActionResult> Run()
        {
            OffsetColumnClass = ""; // Override user table

            // Create EmailAddress field object (used by validation only)
            EmailAddress.EditAttrs.AppendClass("form-control ew-form-control");

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

            // Check modal
            if (IsModal)
                SkipHeaderFooter = true;
            bool validEmail = false;
            string action = "", filter = "", userName = "";
            string[] activateCode;
            bool postBack = IsPost();
            CurrentBreadcrumb = new ();
            var url = CurrentUrl(); // DN
            CurrentBreadcrumb.Add("reset_password", "ResetPwd", url, "", "", true);
            Heading = Language.Phrase("ResetPwd");
            if (postBack) {
                EmailAddress.SetFormValue(Post(EmailAddress.FieldVar));
                validEmail = await ValidateForm();
                if (validEmail) {
                    action = "reset"; // Prompt user to change password
                }

                // Set up filter (WHERE Clause)
                DbField emailFld = UserTable.Fields[Config.UserEmailFieldName];
                if (emailFld.IsEncrypt) { // If encrypted, need to loop all records (to be improved)
                    filter = "";
                } else {
                    filter = GetUserFilter(Config.UserEmailFieldName, EmailAddress.CurrentValue);
                }
            } else if (Get("action", out StringValues sv)) { // Handle email activation
                action = sv.ToString();
                userName = Get("user");
                activateCode = Decrypt(Get("code")).Split(',');
                string activateUserName = activateCode.Length > 0 ? activateCode[0] : "";
                DateTime? dt = activateCode.Length > 1 ? ParseDateTime(activateCode[1]) : null;
                double diff = dt.HasValue ? ((TimeSpan)(DateTime.Now - dt.Value)).TotalMinutes : -1;
                if (!SameString(userName, activateUserName) || diff < 0 || diff > Config.ResetPasswordTimeLimit || !SameText(action, "reset")) { // Email activation
                    if (Empty(FailureMessage))
                        FailureMessage = Language.Phrase("ActivateFailed"); // Set activate failed message
                    return Terminate("login"); // Go to login page
                }
                if (SameText(action, "reset"))
                    action = "resetpassword";
                filter = GetUserFilter(Config.LoginUsernameFieldName, userName);
            }
            if (!Empty(action)) {
                CurrentFilter = filter;
                string sql = CurrentSql;
                string password = "";
                Dictionary<string, object> rsnew = new ();
                using var rsuser = await UserTableConn.OpenDataReaderAsync(sql);
                validEmail = false;
                while (rsuser != null && await rsuser.ReadAsync()) {
                    var rsold = UserTableConn.GetRow(rsuser); // DN
                    if (action == "resetpassword") // Check username if email activation
                        validEmail = SameString(userName, GetUserInfo(Config.LoginUsernameFieldName, rsold));
                    else
                        validEmail = SameText(EmailAddress.CurrentValue, GetUserInfo(Config.UserEmailFieldName, rsold));
                    if (validEmail) {
                        // Call User Recover Password event
                        validEmail = UserRecoverPassword(rsold);
                        if (validEmail) {
                            userName = ConvertToString(GetUserInfo(Config.LoginUsernameFieldName, rsold));
                            password = ConvertToString(GetUserInfo(Config.LoginPasswordFieldName, rsold));
                        }
                    }
                    if (validEmail)
                        break;
                }
                var email = new Email(); // DN
                bool emailSent = false;
                if (validEmail) {
                    if (SameText(action, "resetpassword")) { // Reset password
                        Session[Config.SessionUserProfileUserName] = userName; // Save login user name
                        Session[Config.SessionStatus] = "passwordreset";
                        return Terminate("changepassword");
                    } else {
                        await email.Load(Config.EmailResetPasswordTemplate);
                        var activateLink = FullUrl("", "resetpwd") + "?action=reset";
                        activateLink += "&user=" + RawUrlEncode(userName);
                        string code = userName + "," + StdCurrentDateTime();
                        activateLink += "&code=" + Encrypt(code);
                        email.ReplaceContent("<!--$ActivateLink-->", activateLink);
                        email.ReplaceSender(Config.SenderEmail); // Replace Sender
                        email.ReplaceRecipient(ConvertToString(EmailAddress.CurrentValue)); // Replace Recipient
                        email.ReplaceContent("<!--$UserName-->", userName);
                        var args = new Dictionary<string, dynamic>();
                        if (Config.EncryptedPassword && SameText(action, "confirm"))
                            args.Add("rs", rsnew);
                        if (EmailSending(email, args))
                            emailSent = await email.SendAsync();
                    }
                }
                if (validEmail && !emailSent)
                    FailureMessage = Language.Phrase(email.SendError); // Set up error message
                SuccessMessage = Language.Phrase("ResetPasswordResponse"); // Set up success message
                return Terminate("login"); // Return to login page
            }

            // Set LoginStatus, Page Rendering and Page Render
            if (!IsApi() && !IsTerminated) {
                SetupLoginStatus(); // Setup login status

                // Pass login status to client side
                SetClientVar("login", LoginStatus);
            }
            return PageResult();
        }

        #pragma warning disable 1998
        // Validate form
        protected async Task<bool> ValidateForm() {
            // Check if validation required
            if (!Config.ServerValidate)
                return true;
            bool validateForm = true;
            string emailAddress = ConvertToString(EmailAddress.CurrentValue);
            if (Empty(emailAddress)) {
                EmailAddress.AddErrorMessage(Language.Phrase("EnterRequiredField").Replace("%s", Language.Phrase("Email")));
                validateForm = false;
            }
            if (!CheckEmail(emailAddress)) {
                EmailAddress.AddErrorMessage(Language.Phrase("IncorrectEmail"));
                validateForm = false;
            }

            // Call Form Custom Validate event
            string formCustomError = "";
            validateForm = validateForm && FormCustomValidate(ref formCustomError);
            FailureMessage = formCustomError;
            return validateForm;
        }
        #pragma warning restore 1998

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

        // Email Sending event
        public override bool EmailSending(Email email, dynamic? args) {
            //Log(email);
            if (Email.Mailer == null)
            {
                var smtpClient = new SmtpClient();
                smtpClient.CheckCertificateRevocation = false;
                Email.Mailer = smtpClient;
            }
            else { Email.Mailer.CheckCertificateRevocation = false; }
            return true;
        }

        // Form Custom Validate event
        public virtual bool FormCustomValidate(ref string customError) {
            //Return error message in customError
            return true;
        }

        // User RecoverPassword event
        public virtual bool UserRecoverPassword(Dictionary<string, object> rs) {
            // Return false to abort
            return true;
        }
    } // End page class
} // End Partial class
