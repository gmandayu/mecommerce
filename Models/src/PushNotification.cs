namespace ASPNETMaker2023.Models;

// Partial class
public partial class mecommerce {
    /// <summary>
    /// Subscription schema
    /// </summary>
    public class SubscriptionSchema
    {
        public string Endpoint { get; set; } = "";

        public string PublicKey { get; set; } = "";

        public string AuthToken { get; set; } = "";
    }

    /// <summary>
    /// Push Notification class
    /// </summary>
    public class PushNotification
    {
        // Subscribe
        public async Task<bool> Subscribe()
        {
            string user = CurrentUserID() ?? CurrentUserName();
            string endpoint = Post("endpoint");
            string publicKey = Post("publicKey");
            string authToken = Post("authToken");
            string contentEncoding = Post("contentEncoding");
            if (
                EmptyString(Config.SubscriptionTableVar) ||
                EmptyString(Config.SubscriptionFieldNameUser) ||
                EmptyString(Config.SubscriptionFieldNameEndpoint) ||
                EmptyString(Config.SubscriptionFieldNamePublicKey) ||
                EmptyString(Config.SubscriptionFieldNameAuthToken) ||
                EmptyString(endpoint) ||
                EmptyString(publicKey) ||
                EmptyString(authToken) ||
                EmptyString(contentEncoding)
            ) {
                return false;
            }
            Dictionary<string, object> rsnew = new () {
                { Config.SubscriptionFieldNameUser, user },
                { Config.SubscriptionFieldNameEndpoint, endpoint },
                { Config.SubscriptionFieldNamePublicKey, publicKey },
                { Config.SubscriptionFieldNameAuthToken, authToken },
                { Config.SubscriptionFieldNameContentEncoding, contentEncoding }
            };

            // Insert subscription
            var tbl = Resolve(Config.SubscriptionTableVar);
            bool addSubscription = false;
            if (tbl != null) {
                addSubscription = (bool)tbl.Invoke("RowInserting", new object?[] { null, rsnew });
                if (addSubscription) {
                    addSubscription = await tbl.InsertAsync(rsnew) > 0;
                    if (addSubscription)
                        tbl.Invoke("RowInserted", new object?[] { null, rsnew });
                }
            }
            return addSubscription;
        }

        // Send
        public async Task<List<dynamic>> Send()
        {
            var payload = Form.ToDictionary(kvp => kvp.Key, kvp => kvp.Value); // Get all post data
            if (!Empty(Config.TokenNameKey) && payload.ContainsKey(Config.TokenNameKey)) // Remove token mame
                payload.Remove(Config.TokenNameKey);
            if (!Empty(Config.TokenValueKey) && payload.ContainsKey(Config.TokenValueKey)) // Remove token key
                payload.Remove(Config.TokenValueKey);
            var tbl = Resolve(Config.SubscriptionTableVar)!;
            IEnumerable<SubscriptionSchema>? rows = null;
            if (tbl != null) {
                if (payload.TryGetValue("key_m[]", out StringValues keys) && keys.Count > 0) {
                    rows = await ((IQueryFactory)tbl).GetQueryBuilder().Select(Config.SubscriptionFieldNameEndpoint + " AS Endpoint", Config.SubscriptionFieldNamePublicKey + " AS PublicKey", Config.SubscriptionFieldNameAuthToken + " AS AuthToken").WhereIn(Config.SubscriptionFieldNameId, keys.ToArray()).GetAsync<SubscriptionSchema>();
                    payload.Remove("key_m[]");
                } else {
                    rows = await ((IQueryFactory)tbl).GetQueryBuilder().Select(Config.SubscriptionFieldNameEndpoint + " AS Endpoint", Config.SubscriptionFieldNamePublicKey + " AS PublicKey", Config.SubscriptionFieldNameAuthToken + " AS AuthToken").GetAsync<SubscriptionSchema>();
                }
            }
            if (rows == null || rows.Count() == 0)
                return new () { new { version = Config.ProductVersion, success = false } };
            List<WebPush.PushSubscription> subscriptions = rows.Select(row => new WebPush.PushSubscription(
                ConvertToString(row.Endpoint),
                ConvertToString(row.PublicKey),
                ConvertToString(row.AuthToken)
            )).ToList();
            return await SendNotifications(subscriptions, payload);
        }

