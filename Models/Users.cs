namespace ASPNETMaker2023.Models;

// Partial class
public partial class mecommerce {
    /// <summary>
    /// users
    /// </summary>
    [MaybeNull]
    public static Users users
    {
        get => HttpData.Resolve<Users>("Users");
        set => HttpData["Users"] = value;
    }

    /// <summary>
    /// Table class for Users
    /// </summary>
    public class Users : DbTable, IQueryFactory
    {
        public int RowCount = 0; // DN

        public bool UseSessionForListSql = true;

        // Column CSS classes
        public string LeftColumnClass = "col-sm-2 col-form-label ew-label";

        public string RightColumnClass = "col-sm-10";

        public string OffsetColumnClass = "col-sm-10 offset-sm-2";

        public string TableLeftColumnClass = "w-col-2";

        // Ajax / Modal
        public bool UseAjaxActions = false;

        public bool ModalSearch = false;

        public bool ModalView = false;

        public bool ModalAdd = false;

        public bool ModalEdit = false;

        public bool ModalUpdate = false;

        public bool InlineDelete = false;

        public bool ModalGridAdd = false;

        public bool ModalGridEdit = false;

        public bool ModalMultiEdit = false;

        public readonly DbField<SqlDbType> UserID;

        public readonly DbField<SqlDbType> _Email;

        public readonly DbField<SqlDbType> MobileNumber;

        public readonly DbField<SqlDbType> _Username;

        public readonly DbField<SqlDbType> Password;

        public readonly DbField<SqlDbType> ProfilePicture;

        public readonly DbField<SqlDbType> ProfileDescription;

        public readonly DbField<SqlDbType> IsActive;

        public readonly DbField<SqlDbType> UserLevelID;

        public readonly DbField<SqlDbType> CreatedBy;

        public readonly DbField<SqlDbType> CreatedDateTime;

        public readonly DbField<SqlDbType> UpdatedBy;

        public readonly DbField<SqlDbType> UpdatedDateTime;

