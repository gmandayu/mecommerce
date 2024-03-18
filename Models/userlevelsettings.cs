namespace ASPNETMaker2023.Models;

// Partial class
public partial class mecommerce {
    // Configuration
    public static partial class Config
    {
        //
        // ASP.NET Maker 2023 user level settings
        //

        // User level info
        public static List<UserLevel> UserLevels = new ()
        {
            new () { Id = -2, Name = "Anonymous" },
            new () { Id = 0, Name = "Default" },
            new () { Id = 1, Name = "Merchant" },
            new () { Id = 2, Name = "Supplier" }
        };

        // User level priv info
        public static List<UserLevelPermission> UserLevelPermissions = new ()
        {
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}UserLevelPermissions", Id = -2, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}UserLevelPermissions", Id = 0, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}UserLevelPermissions", Id = 1, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}UserLevelPermissions", Id = 2, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}UserLevels", Id = -2, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}UserLevels", Id = 0, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}UserLevels", Id = 1, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}UserLevels", Id = 2, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Countries", Id = -2, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Countries", Id = 0, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Countries", Id = 1, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Countries", Id = 2, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Users", Id = -2, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Users", Id = 0, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Users", Id = 1, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Users", Id = 2, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}AuditTrail", Id = -2, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}AuditTrail", Id = 0, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}AuditTrail", Id = 1, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}AuditTrail", Id = 2, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Subscriptions", Id = -2, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Subscriptions", Id = 0, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Subscriptions", Id = 1, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Subscriptions", Id = 2, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Customers", Id = -2, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Customers", Id = 0, Permission = 0 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Customers", Id = 1, Permission = 367 },
            new () { Table = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}Customers", Id = 2, Permission = 367 }
        };

        // User level table info // DN
        public static List<UserLevelTablePermission> UserLevelTablePermissions = new ()
        {
            new () { TableName = "UserLevelPermissions", TableVar = "UserLevelPermissions", Caption = "User Level Permissions", Allowed = true, ProjectId = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}", Url = "userlevelpermissionslist" },
            new () { TableName = "UserLevels", TableVar = "UserLevels", Caption = "User Levels", Allowed = true, ProjectId = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}", Url = "userlevelslist" },
            new () { TableName = "Countries", TableVar = "Countries", Caption = "Countries", Allowed = true, ProjectId = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}", Url = "countrieslist" },
            new () { TableName = "Users", TableVar = "Users", Caption = "Users", Allowed = true, ProjectId = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}", Url = "userslist" },
            new () { TableName = "AuditTrail", TableVar = "AuditTrail", Caption = "Audit Trail", Allowed = true, ProjectId = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}", Url = "audittraillist" },
            new () { TableName = "Subscriptions", TableVar = "Subscriptions", Caption = "Subscriptions", Allowed = true, ProjectId = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}", Url = "subscriptionslist" },
            new () { TableName = "Customers", TableVar = "Customers", Caption = "Customers", Allowed = true, ProjectId = "{0AA1F10E-58AB-481A-991C-E9F0FF4ED711}", Url = "customerslist" }
        };
    }
} // End Partial class
