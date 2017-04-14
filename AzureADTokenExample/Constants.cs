namespace AzureADTokenExample
{
    internal class AppModeConstants
    {
        public const string ClientId = "24e4db68-d62d-485d-8b75-7ae3393c88f8";
        public const string ClientSecret = "G8wzkGyYdzoGX9Or+2nxC8uI6YGuLuDgCOUqPLTvSnI=";
        public const string TenantName = "GRIDLake.onMicrosoft.com";
        public const string TenantId = "054c0c91-151b-49c8-9222-25ef37c29fe3";
        public const string AuthString = GlobalConstants.AuthString + TenantName;
    }

    internal class UserModeConstants
    {
        public const string TenantId = AppModeConstants.TenantId;
        public const string ClientId = "15786249-6315-4465-ac9c-f9919317ece8";
        public const string AuthString = GlobalConstants.AuthString + "common/";
        public const string DBConnection = "Server=tcp:gridlakeserver.database.windows.net,1433;Initial Catalog=GRIDLakeDB;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;";
        public const string ManualUserId = "joseph.errett@gridlake.us";
        public const string ManualUserPwd = "Taylor1214!";
    }

    internal class GlobalConstants
    {
        public const string AuthString = "https://login.microsoftonline.com/";        
        public const string ResourceUrl = "https://graph.windows.net";
        public const string SqlResourceUrl = "https://database.windows.net/";
        public const string GraphServiceObjectId = "00000002-0000-0000-c000-000000000000";
    }
}