        // Constructor
        public Users()
        {
            // Language object // DN
            Language = ResolveLanguage();
            TableVar = "Users";
            Name = "Users";
            Type = "TABLE";
            UpdateTable = "dbo.Users"; // Update Table
            DbId = "DB"; // DN
            ExportAll = true;
            ExportPageBreakCount = 0; // Page break per every n record (PDF only)
            ExportPageOrientation = "portrait"; // Page orientation (PDF only)
            ExportPageSize = "a4"; // Page size (PDF only)
            ExportColumnWidths = new float[] {  }; // Column widths (PDF only) // DN
            ExportExcelPageOrientation = ""; // Page orientation (EPPlus only)
            ExportExcelPageSize = ""; // Page size (EPPlus only)
            ExportWordPageOrientation = ""; // Page orientation (Word only)
            DetailAdd = false; // Allow detail add
            DetailEdit = false; // Allow detail edit
            DetailView = false; // Allow detail view
            ShowMultipleDetails = false; // Show multiple details
            GridAddRowCount = 5;
            AllowAddDeleteRow = true; // Allow add/delete row
            UseAjaxActions = UseAjaxActions || Config.UseAjaxActions;
            UserIdAllowSecurity = Config.DefaultUserIdAllowSecurity; // User ID Allow

            // UserID
            UserID = new (this, "x_UserID", 3, SqlDbType.Int) {
                Name = "UserID",
                Expression = "[UserID]",
                BasicSearchExpression = "CAST([UserID] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[UserID]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "NO",
                InputTextType = "text",
                IsAutoIncrement = true, // Autoincrement field
                IsPrimaryKey = true, // Primary key field
                Nullable = false, // NOT NULL field
                Sortable = false, // Allow sort
                DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN" },
                CustomMessage = Language.FieldPhrase("Users", "UserID", "CustomMsg"),
                IsUpload = false
            };
            Fields.Add("UserID", UserID);

            // Email
            _Email = new (this, "x__Email", 202, SqlDbType.NVarChar) {
                Name = "Email",
                Expression = "[Email]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Email]",
                DateTimeFormat = -1,
                VirtualExpression = "[Email]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "email",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "_Email", "CustomMsg"),
                IsUpload = false
            };
            _Email.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(_Email, "Users", true, "Email", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(_Email, "Users", true, "Email", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(_Email, "Users", true, "Email", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("Email", _Email);

            // MobileNumber
            MobileNumber = new (this, "x_MobileNumber", 202, SqlDbType.NVarChar) {
                Name = "MobileNumber",
                Expression = "[MobileNumber]",
                BasicSearchExpression = "[MobileNumber]",
                DateTimeFormat = -1,
                VirtualExpression = "[MobileNumber]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                Sortable = false, // Allow sort
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "MobileNumber", "CustomMsg"),
                IsUpload = false
            };
            Fields.Add("MobileNumber", MobileNumber);

            // Username
            _Username = new (this, "x__Username", 202, SqlDbType.NVarChar) {
                Name = "Username",
                Expression = "[Username]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Username]",
                DateTimeFormat = -1,
                VirtualExpression = "[Username]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                Required = true, // Required field
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "_Username", "CustomMsg"),
                IsUpload = false
            };
            _Username.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(_Username, "Users", true, "Username", new List<string> {"Username", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(_Username, "Users", true, "Username", new List<string> {"Username", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(_Username, "Users", true, "Username", new List<string> {"Username", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("Username", _Username);

            // Password
            Password = new (this, "x_Password", 202, SqlDbType.NVarChar) {
                Name = "Password",
                Expression = "[Password]",
                BasicSearchExpression = "[Password]",
                DateTimeFormat = -1,
                VirtualExpression = "[Password]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "PASSWORD",
                InputTextType = "week",
                Required = true, // Required field
                Sortable = false, // Allow sort
                SearchOperators = new () { "=", "<>", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "Password", "CustomMsg"),
                IsUpload = false
            };
            if (Config.EncryptedPassword)
                Password.Raw = true;
            Fields.Add("Password", Password);

            // ProfilePicture
            ProfilePicture = new (this, "x_ProfilePicture", 203, SqlDbType.NText) {
                Name = "ProfilePicture",
                Expression = "[ProfilePicture]",
                UseBasicSearch = true,
                BasicSearchExpression = "[ProfilePicture]",
                DateTimeFormat = -1,
                VirtualExpression = "[ProfilePicture]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "FILE",
                InputTextType = "text",
                Sortable = false, // Allow sort
                UseFilter = true, // Table header filter
                UploadAllowedFileExtensions = "jpeg,png,jpg",
                UploadMaxFileSize = 5000000,
                SearchOperators = new () { "=", "<>", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "ProfilePicture", "CustomMsg"),
                IsUpload = true
            };
            ProfilePicture.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(ProfilePicture, "Users", true, "ProfilePicture", new List<string> {"ProfilePicture", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(ProfilePicture, "Users", true, "ProfilePicture", new List<string> {"ProfilePicture", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(ProfilePicture, "Users", true, "ProfilePicture", new List<string> {"ProfilePicture", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            ProfilePicture.GetUploadPath = () => "uploads/" + MobileNumber.DbValue;
            Fields.Add("ProfilePicture", ProfilePicture);

            // ProfileDescription
            ProfileDescription = new (this, "x_ProfileDescription", 203, SqlDbType.NText) {
                Name = "ProfileDescription",
                Expression = "[ProfileDescription]",
                UseBasicSearch = true,
                BasicSearchExpression = "[ProfileDescription]",
                DateTimeFormat = -1,
                VirtualExpression = "[ProfileDescription]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXTAREA",
                InputTextType = "text",
                Sortable = false, // Allow sort
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "ProfileDescription", "CustomMsg"),
                IsUpload = false
            };
            ProfileDescription.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(ProfileDescription, "Users", true, "ProfileDescription", new List<string> {"ProfileDescription", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(ProfileDescription, "Users", true, "ProfileDescription", new List<string> {"ProfileDescription", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(ProfileDescription, "Users", true, "ProfileDescription", new List<string> {"ProfileDescription", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("ProfileDescription", ProfileDescription);

            // IsActive
            IsActive = new (this, "x_IsActive", 11, SqlDbType.Bit) {
                Name = "IsActive",
                Expression = "[IsActive]",
                BasicSearchExpression = "[IsActive]",
                DateTimeFormat = -1,
                VirtualExpression = "[IsActive]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "CHECKBOX",
                InputTextType = "text",
                DataType = DataType.Boolean,
                UseFilter = true, // Table header filter
                OptionCount = 2,
                SearchOperators = new () { "=", "<>", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "IsActive", "CustomMsg"),
                IsUpload = false
            };
            IsActive.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(IsActive, "Users", true, "IsActive", new List<string> {"IsActive", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(IsActive, "Users", true, "IsActive", new List<string> {"IsActive", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(IsActive, "Users", true, "IsActive", new List<string> {"IsActive", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("IsActive", IsActive);

            // UserLevelID
            UserLevelID = new (this, "x_UserLevelID", 3, SqlDbType.Int) {
                Name = "UserLevelID",
                Expression = "[UserLevelID]",
                BasicSearchExpression = "CAST([UserLevelID] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[UserLevelID]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "SELECT",
                InputTextType = "text",
                UsePleaseSelect = true, // Use PleaseSelect by default
                PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
                UseFilter = true, // Table header filter
                DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
                SearchOperators = new () { "=", "<>", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "UserLevelID", "CustomMsg"),
                IsUpload = false
            };
            UserLevelID.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(UserLevelID, "UserLevels", true, "UserLevelID", new List<string> {"UserLevelName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[UserLevelName]"),
                "id-ID" => new Lookup<DbField>(UserLevelID, "UserLevels", true, "UserLevelID", new List<string> {"UserLevelName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[UserLevelName]"),
                _ => new Lookup<DbField>(UserLevelID, "UserLevels", true, "UserLevelID", new List<string> {"UserLevelName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[UserLevelName]")
            };
            Fields.Add("UserLevelID", UserLevelID);

            // CreatedBy
            CreatedBy = new (this, "x_CreatedBy", 3, SqlDbType.Int) {
                Name = "CreatedBy",
                Expression = "[CreatedBy]",
                BasicSearchExpression = "CAST([CreatedBy] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[CreatedBy]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "SELECT",
                InputTextType = "text",
                Sortable = false, // Allow sort
                UsePleaseSelect = true, // Use PleaseSelect by default
                PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
                DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
                SearchOperators = new () { "=", "<>", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "CreatedBy", "CustomMsg"),
                IsUpload = false
            };
            CreatedBy.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(CreatedBy, "Users", false, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]"),
                "id-ID" => new Lookup<DbField>(CreatedBy, "Users", false, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]"),
                _ => new Lookup<DbField>(CreatedBy, "Users", false, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]")
            };
            Fields.Add("CreatedBy", CreatedBy);

            // CreatedDateTime
            CreatedDateTime = new (this, "x_CreatedDateTime", 146, SqlDbType.DateTimeOffset) {
                Name = "CreatedDateTime",
                Expression = "[CreatedDateTime]",
                BasicSearchExpression = CastDateFieldForLike("[CreatedDateTime]", 1, "DB"),
                DateTimeFormat = 1,
                VirtualExpression = "[CreatedDateTime]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                Sortable = false, // Allow sort
                DefaultErrorMessage = ConvertToString(Language.Phrase("IncorrectDate")).Replace("%s", DateFormat(1)),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "CreatedDateTime", "CustomMsg"),
                IsUpload = false
            };
            Fields.Add("CreatedDateTime", CreatedDateTime);

            // UpdatedBy
            UpdatedBy = new (this, "x_UpdatedBy", 3, SqlDbType.Int) {
                Name = "UpdatedBy",
                Expression = "[UpdatedBy]",
                BasicSearchExpression = "CAST([UpdatedBy] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[UpdatedBy]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "SELECT",
                InputTextType = "text",
                Sortable = false, // Allow sort
                UsePleaseSelect = true, // Use PleaseSelect by default
                PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
                DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
                SearchOperators = new () { "=", "<>", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "UpdatedBy", "CustomMsg"),
                IsUpload = false
            };
            UpdatedBy.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(UpdatedBy, "Users", false, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]"),
                "id-ID" => new Lookup<DbField>(UpdatedBy, "Users", false, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]"),
                _ => new Lookup<DbField>(UpdatedBy, "Users", false, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]")
            };
            Fields.Add("UpdatedBy", UpdatedBy);

            // UpdatedDateTime
            UpdatedDateTime = new (this, "x_UpdatedDateTime", 146, SqlDbType.DateTimeOffset) {
                Name = "UpdatedDateTime",
                Expression = "[UpdatedDateTime]",
                BasicSearchExpression = CastDateFieldForLike("[UpdatedDateTime]", 1, "DB"),
                DateTimeFormat = 1,
                VirtualExpression = "[UpdatedDateTime]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                Sortable = false, // Allow sort
                DefaultErrorMessage = ConvertToString(Language.Phrase("IncorrectDate")).Replace("%s", DateFormat(1)),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Users", "UpdatedDateTime", "CustomMsg"),
                IsUpload = false
            };
            Fields.Add("UpdatedDateTime", UpdatedDateTime);

            // Call Table Load event
            TableLoad();
        }

        // Set Field Visibility
        public override bool GetFieldVisibility(string fldname)
        {
            var fld = FieldByName(fldname);
            return fld?.Visible ?? false; // Returns original value
        }

        // Invoke method // DN
        public object? Invoke(string name, object?[]? parameters = null)
        {
            var mi = this.GetType().GetMethod(name);
            if (mi != null)
                return IsAsyncMethod(mi)
                    ? InvokeAsync(mi, parameters).GetAwaiter().GetResult()
                    : mi.Invoke(this, parameters);
            return null;
        }

        // Invoke async method // DN
        public async Task<object?> InvokeAsync(MethodInfo? mi, object?[]? parameters = null)
        {
            if (mi != null) {
                dynamic? awaitable = mi.Invoke(this, parameters);
                if (awaitable != null) {
                    await awaitable;
                    return awaitable.GetAwaiter().GetResult();
                }
            }
            return null;
        }

        #pragma warning disable 1998
        // Invoke async method // DN
        public async Task<object> InvokeAsync(string name, object[]? parameters = null) => InvokeAsync(this.GetType().GetMethod(name), parameters);
        #pragma warning restore 1998

        // Check if Invoke async method // DN
        public bool IsAsyncMethod(MethodInfo? mi)
        {
            if (mi != null) {
                Type attType = typeof(AsyncStateMachineAttribute);
                return mi.GetCustomAttribute(attType) is AsyncStateMachineAttribute;
            }
            return false;
        }

        // Check if Invoke async method // DN
        public bool IsAsyncMethod(string name) => IsAsyncMethod(this.GetType().GetMethod(name));

        #pragma warning disable 618
        // Connection
        public virtual DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> Connection => GetConnection(DbId);
        #pragma warning restore 618

        // Get query factory
        public QueryFactory GetQueryFactory(bool main) => Connection.GetQueryFactory(main);

        // Get query builder
        public QueryBuilder GetQueryBuilder(bool main, string output = "")
        {
            var qf = GetQueryFactory(main);
            var qb = (XQuery)qf.Query(UpdateTable);
            if (output != "")
                qb.Compiler = Connection.GetCompiler(output); // Replace the compiler
            return qb;
        }

        // Get query builder with output Id (use secondary connection)
        public QueryBuilder GetQueryBuilder(string output) => GetQueryBuilder(false, output);

        // Get query builder without output Id (use secondary connection)
        public QueryBuilder GetQueryBuilder() => GetQueryBuilder(false);

        // Set left column class (must be predefined col-*-* classes of Bootstrap grid system)
        public void SetLeftColumnClass(string columnClass)
        {
            Match m = Regex.Match(columnClass, @"^col\-(\w+)\-(\d+)$");
            if (m.Success) {
                LeftColumnClass = columnClass + " col-form-label ew-label";
                RightColumnClass = "col-" + m.Groups[1].Value + "-" + ConvertToString(12 - ConvertToInt(m.Groups[2].Value));
                OffsetColumnClass = RightColumnClass + " " + columnClass.Replace("col-", "offset-");
                TableLeftColumnClass = Regex.Replace(columnClass, @"/^col-\w+-(\d+)$/", "w-col-$1"); // Change to w-col-*
            }
        }

        // Single column sort
        public void UpdateSort(DbField fld)
        {
            string lastSort, sortField, curSort, orderBy;
            if (CurrentOrder == fld.Name) {
                sortField = fld.Expression;
                lastSort = fld.Sort;
                if ((new[] { "ASC", "DESC", "NO" }).Contains(CurrentOrderType)) {
                    curSort = CurrentOrderType;
                } else {
                    curSort = lastSort;
                }
                orderBy = (new[] { "ASC", "DESC" }).Contains(curSort) ? sortField + " " + curSort : "";
                SessionOrderBy = orderBy; // Save to Session
            }
        }

        // Update field sort
        public void UpdateFieldSort()
        {
            string orderBy = SessionOrderBy; // Get ORDER BY from Session
            var flds = GetSortFields(orderBy);
            foreach (var (key, field) in Fields) {
                string fldSort = "";
                foreach (var fld in flds) {
                    if (fld[0] == field.Expression || fld[0] == field.VirtualExpression)
                        fldSort = fld[1];
                }
                field.Sort = fldSort;
            }
        }

        #pragma warning disable 1998
        // Render X Axis for chart
        public async Task<Dictionary<string, object>> RenderChartXAxis(string chartVar, Dictionary<string, object> chartRow)
        {
            return chartRow;
        }
        #pragma warning restore 1998

        // Table level SQL
        // FROM
        private string? _sqlFrom = null;

        public string SqlFrom
        {
            get => _sqlFrom ?? "dbo.Users";
            set => _sqlFrom = value;
        }

        // SELECT
        private string? _sqlSelect = null;

        public string SqlSelect { // Select
            get => _sqlSelect ?? "SELECT * FROM " + SqlFrom;
            set => _sqlSelect = value;
        }

        // WHERE // DN
        private string? _sqlWhere = null;

        public string SqlWhere
        {
            get {
                string where = "";
                return _sqlWhere ?? where;
            }
            set {
                _sqlWhere = value;
            }
        }

        // Group By
        private string? _sqlGroupBy = null;

        public string SqlGroupBy
        {
            get => _sqlGroupBy ?? "";
            set => _sqlGroupBy = value;
        }

        // Having
        private string? _sqlHaving = null;

        public string SqlHaving
        {
            get => _sqlHaving ?? "";
            set => _sqlHaving = value;
        }

        // Order By
        private string? _sqlOrderBy = null;

        public string SqlOrderBy
        {
            get => _sqlOrderBy ?? "";
            set => _sqlOrderBy = value;
        }

        // Apply User ID filters
        public string ApplyUserIDFilters(string filter, string id = "")
        {
            if (!Empty(Security.CurrentUserID) && !Security.IsAdmin) { // Non system admin
                filter = AddUserIDFilter(filter, id);
            }
            return filter;
        }

        // Check if User ID security allows view all
        public bool UserIDAllow(string id = "")
        {
            int allow = UserIdAllowSecurity;
            return id switch {
                "add" => ((allow & 1) == 1),
                "copy" => ((allow & 1) == 1),
                "gridadd" => ((allow & 1) == 1),
                "register" => ((allow & 1) == 1),
                "addopt" => ((allow & 1) == 1),
                "edit" => ((allow & 4) == 4),
                "gridedit" => ((allow & 4) == 4),
                "update" => ((allow & 4) == 4),
                "changepassword" => ((allow & 4) == 4),
                "resetpassword" => ((allow & 4) == 4),
                "delete" => ((allow & 2) == 2),
                "view" => ((allow & 32) == 32),
                "search" => ((allow & 64) == 64),
                "lookup" => ((allow & 256) == 256),
                _ => ((allow & 8) == 8)
            };
        }

        /// <summary>
        /// Get record count by reading data reader (Async) // DN
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="c">Connection</param>
        /// <returns>Record count</returns>
        public async Task<int> GetRecordCountAsync(string sql, dynamic? c = null) {
            try {
                var cnt = 0;
                var conn = c ?? Connection;
                using var dr = await conn.OpenDataReaderAsync(sql);
                if (dr != null) {
                    while (await dr.ReadAsync())
                        cnt++;
                }
                return cnt;
            } catch {
                if (Config.Debug)
                    throw;
                return -1;
            }
        }

        /// <summary>
        /// Get record count by reading data reader // DN
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="c">Connection</param>
        /// <returns>Record count</returns>
        public int GetRecordCount(string sql, dynamic? c = null) => GetRecordCountAsync(sql, c).GetAwaiter().GetResult();

        /// <summary>
        /// Try to get record count by SELECT COUNT(*) (Async) // DN
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="c">Connection</param>
        /// <returns>Record count</returns>
        public async Task<int> TryGetRecordCountAsync(string sql, dynamic? c = null)
        {
            string orderBy = OrderBy;
            var conn = c ?? Connection;
            sql = Regex.Replace(sql, @"/\*BeginOrderBy\*/[\s\S]+/\*EndOrderBy\*/", "", RegexOptions.IgnoreCase).Trim(); // Remove ORDER BY clause (MSSQL)
            if (!Empty(orderBy) && sql.EndsWith(orderBy))
                sql = sql.Substring(0, sql.Length - orderBy.Length); // Remove ORDER BY clause
            try {
                string sqlcnt;
                if ((new[] { "TABLE", "VIEW", "LINKTABLE" }).Contains(Type) && sql.StartsWith(SqlSelect)) { // Handle Custom Field
                    sqlcnt = "SELECT COUNT(*) FROM " + SqlFrom + sql.Substring(SqlSelect.Length);
                } else {
                    sqlcnt = "SELECT COUNT(*) FROM (" + sql + ") EW_COUNT_TABLE";
                }
                return Convert.ToInt32(await conn?.ExecuteScalarAsync(sqlcnt));
            } catch {
                return await GetRecordCountAsync(sql, conn);
            }
        }

        /// <summary>
        /// Try to get record count by SELECT COUNT(*) // DN
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="c">Connection</param>
        /// <returns>Record count</returns>
        public int TryGetRecordCount(string sql, dynamic? c = null) => TryGetRecordCountAsync(sql, c).GetAwaiter().GetResult();

        // Get SQL
        public string GetSql(string where, string orderBy = "") => BuildSelectSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, where, orderBy);

        // Table SQL
        public string CurrentSql
        {
            get {
                string filter = CurrentFilter;
                filter = ApplyUserIDFilters(filter); // Add User ID filter
                string sort = SessionOrderBy;
                return GetSql(filter, sort);
            }
        }

        // Table SQL with List page filter
        public string ListSql
        {
            get {
                string sort = "";
                string select = "";
                string filter = UseSessionForListSql ? SessionWhere : "";
                AddFilter(ref filter, CurrentFilter);
                RecordsetSelecting(ref filter);
                filter = ApplyUserIDFilters(filter); // Add User ID filter
                select = SqlSelect;
                sort = UseSessionForListSql ? SessionOrderBy : "";
                return BuildSelectSql(select, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, filter, sort);
            }
        }

        // Get ORDER BY clause
        public string OrderBy
        {
            get {
                string sort = SessionOrderBy;
                return BuildSelectSql("", "", "", "", SqlOrderBy, "", sort);
            }
        }

        /// <summary>
        /// Get record count based on filter (for detail record count in master table pages) (Async) // DN
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>Record count</returns>
        public async Task<int> LoadRecordCountAsync(string filter) => await TryGetRecordCountAsync(GetSql(filter));

        /// <summary>
        /// Get record count based on filter (for detail record count in master table pages) // DN
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>Record count</returns>
        public int LoadRecordCount(string filter) => TryGetRecordCount(GetSql(filter));

        /// <summary>
        /// Get record count (for current List page) (Async) // DN
        /// </summary>
        /// <returns>Record count</returns>
        public async Task<int> ListRecordCountAsync() => await TryGetRecordCountAsync(ListSql);

        /// <summary>
        /// Get record count (for current List page) // DN
        /// </summary>
        /// <returns>Record count</returns>
        public int ListRecordCount() => TryGetRecordCount(ListSql);

        /// <summary>
        /// Insert (Async)
        /// </summary>
        /// <param name="data">Data to be inserted (IDictionary|Anonymous)</param>
        /// <returns>Row affected</returns>
        public async Task<int> InsertAsync(object data)
        {
            int result = 0;
            IDictionary<string, object> row;
            if (data is IDictionary<string, object> d)
                row = d;
            else if (IsAnonymousType(data))
                row = ConvertToDictionary<object>(data);
            else
                throw new ArgumentException("Data must be of anonymous type or Dictionary<string, object> type", "data");
            row = row.Where(kvp => {
                var fld = FieldByName(kvp.Key);
                return fld != null && !fld.IsCustom;
            }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            if (row.Count == 0)
                return -1;
            var queryBuilder = GetQueryBuilder();
            try {
                var lastInsertedId = await queryBuilder.InsertGetIdAsync<int>(row);
                UserID.SetDbValue(lastInsertedId);
                result = 1;
            } catch (Exception e) {
                CurrentPage?.SetFailureMessage(e.Message);
                if (Config.Debug)
                    throw;
            }
            return result;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data">Data to be inserted (IDictionary|Anonymous)</param>
        /// <returns>Row affected</returns>
        public int Insert(object data) => InsertAsync(data).GetAwaiter().GetResult();

        /// <summary>
        /// Update (Async)
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <returns>Row affected</returns>
        public async Task<int> UpdateAsync(object data)
        {
            IDictionary<string, object> row;
            if (data is IDictionary<string, object> d)
                row = d;
            else if (IsAnonymousType(data))
                row = ConvertToDictionary<object>(data);
            else
                throw new ArgumentException("Data must be of anonymous type or Dictionary<string, object> type", "data");
            var where = GetRowFilterAsDictionary(row);
            return where != null ? await UpdateAsync(row, where) : -1; // Prevent update all record
        }

        /// <summary>
        /// Update (Async)
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous|string)</param>
        /// <returns>Row affected</returns>
        public async Task<int> UpdateAsync(object data, object? where) => await UpdateAsync(data, where, null);

        #pragma warning disable 168, 219
        /// <summary>
        /// Update (Async)
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous)</param>
        /// <param name="rsold">Old record</param>
        /// <returns>Row affected</returns>
        public async Task<int> UpdateAsync(object data, object? where, Dictionary<string, object>? rsold)
        {
            int result = -1;
            IDictionary<string, object> row;
            if (data is IDictionary<string, object> d)
                row = d;
            else if (IsAnonymousType(data))
                row = ConvertToDictionary<object>(data);
            else
                throw new ArgumentException("Data must be of anonymous type or Dictionary<string, object> type", "data");
            Dictionary<string, object> rscascade = new ();
            row = row.Where(kvp => FieldByName(kvp.Key) is DbField fld && !fld.IsCustom).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            if (row.Count == 0)
                return -1;
            var queryBuilder = GetQueryBuilder();
            string filter = CurrentFilter;
            if (!Empty(filter))
                queryBuilder.WhereRaw(filter);
            if (IsAnonymousType(where))
                queryBuilder.Where(where);
            else if (where is IDictionary<string, object> dict)
                queryBuilder.Where(dict);
            else if (where is string)
                queryBuilder.WhereRaw((string)where);
            if (rsold != null && GetRowFilterAsDictionary(rsold) is IDictionary<string, object> rsoldFilter) // Add filter from old record
                queryBuilder.Where(rsoldFilter);
            if (queryBuilder.HasComponent("where")) // Prevent update all records
                result = await queryBuilder.UpdateAsync(row);
            return result;
        }
        #pragma warning restore 168, 219

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <returns>Row affected</returns>
        public int Update(object data) => UpdateAsync(data).GetAwaiter().GetResult();

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous|string)</param>
        /// <returns>Row affected</returns>
        public int Update(object data, object where) => UpdateAsync(data, where).GetAwaiter().GetResult();

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data">Data to be updated (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous|string)</param>
        /// <param name="rsold">Old record</param>
        /// <returns>Row affected</returns>
        public int Update(object data, object where, Dictionary<string, object> rsold) => UpdateAsync(data, where, rsold).GetAwaiter().GetResult();

        #pragma warning disable 168, 1998
        /// <summary>
        /// Delete (Async)
        /// </summary>
        /// <param name="data">Data to be removed (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous|string)</param>
        /// <returns>Row affected</returns>
        public async Task<int> DeleteAsync(object? data, object? where = null)
        {
            bool delete = true;
            IDictionary<string, object>? row = null;
            if (data is IDictionary<string, object> d)
                row = d;
            else if (IsAnonymousType(data))
                row = ConvertToDictionary<object>(data);
            var queryBuilder = GetQueryBuilder(true); // Use main connection
            if (GetRowFilterAsDictionary(row) is IDictionary<string, object> rowFilter)
                queryBuilder.Where(rowFilter);
            if (IsAnonymousType(where))
                queryBuilder.Where(where);
            else if (where is IDictionary<string, object> dict)
                queryBuilder.Where(dict);
            else if (where is string)
                queryBuilder.WhereRaw((string)where);
            int result = delete && queryBuilder.HasComponent("where") // Avoid delete if no WHERE clause
                ? await queryBuilder.DeleteAsync(Connection.Transaction)
                : -1;
            return result;
        }
        #pragma warning restore 168, 1998

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data">Data to be removed (IDictionary|Anonymous)</param>
        /// <param name="where">Where (IDictionary|Anonymous|string)</param>
        /// <returns>Row affected</returns>
        public int Delete(object data, object? where = null) => DeleteAsync(data, where).GetAwaiter().GetResult();

        // Load DbValue from recordset
        public void LoadDbValues(Dictionary<string, object>? row)
        {
            if (row == null)
                return;
            try {
                UserID.DbValue = row["UserID"]; // Set DB value only
                _Email.DbValue = row["Email"]; // Set DB value only
                MobileNumber.DbValue = row["MobileNumber"]; // Set DB value only
                _Username.DbValue = row["Username"]; // Set DB value only
                Password.DbValue = row["Password"]; // Set DB value only
                ProfilePicture.Upload.DbValue = row["ProfilePicture"];
                ProfileDescription.DbValue = row["ProfileDescription"]; // Set DB value only
                IsActive.DbValue = (ConvertToBool(row["IsActive"]) ? "1" : "0"); // Set DB value only
                UserLevelID.DbValue = row["UserLevelID"]; // Set DB value only
                CreatedBy.DbValue = row["CreatedBy"]; // Set DB value only
                CreatedDateTime.DbValue = row["CreatedDateTime"]; // Set DB value only
                UpdatedBy.DbValue = row["UpdatedBy"]; // Set DB value only
                UpdatedDateTime.DbValue = row["UpdatedDateTime"]; // Set DB value only
            } catch {}
        }

        public void DeleteUploadedFiles(Dictionary<string, object> row)
        {
            LoadDbValues(row);
            ProfilePicture.OldUploadPath = ProfilePicture.GetUploadPath();
            if (!Empty(row["ProfilePicture"])) {
                DeleteFile(ProfilePicture.OldPhysicalUploadPath + row["ProfilePicture"]);
            }
        }

        // Record filter WHERE clause
        private string _sqlKeyFilter => "[UserID] = @UserID@";

        #pragma warning disable 168, 219
        // Get record filter as string
        public string GetRecordFilter(Dictionary<string, object>? row = null, bool current = false)
        {
            string keyFilter = _sqlKeyFilter;
            object? val = null, result = "";
            val = row?.TryGetValue("UserID", out result) ?? false
                ? result
                : !Empty(UserID.OldValue) && !current ? UserID.OldValue : UserID.CurrentValue;
            if (!IsNumeric(val))
                return "0=1"; // Invalid key
            if (val == null)
                return "0=1"; // Invalid key
            keyFilter = keyFilter.Replace("@UserID@", AdjustSql(val, DbId)); // Replace key value
            return keyFilter;
        }

        // Get record filter as Dictionary // DN
        public Dictionary<string, object>? GetRowFilterAsDictionary(IDictionary<string, object>? row = null)
        {
            Dictionary<string, object>? keyFilter = new ();
            object? val = null, result;
            val = row?.TryGetValue("UserID", out result) ?? false
                ? result
                : !Empty(UserID.OldValue) ? UserID.OldValue : UserID.CurrentValue;
            if (!IsNumeric(val))
                return null; // Invalid key
            if (Empty(val))
                return null; // Invalid key
            keyFilter.Add("UserID", val); // Add key value
            return keyFilter.Count > 0 ? keyFilter : null;
        }
        #pragma warning restore 168, 219

        // Return URL
        public string ReturnUrl
        {
            get {
                string name = Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl;
                // Get referer URL automatically
                if (!Empty(ReferUrl()) && !SameText(ReferPage(), CurrentPageName()) &&
                    !SameText(ReferPage(), "login")) {// Referer not same page or login page
                        Session[name] = ReferUrl(); // Save to Session
                }
                if (!Empty(Session[name])) {
                    return Session.GetString(name);
                } else {
                    return "userslist";
                }
            }
            set {
                Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
            }
        }

        // Get modal caption
        public string GetModalCaption(string pageName)
        {
            if (SameString(pageName, "usersview"))
                return Language.Phrase("View");
            else if (SameString(pageName, "usersedit"))
                return Language.Phrase("Edit");
            else if (SameString(pageName, "usersadd"))
                return Language.Phrase("Add");
            else
                return "";
        }

        // Default route URL
        public string DefaultRouteUrl
        {
            get {
                return "userslist";
            }
        }

        // API page name
        public string GetApiPageName(string action)
        {
            return action.ToLowerInvariant() switch {
                Config.ApiViewAction => "UsersView",
                Config.ApiAddAction => "UsersAdd",
                Config.ApiEditAction => "UsersEdit",
                Config.ApiDeleteAction => "UsersDelete",
                Config.ApiListAction => "UsersList",
                _ => String.Empty
            };
        }

        // Current URL
        public string GetCurrentUrl(string parm = "")
        {
            string url = CurrentPageName();
            if (!Empty(parm))
                url = KeyUrl(url, parm);
            else
                url = KeyUrl(url, Config.TableShowDetail + "=");
            return AddMasterUrl(url);
        }

        // List URL
        public string ListUrl => "userslist";

        // View URL
        public string ViewUrl => GetViewUrl();

        // View URL
        public string GetViewUrl(string parm = "")
        {
            string url = "";
            if (!Empty(parm))
                url = KeyUrl("usersview", parm);
            else
                url = KeyUrl("usersview", Config.TableShowDetail + "=");
            return AddMasterUrl(url);
        }

        // Add URL
        public string AddUrl { get; set; } = "usersadd";

        // Add URL
        public string GetAddUrl(string parm = "")
        {
            string url = "";
            if (!Empty(parm))
                url = "usersadd?" + parm;
            else
                url = "usersadd";
            return AppPath(AddMasterUrl(url));
        }

        // Edit URL
        public string EditUrl => GetEditUrl();

        // Edit URL (with parameter)
        public string GetEditUrl(string parm = "")
        {
            string url = "";
            url = KeyUrl("usersedit", parm);
            return AppPath(AddMasterUrl(url)); // DN
        }

        // Inline edit URL
        public string InlineEditUrl =>
            AppPath(AddMasterUrl(KeyUrl("userslist", "action=edit"))); // DN

        // Copy URL
        public string CopyUrl => GetCopyUrl();

        // Copy URL
        public string GetCopyUrl(string parm = "")
        {
            string url = "";
            url = KeyUrl("usersadd", parm);
            return AppPath(AddMasterUrl(url)); // DN
        }

        // Inline copy URL
        public string InlineCopyUrl =>
            AppPath(AddMasterUrl(KeyUrl("userslist", "action=copy"))); // DN

        // Delete URL
        public string GetDeleteUrl(string parm = "")
        {
            return UseAjaxActions && Param<bool>("infinitescroll") && CurrentPageID() == "list"
                ? AppPath(KeyUrl(Config.ApiUrl + Config.ApiDeleteAction + "/" + TableVar))
                : AppPath(KeyUrl("usersdelete", parm)); // DN
        }

        // Delete URL
        public string DeleteUrl => GetDeleteUrl();

        // Add master URL
        public string AddMasterUrl(string url)
        {
            return url;
        }

        // Get primary key as JSON
        public string KeyToJson(bool htmlEncode = false)
        {
            string json = "";
            json += "\"UserID\":" + ConvertToJson(UserID.CurrentValue, "number", true);
            json = "{" + json + "}";
            if (htmlEncode)
                json = HtmlEncode(json);
            return json;
        }

        // Add key value to URL
        public string KeyUrl(string url, string parm = "") { // DN
            if (!IsNull(UserID.CurrentValue)) {
                url += "/" + UserID.CurrentValue;
            } else {
                return "javascript:ew.alert(ew.language.phrase('InvalidRecord'));";
            }
            if (Empty(parm))
                return url;
            else
                return url + "?" + parm;
        }

        // Render sort
        public string RenderFieldHeader(DbField fld)
        {
            string sortUrl = "";
            string attrs = "";
            if (fld.Sortable) {
                sortUrl = SortUrl(fld);
                attrs = " role=\"button\" data-ew-action=\"sort\" data-ajax=\"" + (UseAjaxActions ? "true" : "false") + "\" data-sort-url=\"" + sortUrl + "\" data-sort-type=\"1\"";
                if (!Empty(ContextClass)) // Add context
                    attrs += " data-context=\"" + HtmlEncode(ContextClass) + "\"";
            }
            string html = "<div class=\"ew-table-header-caption\"" + attrs + ">" + fld.Caption + "</div>";
            if (!Empty(sortUrl)) {
                html += "<div class=\"ew-table-header-sort\">" + fld.SortIcon + "</div>";
            }
            if (!IsExport() && fld.UseFilter && Security.CanSearch) {
                html += "<div class=\"ew-filter-dropdown-btn\" data-ew-action=\"filter\" data-table=\"" + fld.TableVar + "\" data-field=\"" + fld.FieldVar +
                    "\"><div class=\"ew-table-header-filter\" role=\"button\" aria-haspopup=\"true\">" + Language.Phrase("Filter") + "</div></div>";
            }
            html = "<div class=\"ew-table-header-btn\">" + html + "</div>";
            if (UseCustomTemplate) {
                string scriptId = ("tpc_{id}").Replace("{id}", fld.TableVar + "_" + fld.Param);
                html = "<template id=\"" + scriptId + "\">" + html + "</template>";
            }
            return html;
        }

        // Sort URL (already URL-encoded)
        public string SortUrl(DbField fld)
        {
            if (!Empty(CurrentAction) || !Empty(Export) ||
                (new[] {141, 201, 203, 128, 204, 205}).Contains(fld.Type)) { // Unsortable data type
                return "";
            } else if (fld.Sortable) {
                string urlParm = "order=" + UrlEncode(fld.Name) + "&amp;ordertype=" + fld.NextSort;
                if (DashboardReport)
                    urlParm += "&amp;" + Config.PageDashboard + "=true";
                return AddMasterUrl(CurrentDashboardPageUrl() + "?" + urlParm);
            }
            return "";
        }

        #pragma warning disable 168, 219
        // Get key as string
        public string GetKey(bool current = false)
        {
            List<string> keys = new ();
            string val;
            val = current ? ConvertToString(UserID.CurrentValue) ?? "" : ConvertToString(UserID.OldValue) ?? "";
            if (Empty(val))
                return String.Empty;
            keys.Add(val);
            return String.Join(Config.CompositeKeySeparator, keys);
        }

        // Get record filter as string // DN
        public string GetKey(IDictionary<string, object> row)
        {
            List<string> keys = new ();
            object? val = null, result;
            val = row?.TryGetValue("UserID", out result) ?? false ? ConvertToString(result) : null;
            if (Empty(val))
                return String.Empty; // Invalid key
            keys.Add(ConvertToString(val)); // Add key value
            return String.Join(Config.CompositeKeySeparator, keys);
        }
        #pragma warning restore 168, 219

        // Set key
        public void SetKey(string key, bool current = false)
        {
            OldKey = key;
            string[] keys = OldKey.Split(Convert.ToChar(Config.CompositeKeySeparator));
            if (keys.Length == 1) {
                if (current) {
                    UserID.CurrentValue = keys[0];
                } else {
                    UserID.OldValue = keys[0];
                }
            }
        }

        #pragma warning disable 168
        // Get record keys
        public List<string> GetRecordKeys()
        {
            List<string> result = new ();
            StringValues sv;
            List<string> keysList = new ();
            if (Post("key_m[]", out sv) || Get("key_m[]", out sv)) { // DN
                keysList = ((StringValues)sv).Select(k => ConvertToString(k)).ToList();
            } else if (RouteValues.Count > 0 || Query.Count > 0 || Form.Count > 0) { // DN
                string key = "";
                string[] keyValues = {};
                if (IsApi() && RouteValues.TryGetValue("key", out object? k)) {
                    string str = ConvertToString(k);
                    keyValues = str.Split('/');
                }
                if (RouteValues.TryGetValue("UserID", out object? v0)) { // UserID // DN
                    key = UrlDecode(v0); // DN
                } else if (IsApi() && !Empty(keyValues)) {
                    key = keyValues[0];
                } else {
                    key = Param("UserID");
                }
                keysList.Add(key);
            }
            // Check keys
            foreach (var keys in keysList) {
                if (!IsNumeric(keys)) // UserID
                    continue;
                result.Add(keys);
            }
            return result;
        }
        #pragma warning restore 168

        // Get filter from records
        public string GetFilterFromRecords(IEnumerable<Dictionary<string, object>> rows) =>
            String.Join(" OR ", rows.Select(row => "(" + GetRecordFilter(row) + ")"));

        // Get filter from record keys
        public string GetFilterFromRecordKeys(bool setCurrent = true)
        {
            List<string> recordKeys = GetRecordKeys();
            string keyFilter = "";
            foreach (var keys in recordKeys) {
                if (!Empty(keyFilter))
                    keyFilter += " OR ";
                if (setCurrent)
                    UserID.CurrentValue = keys;
                else
                    UserID.OldValue = keys;
                keyFilter += "(" + GetRecordFilter() + ")";
            }
            return keyFilter;
        }

        #pragma warning disable 618
        // Load rows based on filter // DN
        public async Task<DbDataReader?> LoadReader(string filter, string sort = "", DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType>? conn = null)
        {
            // Set up filter (SQL WHERE clause) and get return SQL
            string sql = GetSql(filter, sort);
            try {
                var dr = await (conn ?? Connection).OpenDataReaderAsync(sql);
                if (dr?.HasRows ?? false)
                    return dr;
                else
                    dr?.Close(); // Close unused data reader // DN
            } catch {}
            return null;
        }
        #pragma warning restore 618

        // Load row values from recordset
        public void LoadListRowValues(DbDataReader? dr)
        {
            if (dr == null)
                return;
            UserID.SetDbValue(dr["UserID"]);
            _Email.SetDbValue(dr["Email"]);
            MobileNumber.SetDbValue(dr["MobileNumber"]);
            _Username.SetDbValue(dr["Username"]);
            Password.SetDbValue(dr["Password"]);
            ProfilePicture.Upload.DbValue = dr["ProfilePicture"];
            ProfilePicture.SetDbValue(ProfilePicture.Upload.DbValue);
            ProfileDescription.SetDbValue(dr["ProfileDescription"]);
            IsActive.SetDbValue(ConvertToBool(dr["IsActive"]) ? "1" : "0");
            UserLevelID.SetDbValue(dr["UserLevelID"]);
            CreatedBy.SetDbValue(dr["CreatedBy"]);
            CreatedDateTime.SetDbValue(dr["CreatedDateTime"]);
            UpdatedBy.SetDbValue(dr["UpdatedBy"]);
            UpdatedDateTime.SetDbValue(dr["UpdatedDateTime"]);
        }

        // Render list content
        public async Task<string> RenderListContent(string filter)
        {
            string pageName = "UsersList";
            dynamic? page = CreateInstance(pageName, new object[] { Controller }); // DN
            if (page != null) {
                page.UseLayout = false; // DN
                await page.LoadRecordsetFromFilter(filter);
                string html = await GetViewOutput(pageName, page, false);
                page.Terminate(); // Terminate page and clean up
                return html;
            }
            return "";
        }

        #pragma warning disable 1998
        // Render list row values
        public async Task RenderListRow()
        {
            // Call Row Rendering event
            RowRendering();

            // Common render codes

            // UserID
            UserID.CellCssStyle = "white-space: nowrap;";

            // Email
            _Email.CellCssStyle = "white-space: nowrap;";

            // MobileNumber
            MobileNumber.CellCssStyle = "white-space: nowrap;";

            // Username
            _Username.CellCssStyle = "white-space: nowrap;";

            // Password
            Password.CellCssStyle = "white-space: nowrap;";

            // ProfilePicture
            ProfilePicture.CellCssStyle = "white-space: nowrap;";

            // ProfileDescription
            ProfileDescription.CellCssStyle = "white-space: nowrap;";

            // IsActive
            IsActive.CellCssStyle = "white-space: nowrap;";

            // UserLevelID
            UserLevelID.CellCssStyle = "white-space: nowrap;";

            // CreatedBy
            CreatedBy.CellCssStyle = "white-space: nowrap;";

            // CreatedDateTime
            CreatedDateTime.CellCssStyle = "white-space: nowrap;";

            // UpdatedBy
            UpdatedBy.CellCssStyle = "white-space: nowrap;";

            // UpdatedDateTime
            UpdatedDateTime.CellCssStyle = "white-space: nowrap;";

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
            ProfilePicture.CellCssStyle += "text-align: center;";
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

            // UserID
            UserID.HrefValue = "";
            UserID.TooltipValue = "";

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

            // ProfileDescription
            ProfileDescription.HrefValue = "";
            ProfileDescription.TooltipValue = "";

            // IsActive
            IsActive.HrefValue = "";
            IsActive.TooltipValue = "";

            // UserLevelID
            UserLevelID.HrefValue = "";
            UserLevelID.TooltipValue = "";

            // CreatedBy
            CreatedBy.HrefValue = "";
            CreatedBy.TooltipValue = "";

            // CreatedDateTime
            CreatedDateTime.HrefValue = "";
            CreatedDateTime.TooltipValue = "";

            // UpdatedBy
            UpdatedBy.HrefValue = "";
            UpdatedBy.TooltipValue = "";

            // UpdatedDateTime
            UpdatedDateTime.HrefValue = "";
            UpdatedDateTime.TooltipValue = "";

            // Call Row Rendered event
            RowRendered();

            // Save data for Custom Template
            Rows.Add(CustomTemplateFieldValues());
        }
        #pragma warning restore 1998

        #pragma warning disable 1998
        // Render edit row values
        public async Task RenderEditRow()
        {
            // Call Row Rendering event
            RowRendering();

            // UserID
            UserID.SetupEditAttributes();
            UserID.EditValue = UserID.CurrentValue;
            UserID.ViewCustomAttributes = "";

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
                    filterWrk = "";
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
            CreatedBy.PlaceHolder = RemoveHtml(CreatedBy.Caption);
            if (!Empty(CreatedBy.EditValue) && IsNumeric(CreatedBy.EditValue))
                CreatedBy.EditValue = FormatNumber(CreatedBy.EditValue, CreatedBy.FormatPattern);

            // CreatedDateTime
            CreatedDateTime.SetupEditAttributes();
            CreatedDateTime.EditValue = FormatDateTime(CreatedDateTime.CurrentValue, CreatedDateTime.FormatPattern); // DN
            CreatedDateTime.PlaceHolder = RemoveHtml(CreatedDateTime.Caption);

            // UpdatedBy
            UpdatedBy.SetupEditAttributes();
            UpdatedBy.PlaceHolder = RemoveHtml(UpdatedBy.Caption);
            if (!Empty(UpdatedBy.EditValue) && IsNumeric(UpdatedBy.EditValue))
                UpdatedBy.EditValue = FormatNumber(UpdatedBy.EditValue, UpdatedBy.FormatPattern);

            // UpdatedDateTime
            UpdatedDateTime.SetupEditAttributes();
            UpdatedDateTime.EditValue = FormatDateTime(UpdatedDateTime.CurrentValue, UpdatedDateTime.FormatPattern); // DN
            UpdatedDateTime.PlaceHolder = RemoveHtml(UpdatedDateTime.Caption);

            // Call Row Rendered event
            RowRendered();
        }
        #pragma warning restore 1998

        // Aggregate list row values
        public void AggregateListRowValues()
        {
        }

        #pragma warning disable 1998
        // Aggregate list row (for rendering)
        public async Task AggregateListRow()
        {
            // Call Row Rendered event
            RowRendered();
        }
        #pragma warning restore 1998

        // Export data in HTML/CSV/Word/Excel/Email/PDF format
        public async Task ExportDocument(dynamic doc, DbDataReader dataReader, int startRec, int stopRec, string exportType = "")
        {
            if (doc == null)
                return;
            if (dataReader == null)
                return;
            if (!doc.ExportCustom) {
                // Write header
                doc.ExportTableHeader();
                if (doc.Horizontal) { // Horizontal format, write header
                    doc.BeginExportRow();
                    if (exportType == "view") {
                        doc.ExportCaption(_Email);
                        doc.ExportCaption(MobileNumber);
                        doc.ExportCaption(_Username);
                        doc.ExportCaption(Password);
                        doc.ExportCaption(ProfilePicture);
                        doc.ExportCaption(ProfileDescription);
                        doc.ExportCaption(IsActive);
                        doc.ExportCaption(UserLevelID);
                        doc.ExportCaption(CreatedBy);
                        doc.ExportCaption(CreatedDateTime);
                        doc.ExportCaption(UpdatedBy);
                        doc.ExportCaption(UpdatedDateTime);
                    } else {
                        doc.ExportCaption(_Email);
                        doc.ExportCaption(MobileNumber);
                        doc.ExportCaption(_Username);
                        doc.ExportCaption(Password);
                        doc.ExportCaption(IsActive);
                        doc.ExportCaption(UserLevelID);
                        doc.ExportCaption(CreatedBy);
                        doc.ExportCaption(CreatedDateTime);
                        doc.ExportCaption(UpdatedBy);
                        doc.ExportCaption(UpdatedDateTime);
                    }
                    doc.EndExportRow();
                }
            }
            int recCnt = startRec - 1;
            // Read first record for View Page // DN
            if (exportType == "view") {
                await dataReader.ReadAsync();
            // Move to and read first record for list page // DN
            } else {
                if (Connection.SelectOffset) {
                    await dataReader.ReadAsync();
                } else {
                    for (int i = 0; i < startRec; i++) // Move to the start record and use do-while loop
                        await dataReader.ReadAsync();
                }
            }
            int rowcnt = 0; // DN
            do { // DN
                recCnt++;
                if (recCnt >= startRec) {
                    rowcnt = recCnt - startRec + 1;

                    // Page break
                    if (ExportPageBreakCount > 0) {
                        if (rowcnt > 1 && (rowcnt - 1) % ExportPageBreakCount == 0)
                            doc.ExportPageBreak();
                    }
                    LoadListRowValues(dataReader);

                    // Render row
                    RowType = RowType.View; // Render view
                    ResetAttributes();
                    await RenderListRow();
                    if (!doc.ExportCustom) {
                        doc.BeginExportRow(rowcnt); // Allow CSS styles if enabled
                        if (exportType == "view") {
                            await doc.ExportField(_Email);
                            await doc.ExportField(MobileNumber);
                            await doc.ExportField(_Username);
                            await doc.ExportField(Password);
                            await doc.ExportField(ProfilePicture);
                            await doc.ExportField(ProfileDescription);
                            await doc.ExportField(IsActive);
                            await doc.ExportField(UserLevelID);
                            await doc.ExportField(CreatedBy);
                            await doc.ExportField(CreatedDateTime);
                            await doc.ExportField(UpdatedBy);
                            await doc.ExportField(UpdatedDateTime);
                        } else {
                            await doc.ExportField(_Email);
                            await doc.ExportField(MobileNumber);
                            await doc.ExportField(_Username);
                            await doc.ExportField(Password);
                            await doc.ExportField(IsActive);
                            await doc.ExportField(UserLevelID);
                            await doc.ExportField(CreatedBy);
                            await doc.ExportField(CreatedDateTime);
                            await doc.ExportField(UpdatedBy);
                            await doc.ExportField(UpdatedDateTime);
                        }
                        doc.EndExportRow(rowcnt);
                    }
                }

                // Call Row Export server event
                if (doc.ExportCustom)
                    RowExport(doc, dataReader);
            } while (recCnt < stopRec && await dataReader.ReadAsync()); // DN
            if (!doc.ExportCustom)
                doc.ExportTableFooter();
        }

        // User ID filter
        public string GetUserIDFilter(object userid)
        {
            string userIdFilter = "[UserID] = " + QuotedValue(userid, DataType.Number, Config.UserTableDbId);
            return userIdFilter;
        }

        // Add User ID filter
        public string AddUserIDFilter(string filter = "", string id = "")
        {
            string filterWrk = "";
            if (id == "")
                id = (CurrentPageID() == "list") ? CurrentAction : CurrentPageID();
            if (!UserIDAllow(id) && !Security.IsAdmin) {
                filterWrk = Security.UserIDList();
                if (!Empty(filterWrk))
                    filterWrk = "[UserID] IN (" + filterWrk + ")";
            }

            // Call User ID Filtering event
            UserIdFiltering(ref filterWrk);
            AddFilter(ref filter, filterWrk);
            return filter;
        }

        // User ID subquery
        public string GetUserIDSubquery(DbField fld, DbField masterfld)
        {
            string wrk = "";
            string sql = "SELECT " + masterfld.Expression + " FROM dbo.Users";
            string filter = AddUserIDFilter();
            if (!Empty(filter))
                sql += " WHERE " + filter;
            var list = Connection.GetRows(sql);
            wrk = String.Join(",", list.Select(d => QuotedValue(d.Values.First(), masterfld.DataType, Config.UserTableDbId))); // List all values
            if (!Empty(wrk))
                wrk = fld.Expression + " IN (" + wrk + ")";
            else
                wrk = "0=1"; // No User ID value found
            return wrk;
        }

        // Table filter
        private string? _tableFilter = null;

        public string TableFilter
        {
            get => _tableFilter ?? "";
            set => _tableFilter = value;
        }

        // TblBasicSearchDefault
        private string? _tableBasicSearchDefault = null;

        public string TableBasicSearchDefault
        {
            get => _tableBasicSearchDefault ?? "";
            set => _tableBasicSearchDefault = value;
        }

        #pragma warning disable 1998
        // Get file data
        public async Task<IActionResult> GetFileData(string fldparm, string[] keys, bool resize, int width = -1, int height = -1)
        {
            if (width < 0)
                width = Config.ThumbnailDefaultWidth;
            if (height < 0)
                height = Config.ThumbnailDefaultHeight;

            // Set up field names
            string fldName = "", fileNameFld = "", fileTypeFld = "";
            if (SameText(fldparm, "ProfilePicture")) {
                fldName = "ProfilePicture";
                fileNameFld = "ProfilePicture";
            } else {
                return JsonBoolResult.FalseResult; // Incorrect field
            }

            // Set up key values
            if (keys.Length == 1) {
                UserID.CurrentValue = keys[0];
            } else {
                return JsonBoolResult.FalseResult; // Incorrect key
            }

            // Set up filter (WHERE Clause)
            string filter = GetRecordFilter();
            CurrentFilter = filter;
            string sql = CurrentSql;
            using var rs = await Connection.GetDataReaderAsync(sql);
            if (rs != null && await rs.ReadAsync()) {
                var val = rs[fldName];
                if (!Empty(val)) {
                    if (Fields.TryGetValue(fldName, out DbField? fld) && fld != null) {
                        // Binary data
                        if (fld.IsBlob) {
                            byte[] data = (byte[])val;
                            if (resize && data.Length > 0)
                                ResizeBinary(ref data, ref width, ref height);
                            string? contentType = "";

                            // Write file type
                            if (!Empty(fileTypeFld) && !Empty(rs[fileTypeFld]))
                                contentType = ConvertToString(rs[fileTypeFld]);
                            else
                                contentType = ContentType(data);

                            // Write file data
                            if (data.Take(8).SequenceEqual(new byte[] {0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00}) && // Fix Office 2007 documents
                                !data.TakeLast(4).SequenceEqual(new byte[] {0x00, 0x00, 0x00, 0x00}))
                                    data.Concat(new byte[] {0x00, 0x00, 0x00, 0x00});

                            // Clear any debug message
                            // Response?.Clear();

                            // Return file content result // DN
                            string downloadFileName = !Empty(fileNameFld) && !Empty(rs[fileNameFld]) ?
                                ConvertToString(rs[fileNameFld]) :
                                DownloadFileName;
                            string ext = ContentExtension(data).Replace(".", ""); // Get file extension
                            if (ext == "pdf" && Config.EmbedPdf) // Embed Pdf // DN
                                downloadFileName = "";
                            if (!Empty(downloadFileName))
                                return Controller.File(data, contentType, downloadFileName);
                            else
                                return Controller.File(data, contentType);

                        // Upload to folder
                        } else {
                            List<string> files;
                            if (fld.UploadMultiple)
                                files = ConvertToString(val).Split(Config.MultipleUploadSeparator).ToList();
                            else
                                files = new () { ConvertToString(val) };
                            var mi = fld.GetType().GetMethod("GetUploadPath");
                            if (mi != null) // GetUploadPath
                                fld.UploadPath = ConvertToString(mi.Invoke(fld, null));
                            var result = files.ToDictionary(f => f, f => Config.EncryptFilePath
                                ? FullUrl(Config.ApiUrl + Config.ApiFileAction + "/" + TableVar + "/" + Encrypt(fld.PhysicalUploadPath + f))
                                : FullUrl(fld.HrefPath + f));
                            return new JsonBoolResult(new Dictionary<string, object> { { fld.Param, result } }, true);
                        }
                    }
                }
            }
            return JsonBoolResult.FalseResult; // Incorrect key
        }
        #pragma warning restore 1998

        // Table level events

        // Table Load event
        public void TableLoad()
        {
            // Enter your code here
        }

        // Recordset Selecting event
        public void RecordsetSelecting(ref string filter) {
            // Enter your code here
        }

        // Recordset Selected event
        public void RecordsetSelected(DbDataReader rs) {
            // Enter your code here
        }

        // Recordset Searching event
        public void RecordsetSearching(ref string filter) {
            // Enter your code here
        }

        // Recordset Search Validated event
        public void RecordsetSearchValidated() {
            // Enter your code here
        }

        // Row_Selecting event
        public void RowSelecting(ref string filter) {
            // Enter your code here
        }

        // Row Selected event
        public void RowSelected(Dictionary<string, object> row) {
            //Log("Row Selected");
        }

        // Row Inserting event
        public bool RowInserting(Dictionary<string, object>? rsold, Dictionary<string, object> rsnew) {
            // Enter your code here
            // To cancel, set return value to False and error message to CancelMessage
            return true;
        }

        // Row Inserted event
        public void RowInserted(Dictionary<string, object>? rsold, Dictionary<string, object> rsnew) {
            //Log("Row Inserted");
        }

        // Row Updating event
        public bool RowUpdating(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {
            // Enter your code here
            // To cancel, set return value to False and error message to CancelMessage
            return true;
        }

        // Row Updated event
        public void RowUpdated(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {
            //Log("Row Updated");
        }

        // Row Update Conflict event
        public bool RowUpdateConflict(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {
            // Enter your code here
            // To ignore conflict, set return value to false
            return true;
        }

        // Recordset Deleting event
        public bool RowDeleting(Dictionary<string, object> rs) {
            // Enter your code here
            // To cancel, set return value to False and error message to CancelMessage
            return true;
        }

        // Row Deleted event
        public void RowDeleted(Dictionary<string, object> rs) {
            //Log("Row Deleted");
        }

        // Row Export event
        // doc = export document object
        public virtual void RowExport(dynamic doc, DbDataReader rs) {
            //doc.Text.Append("<div>" + MyField.ViewValue +"</div>"); // Build HTML with field value: rs["MyField"] or MyField.ViewValue
        }

        // Email Sending event
        public virtual bool EmailSending(Email email, dynamic? args) {
            //Log(email);
            return true;
        }

        // Lookup Selecting event
        public void LookupSelecting(DbField fld, ref string filter) {
            // Enter your code here
        }

        // Row Rendering event
        public void RowRendering() {
            // Enter your code here
        }

        // Row Rendered event
        public void RowRendered() {
            //VarDump(<FieldName>); // View field properties
        }

        // User ID Filtering event
        public void UserIdFiltering(ref string filter) {
            // Enter your code here
        }
    }
} // End Partial class
