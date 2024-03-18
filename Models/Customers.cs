namespace ASPNETMaker2023.Models;

// Partial class
public partial class mecommerce {
    /// <summary>
    /// customers
    /// </summary>
    [MaybeNull]
    public static Customers customers
    {
        get => HttpData.Resolve<Customers>("Customers");
        set => HttpData["Customers"] = value;
    }

    /// <summary>
    /// Table class for Customers
    /// </summary>
    public class Customers : DbTable, IQueryFactory
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

        public readonly DbField<SqlDbType> CustomerID;

        public readonly DbField<SqlDbType> FirstName;

        public readonly DbField<SqlDbType> MiddleName;

        public readonly DbField<SqlDbType> LastName;

        public readonly DbField<SqlDbType> Gender;

        public readonly DbField<SqlDbType> PlaceOfBirth;

        public readonly DbField<SqlDbType> DateOfBirth;

        public readonly DbField<SqlDbType> PrimaryAddress;

        public readonly DbField<SqlDbType> PrimaryAddressCity;

        public readonly DbField<SqlDbType> PrimaryAddressPostCode;

        public readonly DbField<SqlDbType> PrimaryAddressCountryID;

        public readonly DbField<SqlDbType> AlternativeAddress;

        public readonly DbField<SqlDbType> AlternativeAddressCity;

        public readonly DbField<SqlDbType> AlternativeAddressPostCode;

        public readonly DbField<SqlDbType> AlternativeAddressCountryID;

        public readonly DbField<SqlDbType> MobileNumber;

        public readonly DbField<SqlDbType> UserID;

        public readonly DbField<SqlDbType> Status;

        public readonly DbField<SqlDbType> CreatedBy;

        public readonly DbField<SqlDbType> CreatedDateTime;

        public readonly DbField<SqlDbType> UpdatedBy;

        public readonly DbField<SqlDbType> UpdatedDateTime;

