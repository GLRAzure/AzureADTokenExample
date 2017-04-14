namespace AzureADTokenExample
{
    internal class AppModeConstants
    {
        public const string ClientId = "";
        public const string ClientSecret = "";
        public const string TenantName = "";
        public const string TenantId = "";
        public const string AuthString = GlobalConstants.AuthString + TenantName;
    }

    internal class UserModeConstants
    {
        public const string TenantId = AppModeConstants.TenantId;
        public const string ClientId = "";
        public const string AuthString = GlobalConstants.AuthString + "common/";
        public const string DBConnection = "";
        public const string ManualUserId = "";
        public const string ManualUserPwd = "";
    }

    internal class GlobalConstants
    {
        public const string AuthString = "https://login.microsoftonline.com/";        
        public const string ResourceUrl = "https://graph.windows.net";
        public const string SqlResourceUrl = "https://database.windows.net/";
        public const string GraphServiceObjectId = "00000002-0000-0000-c000-000000000000";
    }
}
