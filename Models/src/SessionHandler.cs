namespace ASPNETMaker2023.Models;

// Partial class
public partial class mecommerce {
    /// <summary>
    /// Session handler
    /// </summary>
    public class SessionHandler
    {
        // Constructor
        public SessionHandler(Controller controller) => Controller = controller;

        // Get session value
        public IActionResult GetSession()
        {
            string content = RemoveXss(Get<string>(Config.TokenName));
            if (Empty(content) && HttpContext != null)
                content = Antiforgery.GetAndStoreTokens(HttpContext).RequestToken ?? "";
            return Controller.Content(content);
        }
    }
} // End Partial class