        #pragma warning disable 1998
        // Send notifications
        protected async Task<List<dynamic>> SendNotifications(List<WebPush.PushSubscription> subscriptions, IDictionary<string, StringValues> payload)
        {
            WebPushClient webPushClient = new ();
            // Vapid subject must be valid URL
            VapidDetails vapidDetails = new (
                BaseUrl(),
                Config.PushServerPublicKey,
                Config.PushServerPrivateKey
            );
            if (!payload.ContainsKey("title")) // Add title if not found // DN
                payload.Add("title", Language.Phrase("PushNotificationDefaultTitle"));
            List<dynamic> results = new ();
            IEnumerable<dynamic> reports = subscriptions.Select(subscription => {
                try {
                    webPushClient.SendNotification(subscription, ConvertToJson(payload), vapidDetails);
                    dynamic result = new { success = true };
                    results.Add(result);
                    return result;
                } catch (WebPushException exception) {
                    dynamic result = new { success = false, message = exception.Message };
                    results.Add(new { success = result.success });
                    return result;
                }
            });
            if (Config.Debug)
                Log(ConvertToJson(reports));
            return results; // Return "success" only
        }
        #pragma warning restore 1998

        // Delete
        public async Task<object> Delete()
        {
            string user = CurrentUserID() ?? CurrentUserName();
            string endpoint = Post("endpoint");
            string publicKey = Post("publicKey");
            string authToken = Post("authToken");
            string contentEncoding = Post("contentEncoding");
            if (
                EmptyString(Config.SubscriptionTableVar) ||
                EmptyString(Config.SubscriptionFieldNameUser) ||
                EmptyString(Config.SubscriptionFieldNameEndpoint) ||
                EmptyString(Config.SubscriptionFieldNamePublicKey) ||
                EmptyString(Config.SubscriptionFieldNameAuthToken) ||
                EmptyString(endpoint) ||
                EmptyString(publicKey) ||
                EmptyString(authToken) ||
                EmptyString(contentEncoding)
            ) {
                return new { success = false, error = "Invalid subscription table settings" };
            }
            if (
                EmptyString(endpoint) ||
                EmptyString(publicKey) ||
                EmptyString(authToken) ||
                EmptyString(contentEncoding)
            ) {
                return new { success = false, error = "Invalid subscription" };
            }
            Dictionary<string, object> rsold = new () {
                { Config.SubscriptionFieldNameUser, user },
                { Config.SubscriptionFieldNameEndpoint, endpoint },
                { Config.SubscriptionFieldNamePublicKey, publicKey },
                { Config.SubscriptionFieldNameAuthToken, authToken }
            };

            // Delete subscription
            var tbl = Resolve(Config.SubscriptionTableVar)!;
            bool deleteSubscription = false;
            if (tbl != null) {
                Dictionary<string, object> filter = new () { { Config.SubscriptionFieldNameEndpoint, endpoint } };
                if (!(await ((IQueryFactory)tbl).GetQueryBuilder().Where(filter).ExistsAsync())) // Subscription not exist
                    return new { success = true };
                deleteSubscription = (bool)tbl.Invoke("RowDeleting", new object?[] { rsold });
                if (deleteSubscription) {
                    deleteSubscription = await tbl.DeleteAsync(rsold, filter) > 0;
                    if (deleteSubscription)
                        tbl.Invoke("RowDeleted", new object?[] { rsold });
                }
            }
            return new { success = deleteSubscription };
        }
    }
} // End Partial class
