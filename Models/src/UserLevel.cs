namespace ASPNETMaker2023.Models;

// Partial class
public partial class mecommerce {
    /// <summary>
    /// User level class
    /// </summary>
    public class UserLevel
    {
        // User level ID
        [SqlKata.Column("UserLevelID")]
        public int Id { set; get; }

        // Name
        [SqlKata.Column("UserLevelName")]
        public string Name { set; get; } = "";
    }
} // End Partial class