        // Constructor
        public Customers()
        {
            // Language object // DN
            Language = ResolveLanguage();
            TableVar = "Customers";
            Name = "Customers";
            Type = "TABLE";
            UpdateTable = "dbo.Customers"; // Update Table
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

            // CustomerID
            CustomerID = new (this, "x_CustomerID", 3, SqlDbType.Int) {
                Name = "CustomerID",
                Expression = "[CustomerID]",
                BasicSearchExpression = "CAST([CustomerID] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[CustomerID]",
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
                CustomMessage = Language.FieldPhrase("Customers", "CustomerID", "CustomMsg"),
                IsUpload = false
            };
            Fields.Add("CustomerID", CustomerID);

            // FirstName
            FirstName = new (this, "x_FirstName", 202, SqlDbType.NVarChar) {
                Name = "FirstName",
                Expression = "[FirstName]",
                UseBasicSearch = true,
                BasicSearchExpression = "[FirstName]",
                DateTimeFormat = -1,
                VirtualExpression = "[FirstName]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "FirstName", "CustomMsg"),
                IsUpload = false
            };
            FirstName.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(FirstName, "Customers", true, "FirstName", new List<string> {"FirstName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(FirstName, "Customers", true, "FirstName", new List<string> {"FirstName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(FirstName, "Customers", true, "FirstName", new List<string> {"FirstName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("FirstName", FirstName);

            // MiddleName
            MiddleName = new (this, "x_MiddleName", 202, SqlDbType.NVarChar) {
                Name = "MiddleName",
                Expression = "[MiddleName]",
                UseBasicSearch = true,
                BasicSearchExpression = "[MiddleName]",
                DateTimeFormat = -1,
                VirtualExpression = "[MiddleName]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "MiddleName", "CustomMsg"),
                IsUpload = false
            };
            MiddleName.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(MiddleName, "Customers", true, "MiddleName", new List<string> {"MiddleName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(MiddleName, "Customers", true, "MiddleName", new List<string> {"MiddleName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(MiddleName, "Customers", true, "MiddleName", new List<string> {"MiddleName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("MiddleName", MiddleName);

            // LastName
            LastName = new (this, "x_LastName", 202, SqlDbType.NVarChar) {
                Name = "LastName",
                Expression = "[LastName]",
                UseBasicSearch = true,
                BasicSearchExpression = "[LastName]",
                DateTimeFormat = -1,
                VirtualExpression = "[LastName]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "LastName", "CustomMsg"),
                IsUpload = false
            };
            LastName.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(LastName, "Customers", true, "LastName", new List<string> {"LastName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(LastName, "Customers", true, "LastName", new List<string> {"LastName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(LastName, "Customers", true, "LastName", new List<string> {"LastName", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("LastName", LastName);

            // Gender
            Gender = new (this, "x_Gender", 202, SqlDbType.NVarChar) {
                Name = "Gender",
                Expression = "[Gender]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Gender]",
                DateTimeFormat = -1,
                VirtualExpression = "[Gender]",
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
                OptionCount = 2,
                SearchOperators = new () { "=", "<>", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "Gender", "CustomMsg"),
                IsUpload = false
            };
            Gender.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(Gender, "Customers", true, "Gender", new List<string> {"Gender", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(Gender, "Customers", true, "Gender", new List<string> {"Gender", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(Gender, "Customers", true, "Gender", new List<string> {"Gender", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("Gender", Gender);

            // PlaceOfBirth
            PlaceOfBirth = new (this, "x_PlaceOfBirth", 202, SqlDbType.NVarChar) {
                Name = "PlaceOfBirth",
                Expression = "[PlaceOfBirth]",
                UseBasicSearch = true,
                BasicSearchExpression = "[PlaceOfBirth]",
                DateTimeFormat = -1,
                VirtualExpression = "[PlaceOfBirth]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "PlaceOfBirth", "CustomMsg"),
                IsUpload = false
            };
            PlaceOfBirth.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(PlaceOfBirth, "Customers", true, "PlaceOfBirth", new List<string> {"PlaceOfBirth", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(PlaceOfBirth, "Customers", true, "PlaceOfBirth", new List<string> {"PlaceOfBirth", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(PlaceOfBirth, "Customers", true, "PlaceOfBirth", new List<string> {"PlaceOfBirth", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("PlaceOfBirth", PlaceOfBirth);

            // DateOfBirth
            DateOfBirth = new (this, "x_DateOfBirth", 133, SqlDbType.DateTime) {
                Name = "DateOfBirth",
                Expression = "[DateOfBirth]",
                BasicSearchExpression = CastDateFieldForLike("[DateOfBirth]", 0, "DB"),
                DateTimeFormat = 0,
                VirtualExpression = "[DateOfBirth]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                DefaultErrorMessage = ConvertToString(Language.Phrase("IncorrectDate")).Replace("%s", CurrentDateTimeFormat.ShortDatePattern),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "DateOfBirth", "CustomMsg"),
                IsUpload = false
            };
            Fields.Add("DateOfBirth", DateOfBirth);

            // PrimaryAddress
            PrimaryAddress = new (this, "x_PrimaryAddress", 203, SqlDbType.NText) {
                Name = "PrimaryAddress",
                Expression = "[PrimaryAddress]",
                BasicSearchExpression = "[PrimaryAddress]",
                DateTimeFormat = -1,
                VirtualExpression = "[PrimaryAddress]",
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
                CustomMessage = Language.FieldPhrase("Customers", "PrimaryAddress", "CustomMsg"),
                IsUpload = false
            };
            PrimaryAddress.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(PrimaryAddress, "Customers", true, "PrimaryAddress", new List<string> {"PrimaryAddress", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(PrimaryAddress, "Customers", true, "PrimaryAddress", new List<string> {"PrimaryAddress", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(PrimaryAddress, "Customers", true, "PrimaryAddress", new List<string> {"PrimaryAddress", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("PrimaryAddress", PrimaryAddress);

            // PrimaryAddressCity
            PrimaryAddressCity = new (this, "x_PrimaryAddressCity", 202, SqlDbType.NVarChar) {
                Name = "PrimaryAddressCity",
                Expression = "[PrimaryAddressCity]",
                UseBasicSearch = true,
                BasicSearchExpression = "[PrimaryAddressCity]",
                DateTimeFormat = -1,
                VirtualExpression = "[PrimaryAddressCity]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "PrimaryAddressCity", "CustomMsg"),
                IsUpload = false
            };
            PrimaryAddressCity.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(PrimaryAddressCity, "Customers", true, "PrimaryAddressCity", new List<string> {"PrimaryAddressCity", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(PrimaryAddressCity, "Customers", true, "PrimaryAddressCity", new List<string> {"PrimaryAddressCity", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(PrimaryAddressCity, "Customers", true, "PrimaryAddressCity", new List<string> {"PrimaryAddressCity", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("PrimaryAddressCity", PrimaryAddressCity);

            // PrimaryAddressPostCode
            PrimaryAddressPostCode = new (this, "x_PrimaryAddressPostCode", 202, SqlDbType.NVarChar) {
                Name = "PrimaryAddressPostCode",
                Expression = "[PrimaryAddressPostCode]",
                UseBasicSearch = true,
                BasicSearchExpression = "[PrimaryAddressPostCode]",
                DateTimeFormat = -1,
                VirtualExpression = "[PrimaryAddressPostCode]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "number",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "PrimaryAddressPostCode", "CustomMsg"),
                IsUpload = false
            };
            PrimaryAddressPostCode.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(PrimaryAddressPostCode, "Customers", true, "PrimaryAddressPostCode", new List<string> {"PrimaryAddressPostCode", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(PrimaryAddressPostCode, "Customers", true, "PrimaryAddressPostCode", new List<string> {"PrimaryAddressPostCode", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(PrimaryAddressPostCode, "Customers", true, "PrimaryAddressPostCode", new List<string> {"PrimaryAddressPostCode", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("PrimaryAddressPostCode", PrimaryAddressPostCode);

            // PrimaryAddressCountryID
            PrimaryAddressCountryID = new (this, "x_PrimaryAddressCountryID", 3, SqlDbType.Int) {
                Name = "PrimaryAddressCountryID",
                Expression = "[PrimaryAddressCountryID]",
                BasicSearchExpression = "CAST([PrimaryAddressCountryID] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[PrimaryAddressCountryID]",
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
                CustomMessage = Language.FieldPhrase("Customers", "PrimaryAddressCountryID", "CustomMsg"),
                IsUpload = false
            };
            PrimaryAddressCountryID.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(PrimaryAddressCountryID, "Countries", true, "CountryID", new List<string> {"NiceName", "Name", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "CONCAT([NiceName],'" + ValueSeparator(1, PrimaryAddressCountryID) + "',[Name])"),
                "id-ID" => new Lookup<DbField>(PrimaryAddressCountryID, "Countries", true, "CountryID", new List<string> {"NiceName", "Name", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "CONCAT([NiceName],'" + ValueSeparator(1, PrimaryAddressCountryID) + "',[Name])"),
                _ => new Lookup<DbField>(PrimaryAddressCountryID, "Countries", true, "CountryID", new List<string> {"NiceName", "Name", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "CONCAT([NiceName],'" + ValueSeparator(1, PrimaryAddressCountryID) + "',[Name])")
            };
            Fields.Add("PrimaryAddressCountryID", PrimaryAddressCountryID);

            // AlternativeAddress
            AlternativeAddress = new (this, "x_AlternativeAddress", 203, SqlDbType.NText) {
                Name = "AlternativeAddress",
                Expression = "[AlternativeAddress]",
                BasicSearchExpression = "[AlternativeAddress]",
                DateTimeFormat = -1,
                VirtualExpression = "[AlternativeAddress]",
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
                CustomMessage = Language.FieldPhrase("Customers", "AlternativeAddress", "CustomMsg"),
                IsUpload = false
            };
            AlternativeAddress.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(AlternativeAddress, "Customers", true, "AlternativeAddress", new List<string> {"AlternativeAddress", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(AlternativeAddress, "Customers", true, "AlternativeAddress", new List<string> {"AlternativeAddress", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(AlternativeAddress, "Customers", true, "AlternativeAddress", new List<string> {"AlternativeAddress", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("AlternativeAddress", AlternativeAddress);

            // AlternativeAddressCity
            AlternativeAddressCity = new (this, "x_AlternativeAddressCity", 202, SqlDbType.NVarChar) {
                Name = "AlternativeAddressCity",
                Expression = "[AlternativeAddressCity]",
                UseBasicSearch = true,
                BasicSearchExpression = "[AlternativeAddressCity]",
                DateTimeFormat = -1,
                VirtualExpression = "[AlternativeAddressCity]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "AlternativeAddressCity", "CustomMsg"),
                IsUpload = false
            };
            AlternativeAddressCity.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(AlternativeAddressCity, "Customers", true, "AlternativeAddressCity", new List<string> {"AlternativeAddressCity", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(AlternativeAddressCity, "Customers", true, "AlternativeAddressCity", new List<string> {"AlternativeAddressCity", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(AlternativeAddressCity, "Customers", true, "AlternativeAddressCity", new List<string> {"AlternativeAddressCity", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("AlternativeAddressCity", AlternativeAddressCity);

            // AlternativeAddressPostCode
            AlternativeAddressPostCode = new (this, "x_AlternativeAddressPostCode", 202, SqlDbType.NVarChar) {
                Name = "AlternativeAddressPostCode",
                Expression = "[AlternativeAddressPostCode]",
                UseBasicSearch = true,
                BasicSearchExpression = "[AlternativeAddressPostCode]",
                DateTimeFormat = -1,
                VirtualExpression = "[AlternativeAddressPostCode]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "number",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "AlternativeAddressPostCode", "CustomMsg"),
                IsUpload = false
            };
            AlternativeAddressPostCode.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(AlternativeAddressPostCode, "Customers", true, "AlternativeAddressPostCode", new List<string> {"AlternativeAddressPostCode", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(AlternativeAddressPostCode, "Customers", true, "AlternativeAddressPostCode", new List<string> {"AlternativeAddressPostCode", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(AlternativeAddressPostCode, "Customers", true, "AlternativeAddressPostCode", new List<string> {"AlternativeAddressPostCode", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("AlternativeAddressPostCode", AlternativeAddressPostCode);

            // AlternativeAddressCountryID
            AlternativeAddressCountryID = new (this, "x_AlternativeAddressCountryID", 3, SqlDbType.Int) {
                Name = "AlternativeAddressCountryID",
                Expression = "[AlternativeAddressCountryID]",
                BasicSearchExpression = "CAST([AlternativeAddressCountryID] AS NVARCHAR)",
                DateTimeFormat = -1,
                VirtualExpression = "[AlternativeAddressCountryID]",
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
                CustomMessage = Language.FieldPhrase("Customers", "AlternativeAddressCountryID", "CustomMsg"),
                IsUpload = false
            };
            AlternativeAddressCountryID.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(AlternativeAddressCountryID, "Countries", true, "CountryID", new List<string> {"NiceName", "Name", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "CONCAT([NiceName],'" + ValueSeparator(1, AlternativeAddressCountryID) + "',[Name])"),
                "id-ID" => new Lookup<DbField>(AlternativeAddressCountryID, "Countries", true, "CountryID", new List<string> {"NiceName", "Name", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "CONCAT([NiceName],'" + ValueSeparator(1, AlternativeAddressCountryID) + "',[Name])"),
                _ => new Lookup<DbField>(AlternativeAddressCountryID, "Countries", true, "CountryID", new List<string> {"NiceName", "Name", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "CONCAT([NiceName],'" + ValueSeparator(1, AlternativeAddressCountryID) + "',[Name])")
            };
            Fields.Add("AlternativeAddressCountryID", AlternativeAddressCountryID);

            // MobileNumber
            MobileNumber = new (this, "x_MobileNumber", 202, SqlDbType.NVarChar) {
                Name = "MobileNumber",
                Expression = "[MobileNumber]",
                UseBasicSearch = true,
                BasicSearchExpression = "[MobileNumber]",
                DateTimeFormat = -1,
                VirtualExpression = "[MobileNumber]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "tel",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "MobileNumber", "CustomMsg"),
                IsUpload = false
            };
            MobileNumber.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(MobileNumber, "Customers", true, "MobileNumber", new List<string> {"MobileNumber", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(MobileNumber, "Customers", true, "MobileNumber", new List<string> {"MobileNumber", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(MobileNumber, "Customers", true, "MobileNumber", new List<string> {"MobileNumber", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("MobileNumber", MobileNumber);

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
                HtmlTag = "SELECT",
                InputTextType = "text",
                UsePleaseSelect = true, // Use PleaseSelect by default
                PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
                UseFilter = true, // Table header filter
                DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
                SearchOperators = new () { "=", "<>", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "UserID", "CustomMsg"),
                IsUpload = false
            };
            UserID.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(UserID, "Users", true, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]"),
                "id-ID" => new Lookup<DbField>(UserID, "Users", true, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]"),
                _ => new Lookup<DbField>(UserID, "Users", true, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]")
            };
            Fields.Add("UserID", UserID);

            // Status
            Status = new (this, "x_Status", 202, SqlDbType.NVarChar) {
                Name = "Status",
                Expression = "[Status]",
                UseBasicSearch = true,
                BasicSearchExpression = "[Status]",
                DateTimeFormat = -1,
                VirtualExpression = "[Status]",
                IsVirtual = false,
                ForceSelection = false,
                SelectMultiple = false,
                VirtualSearch = false,
                ViewTag = "FORMATTED TEXT",
                HtmlTag = "TEXT",
                InputTextType = "text",
                UseFilter = true, // Table header filter
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "STARTS WITH", "NOT STARTS WITH", "LIKE", "NOT LIKE", "ENDS WITH", "NOT ENDS WITH", "IS EMPTY", "IS NOT EMPTY", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "Status", "CustomMsg"),
                IsUpload = false
            };
            Status.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(Status, "Customers", true, "Status", new List<string> {"Status", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                "id-ID" => new Lookup<DbField>(Status, "Customers", true, "Status", new List<string> {"Status", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", ""),
                _ => new Lookup<DbField>(Status, "Customers", true, "Status", new List<string> {"Status", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "")
            };
            Fields.Add("Status", Status);

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
                UsePleaseSelect = true, // Use PleaseSelect by default
                PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
                UseFilter = true, // Table header filter
                DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
                SearchOperators = new () { "=", "<>", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "CreatedBy", "CustomMsg"),
                IsUpload = false
            };
            CreatedBy.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(CreatedBy, "Users", true, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]"),
                "id-ID" => new Lookup<DbField>(CreatedBy, "Users", true, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]"),
                _ => new Lookup<DbField>(CreatedBy, "Users", true, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]")
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
                DefaultErrorMessage = ConvertToString(Language.Phrase("IncorrectDate")).Replace("%s", DateFormat(1)),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "CreatedDateTime", "CustomMsg"),
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
                UsePleaseSelect = true, // Use PleaseSelect by default
                PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
                UseFilter = true, // Table header filter
                DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
                SearchOperators = new () { "=", "<>", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "UpdatedBy", "CustomMsg"),
                IsUpload = false
            };
            UpdatedBy.Lookup = CurrentLanguage switch {
                "en-US" => new Lookup<DbField>(UpdatedBy, "Users", true, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]"),
                "id-ID" => new Lookup<DbField>(UpdatedBy, "Users", true, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]"),
                _ => new Lookup<DbField>(UpdatedBy, "Users", true, "UserID", new List<string> {"Email", "", "", ""}, "", "", new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "", "[Email]")
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
                DefaultErrorMessage = ConvertToString(Language.Phrase("IncorrectDate")).Replace("%s", DateFormat(1)),
                SearchOperators = new () { "=", "<>", "IN", "NOT IN", "<", "<=", ">", ">=", "BETWEEN", "NOT BETWEEN", "IS NULL", "IS NOT NULL" },
                CustomMessage = Language.FieldPhrase("Customers", "UpdatedDateTime", "CustomMsg"),
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
            get => _sqlFrom ?? "dbo.Customers";
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
                CustomerID.SetDbValue(lastInsertedId);
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
                CustomerID.DbValue = row["CustomerID"]; // Set DB value only
                FirstName.DbValue = row["FirstName"]; // Set DB value only
                MiddleName.DbValue = row["MiddleName"]; // Set DB value only
                LastName.DbValue = row["LastName"]; // Set DB value only
                Gender.DbValue = row["Gender"]; // Set DB value only
                PlaceOfBirth.DbValue = row["PlaceOfBirth"]; // Set DB value only
                DateOfBirth.DbValue = row["DateOfBirth"]; // Set DB value only
                PrimaryAddress.DbValue = row["PrimaryAddress"]; // Set DB value only
                PrimaryAddressCity.DbValue = row["PrimaryAddressCity"]; // Set DB value only
                PrimaryAddressPostCode.DbValue = row["PrimaryAddressPostCode"]; // Set DB value only
                PrimaryAddressCountryID.DbValue = row["PrimaryAddressCountryID"]; // Set DB value only
                AlternativeAddress.DbValue = row["AlternativeAddress"]; // Set DB value only
                AlternativeAddressCity.DbValue = row["AlternativeAddressCity"]; // Set DB value only
                AlternativeAddressPostCode.DbValue = row["AlternativeAddressPostCode"]; // Set DB value only
                AlternativeAddressCountryID.DbValue = row["AlternativeAddressCountryID"]; // Set DB value only
                MobileNumber.DbValue = row["MobileNumber"]; // Set DB value only
                UserID.DbValue = row["UserID"]; // Set DB value only
                Status.DbValue = row["Status"]; // Set DB value only
                CreatedBy.DbValue = row["CreatedBy"]; // Set DB value only
                CreatedDateTime.DbValue = row["CreatedDateTime"]; // Set DB value only
                UpdatedBy.DbValue = row["UpdatedBy"]; // Set DB value only
                UpdatedDateTime.DbValue = row["UpdatedDateTime"]; // Set DB value only
            } catch {}
        }

        public void DeleteUploadedFiles(Dictionary<string, object> row)
        {
            LoadDbValues(row);
        }

        // Record filter WHERE clause
        private string _sqlKeyFilter => "[CustomerID] = @CustomerID@";

        #pragma warning disable 168, 219
        // Get record filter as string
        public string GetRecordFilter(Dictionary<string, object>? row = null, bool current = false)
        {
            string keyFilter = _sqlKeyFilter;
            object? val = null, result = "";
            val = row?.TryGetValue("CustomerID", out result) ?? false
                ? result
                : !Empty(CustomerID.OldValue) && !current ? CustomerID.OldValue : CustomerID.CurrentValue;
            if (!IsNumeric(val))
                return "0=1"; // Invalid key
            if (val == null)
                return "0=1"; // Invalid key
            keyFilter = keyFilter.Replace("@CustomerID@", AdjustSql(val, DbId)); // Replace key value
            return keyFilter;
        }

        // Get record filter as Dictionary // DN
        public Dictionary<string, object>? GetRowFilterAsDictionary(IDictionary<string, object>? row = null)
        {
            Dictionary<string, object>? keyFilter = new ();
            object? val = null, result;
            val = row?.TryGetValue("CustomerID", out result) ?? false
                ? result
                : !Empty(CustomerID.OldValue) ? CustomerID.OldValue : CustomerID.CurrentValue;
            if (!IsNumeric(val))
                return null; // Invalid key
            if (Empty(val))
                return null; // Invalid key
            keyFilter.Add("CustomerID", val); // Add key value
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
                    return "customerslist";
                }
            }
            set {
                Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
            }
        }

        // Get modal caption
        public string GetModalCaption(string pageName)
        {
            if (SameString(pageName, "customersview"))
                return Language.Phrase("View");
            else if (SameString(pageName, "customersedit"))
                return Language.Phrase("Edit");
            else if (SameString(pageName, "customersadd"))
                return Language.Phrase("Add");
            else
                return "";
        }

        // Default route URL
        public string DefaultRouteUrl
        {
            get {
                return "customerslist";
            }
        }

        // API page name
        public string GetApiPageName(string action)
        {
            return action.ToLowerInvariant() switch {
                Config.ApiViewAction => "CustomersView",
                Config.ApiAddAction => "CustomersAdd",
                Config.ApiEditAction => "CustomersEdit",
                Config.ApiDeleteAction => "CustomersDelete",
                Config.ApiListAction => "CustomersList",
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
        public string ListUrl => "customerslist";

        // View URL
        public string ViewUrl => GetViewUrl();

        // View URL
        public string GetViewUrl(string parm = "")
        {
            string url = "";
            if (!Empty(parm))
                url = KeyUrl("customersview", parm);
            else
                url = KeyUrl("customersview", Config.TableShowDetail + "=");
            return AddMasterUrl(url);
        }

        // Add URL
        public string AddUrl { get; set; } = "customersadd";

        // Add URL
        public string GetAddUrl(string parm = "")
        {
            string url = "";
            if (!Empty(parm))
                url = "customersadd?" + parm;
            else
                url = "customersadd";
            return AppPath(AddMasterUrl(url));
        }

        // Edit URL
        public string EditUrl => GetEditUrl();

        // Edit URL (with parameter)
        public string GetEditUrl(string parm = "")
        {
            string url = "";
            url = KeyUrl("customersedit", parm);
            return AppPath(AddMasterUrl(url)); // DN
        }

        // Inline edit URL
        public string InlineEditUrl =>
            AppPath(AddMasterUrl(KeyUrl("customerslist", "action=edit"))); // DN

        // Copy URL
        public string CopyUrl => GetCopyUrl();

        // Copy URL
        public string GetCopyUrl(string parm = "")
        {
            string url = "";
            url = KeyUrl("customersadd", parm);
            return AppPath(AddMasterUrl(url)); // DN
        }

        // Inline copy URL
        public string InlineCopyUrl =>
            AppPath(AddMasterUrl(KeyUrl("customerslist", "action=copy"))); // DN

        // Delete URL
        public string GetDeleteUrl(string parm = "")
        {
            return UseAjaxActions && Param<bool>("infinitescroll") && CurrentPageID() == "list"
                ? AppPath(KeyUrl(Config.ApiUrl + Config.ApiDeleteAction + "/" + TableVar))
                : AppPath(KeyUrl("customersdelete", parm)); // DN
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
            json += "\"CustomerID\":" + ConvertToJson(CustomerID.CurrentValue, "number", true);
            json = "{" + json + "}";
            if (htmlEncode)
                json = HtmlEncode(json);
            return json;
        }

        // Add key value to URL
        public string KeyUrl(string url, string parm = "") { // DN
            if (!IsNull(CustomerID.CurrentValue)) {
                url += "/" + CustomerID.CurrentValue;
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
            val = current ? ConvertToString(CustomerID.CurrentValue) ?? "" : ConvertToString(CustomerID.OldValue) ?? "";
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
            val = row?.TryGetValue("CustomerID", out result) ?? false ? ConvertToString(result) : null;
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
                    CustomerID.CurrentValue = keys[0];
                } else {
                    CustomerID.OldValue = keys[0];
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
                if (RouteValues.TryGetValue("CustomerID", out object? v0)) { // CustomerID // DN
                    key = UrlDecode(v0); // DN
                } else if (IsApi() && !Empty(keyValues)) {
                    key = keyValues[0];
                } else {
                    key = Param("CustomerID");
                }
                keysList.Add(key);
            }
            // Check keys
            foreach (var keys in keysList) {
                if (!IsNumeric(keys)) // CustomerID
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
                    CustomerID.CurrentValue = keys;
                else
                    CustomerID.OldValue = keys;
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
            CustomerID.SetDbValue(dr["CustomerID"]);
            FirstName.SetDbValue(dr["FirstName"]);
            MiddleName.SetDbValue(dr["MiddleName"]);
            LastName.SetDbValue(dr["LastName"]);
            Gender.SetDbValue(dr["Gender"]);
            PlaceOfBirth.SetDbValue(dr["PlaceOfBirth"]);
            DateOfBirth.SetDbValue(dr["DateOfBirth"]);
            PrimaryAddress.SetDbValue(dr["PrimaryAddress"]);
            PrimaryAddressCity.SetDbValue(dr["PrimaryAddressCity"]);
            PrimaryAddressPostCode.SetDbValue(dr["PrimaryAddressPostCode"]);
            PrimaryAddressCountryID.SetDbValue(dr["PrimaryAddressCountryID"]);
            AlternativeAddress.SetDbValue(dr["AlternativeAddress"]);
            AlternativeAddressCity.SetDbValue(dr["AlternativeAddressCity"]);
            AlternativeAddressPostCode.SetDbValue(dr["AlternativeAddressPostCode"]);
            AlternativeAddressCountryID.SetDbValue(dr["AlternativeAddressCountryID"]);
            MobileNumber.SetDbValue(dr["MobileNumber"]);
            UserID.SetDbValue(dr["UserID"]);
            Status.SetDbValue(dr["Status"]);
            CreatedBy.SetDbValue(dr["CreatedBy"]);
            CreatedDateTime.SetDbValue(dr["CreatedDateTime"]);
            UpdatedBy.SetDbValue(dr["UpdatedBy"]);
            UpdatedDateTime.SetDbValue(dr["UpdatedDateTime"]);
        }

        // Render list content
        public async Task<string> RenderListContent(string filter)
        {
            string pageName = "CustomersList";
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

            // CustomerID
            CustomerID.CellCssStyle = "white-space: nowrap;";

            // FirstName
            FirstName.CellCssStyle = "white-space: nowrap;";

            // MiddleName
            MiddleName.CellCssStyle = "white-space: nowrap;";

            // LastName
            LastName.CellCssStyle = "white-space: nowrap;";

            // Gender
            Gender.CellCssStyle = "white-space: nowrap;";

            // PlaceOfBirth
            PlaceOfBirth.CellCssStyle = "white-space: nowrap;";

            // DateOfBirth
            DateOfBirth.CellCssStyle = "white-space: nowrap;";

            // PrimaryAddress
            PrimaryAddress.CellCssStyle = "white-space: nowrap;";

            // PrimaryAddressCity
            PrimaryAddressCity.CellCssStyle = "white-space: nowrap;";

            // PrimaryAddressPostCode
            PrimaryAddressPostCode.CellCssStyle = "white-space: nowrap;";

            // PrimaryAddressCountryID
            PrimaryAddressCountryID.CellCssStyle = "white-space: nowrap;";

            // AlternativeAddress
            AlternativeAddress.CellCssStyle = "white-space: nowrap;";

            // AlternativeAddressCity
            AlternativeAddressCity.CellCssStyle = "white-space: nowrap;";

            // AlternativeAddressPostCode
            AlternativeAddressPostCode.CellCssStyle = "white-space: nowrap;";

            // AlternativeAddressCountryID
            AlternativeAddressCountryID.CellCssStyle = "white-space: nowrap;";

            // MobileNumber
            MobileNumber.CellCssStyle = "white-space: nowrap;";

            // UserID
            UserID.CellCssStyle = "white-space: nowrap;";

            // Status
            Status.CellCssStyle = "white-space: nowrap;";

            // CreatedBy
            CreatedBy.CellCssStyle = "white-space: nowrap;";

            // CreatedDateTime
            CreatedDateTime.CellCssStyle = "white-space: nowrap;";

            // UpdatedBy
            UpdatedBy.CellCssStyle = "white-space: nowrap;";

            // UpdatedDateTime
            UpdatedDateTime.CellCssStyle = "white-space: nowrap;";

            // CustomerID
            CustomerID.ViewValue = CustomerID.CurrentValue;
            CustomerID.ViewCustomAttributes = "";

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

            // CustomerID
            CustomerID.HrefValue = "";
            CustomerID.TooltipValue = "";

            // FirstName
            FirstName.HrefValue = "";
            FirstName.TooltipValue = "";

            // MiddleName
            MiddleName.HrefValue = "";
            MiddleName.TooltipValue = "";

            // LastName
            LastName.HrefValue = "";
            LastName.TooltipValue = "";

            // Gender
            Gender.HrefValue = "";
            Gender.TooltipValue = "";

            // PlaceOfBirth
            PlaceOfBirth.HrefValue = "";
            PlaceOfBirth.TooltipValue = "";

            // DateOfBirth
            DateOfBirth.HrefValue = "";
            DateOfBirth.TooltipValue = "";

            // PrimaryAddress
            PrimaryAddress.HrefValue = "";
            PrimaryAddress.TooltipValue = "";

            // PrimaryAddressCity
            PrimaryAddressCity.HrefValue = "";
            PrimaryAddressCity.TooltipValue = "";

            // PrimaryAddressPostCode
            PrimaryAddressPostCode.HrefValue = "";
            PrimaryAddressPostCode.TooltipValue = "";

            // PrimaryAddressCountryID
            PrimaryAddressCountryID.HrefValue = "";
            PrimaryAddressCountryID.TooltipValue = "";

            // AlternativeAddress
            AlternativeAddress.HrefValue = "";
            AlternativeAddress.TooltipValue = "";

            // AlternativeAddressCity
            AlternativeAddressCity.HrefValue = "";
            AlternativeAddressCity.TooltipValue = "";

            // AlternativeAddressPostCode
            AlternativeAddressPostCode.HrefValue = "";
            AlternativeAddressPostCode.TooltipValue = "";

            // AlternativeAddressCountryID
            AlternativeAddressCountryID.HrefValue = "";
            AlternativeAddressCountryID.TooltipValue = "";

            // MobileNumber
            MobileNumber.HrefValue = "";
            MobileNumber.TooltipValue = "";

            // UserID
            UserID.HrefValue = "";
            UserID.TooltipValue = "";

            // Status
            Status.HrefValue = "";
            Status.TooltipValue = "";

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

            // CustomerID
            CustomerID.SetupEditAttributes();
            CustomerID.EditValue = CustomerID.CurrentValue;
            CustomerID.ViewCustomAttributes = "";

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
                filterWrk = "";
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
                filterWrk = "";
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
                filterWrk = "";
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

            // CreatedBy
            CreatedBy.SetupEditAttributes();
            curVal = ConvertToString(CreatedBy.CurrentValue)?.Trim() ?? "";
            if (CreatedBy.Lookup != null && IsDictionary(CreatedBy.Lookup?.Options) && CreatedBy.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                CreatedBy.EditValue = CreatedBy.Lookup?.Options.Values.ToList();
            } else { // Lookup from database
                filterWrk = "";
                sqlWrk = CreatedBy.Lookup?.GetSql(true, filterWrk, null, this, false, true);
                rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                CreatedBy.EditValue = rswrk;
            }
            CreatedBy.PlaceHolder = RemoveHtml(CreatedBy.Caption);
            if (!Empty(CreatedBy.EditValue) && IsNumeric(CreatedBy.EditValue))
                CreatedBy.EditValue = FormatNumber(CreatedBy.EditValue, CreatedBy.FormatPattern);

            // CreatedDateTime
            CreatedDateTime.SetupEditAttributes();
            CreatedDateTime.EditValue = FormatDateTime(CreatedDateTime.CurrentValue, CreatedDateTime.FormatPattern); // DN
            CreatedDateTime.PlaceHolder = RemoveHtml(CreatedDateTime.Caption);

            // UpdatedBy
            UpdatedBy.SetupEditAttributes();
            curVal = ConvertToString(UpdatedBy.CurrentValue)?.Trim() ?? "";
            if (UpdatedBy.Lookup != null && IsDictionary(UpdatedBy.Lookup?.Options) && UpdatedBy.Lookup?.Options.Values.Count > 0) { // Load from cache // DN
                UpdatedBy.EditValue = UpdatedBy.Lookup?.Options.Values.ToList();
            } else { // Lookup from database
                filterWrk = "";
                sqlWrk = UpdatedBy.Lookup?.GetSql(true, filterWrk, null, this, false, true);
                rswrk = sqlWrk != null ? Connection.GetRows(sqlWrk) : null; // Must use Sync to avoid overwriting ViewValue in RenderViewRow
                UpdatedBy.EditValue = rswrk;
            }
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
                        doc.ExportCaption(FirstName);
                        doc.ExportCaption(MiddleName);
                        doc.ExportCaption(LastName);
                        doc.ExportCaption(Gender);
                        doc.ExportCaption(PlaceOfBirth);
                        doc.ExportCaption(DateOfBirth);
                        doc.ExportCaption(PrimaryAddress);
                        doc.ExportCaption(PrimaryAddressCity);
                        doc.ExportCaption(PrimaryAddressPostCode);
                        doc.ExportCaption(PrimaryAddressCountryID);
                        doc.ExportCaption(AlternativeAddress);
                        doc.ExportCaption(AlternativeAddressCity);
                        doc.ExportCaption(AlternativeAddressPostCode);
                        doc.ExportCaption(AlternativeAddressCountryID);
                        doc.ExportCaption(MobileNumber);
                        doc.ExportCaption(UserID);
                        doc.ExportCaption(Status);
                        doc.ExportCaption(CreatedBy);
                        doc.ExportCaption(CreatedDateTime);
                        doc.ExportCaption(UpdatedBy);
                        doc.ExportCaption(UpdatedDateTime);
                    } else {
                        doc.ExportCaption(FirstName);
                        doc.ExportCaption(MiddleName);
                        doc.ExportCaption(LastName);
                        doc.ExportCaption(Gender);
                        doc.ExportCaption(PlaceOfBirth);
                        doc.ExportCaption(DateOfBirth);
                        doc.ExportCaption(PrimaryAddressCity);
                        doc.ExportCaption(PrimaryAddressPostCode);
                        doc.ExportCaption(PrimaryAddressCountryID);
                        doc.ExportCaption(AlternativeAddressCity);
                        doc.ExportCaption(AlternativeAddressPostCode);
                        doc.ExportCaption(AlternativeAddressCountryID);
                        doc.ExportCaption(MobileNumber);
                        doc.ExportCaption(UserID);
                        doc.ExportCaption(Status);
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
                            await doc.ExportField(FirstName);
                            await doc.ExportField(MiddleName);
                            await doc.ExportField(LastName);
                            await doc.ExportField(Gender);
                            await doc.ExportField(PlaceOfBirth);
                            await doc.ExportField(DateOfBirth);
                            await doc.ExportField(PrimaryAddress);
                            await doc.ExportField(PrimaryAddressCity);
                            await doc.ExportField(PrimaryAddressPostCode);
                            await doc.ExportField(PrimaryAddressCountryID);
                            await doc.ExportField(AlternativeAddress);
                            await doc.ExportField(AlternativeAddressCity);
                            await doc.ExportField(AlternativeAddressPostCode);
                            await doc.ExportField(AlternativeAddressCountryID);
                            await doc.ExportField(MobileNumber);
                            await doc.ExportField(UserID);
                            await doc.ExportField(Status);
                            await doc.ExportField(CreatedBy);
                            await doc.ExportField(CreatedDateTime);
                            await doc.ExportField(UpdatedBy);
                            await doc.ExportField(UpdatedDateTime);
                        } else {
                            await doc.ExportField(FirstName);
                            await doc.ExportField(MiddleName);
                            await doc.ExportField(LastName);
                            await doc.ExportField(Gender);
                            await doc.ExportField(PlaceOfBirth);
                            await doc.ExportField(DateOfBirth);
                            await doc.ExportField(PrimaryAddressCity);
                            await doc.ExportField(PrimaryAddressPostCode);
                            await doc.ExportField(PrimaryAddressCountryID);
                            await doc.ExportField(AlternativeAddressCity);
                            await doc.ExportField(AlternativeAddressPostCode);
                            await doc.ExportField(AlternativeAddressCountryID);
                            await doc.ExportField(MobileNumber);
                            await doc.ExportField(UserID);
                            await doc.ExportField(Status);
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

            // No binary fields
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
