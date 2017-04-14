using System;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Security;

namespace AzureADTokenExample
{
    internal class AuthenticationHelper
    {
        public static string TokenForUser_GraphAPI;
        public static string TokenForUser_SQL;
        public static string TokenForApplication;
        public static bool SQLTestMode = false;
        public static bool InteractiveCredsMode = false;

        /// <summary>
        /// Get Active Directory Client for Application.
        /// </summary>
        /// <returns>ActiveDirectoryClient for Application.</returns>
        public static ActiveDirectoryClient GetActiveDirectoryClientAsApplication()
        {
            Uri servicePointUri = new Uri(GlobalConstants.ResourceUrl);
            Uri serviceRoot = new Uri(servicePointUri, AppModeConstants.TenantId);
            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                async () => await AcquireTokenAsyncForApplication());
            return activeDirectoryClient;
        }

        /// <summary>
        /// Async task to acquire token for Application.
        /// </summary>
        /// <returns>Async Token for application.</returns>
        public static async Task<string> AcquireTokenAsyncForApplication()
        {
            return await GetTokenForApplication();
        }

        /// <summary>
        /// Get Token for Application.
        /// </summary>
        /// <returns>Token for application.</returns>
        public static async Task<string> GetTokenForApplication()
        {
            if (TokenForApplication == null)
            {
                AuthenticationContext authenticationContext = new AuthenticationContext(AppModeConstants.AuthString, false);
                // Config for OAuth client credentials 
                ClientCredential clientCred = new ClientCredential(AppModeConstants.ClientId,
                    AppModeConstants.ClientSecret);
                AuthenticationResult authenticationResult =
                    await authenticationContext.AcquireTokenAsync(GlobalConstants.ResourceUrl,
                        clientCred);
                TokenForApplication = authenticationResult.AccessToken;
            }
            return TokenForApplication;
        }

        /// <summary>
        /// Get Active Directory Client for User.
        /// </summary>
        /// <returns>ActiveDirectoryClient for User.</returns>
        public static ActiveDirectoryClient GetActiveDirectoryClientAsUser()
        {
            Uri servicePointUri = new Uri(GlobalConstants.ResourceUrl);
            Uri serviceRoot = new Uri(servicePointUri, UserModeConstants.TenantId);
            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                async () => await AcquireTokenAsyncForUser());
            return activeDirectoryClient;
        }

        /// <summary>
        /// Async task to acquire token for User.
        /// </summary>
        /// <returns>Token for user.</returns>
        public static async Task<string> AcquireTokenAsyncForUser()
        {
            return await GetTokenForUser();
        }

        /// <summary>
        /// Get Token for User.
        /// </summary>
        /// <returns>Token for user.</returns>
        public static async Task<string> GetTokenForUser()
        {
            if (TokenForUser_GraphAPI == null)
            {
                var redirectUri = new Uri("http://localhost");
                AuthenticationContext authenticationContext = new AuthenticationContext(UserModeConstants.AuthString, false);
                AuthenticationResult sqlAuthnResult;
                AuthenticationResult graphAuthnResult;

                if (InteractiveCredsMode)
                {
                    sqlAuthnResult = await authenticationContext.AcquireTokenAsync(GlobalConstants.SqlResourceUrl,
                            UserModeConstants.ClientId, redirectUri, new PlatformParameters(PromptBehavior.RefreshSession));
                    graphAuthnResult = await authenticationContext.AcquireTokenSilentAsync(GlobalConstants.ResourceUrl, UserModeConstants.ClientId);
                }
                else
                {
                    sqlAuthnResult = await authenticationContext.AcquireTokenAsync(GlobalConstants.SqlResourceUrl,
                            UserModeConstants.ClientId, GetUserCredential());
                    graphAuthnResult = await authenticationContext.AcquireTokenSilentAsync(GlobalConstants.ResourceUrl, UserModeConstants.ClientId);
                }

                TokenForUser_GraphAPI = graphAuthnResult.AccessToken;
                TokenForUser_SQL = sqlAuthnResult.AccessToken;
                if (SQLTestMode) { DBHelper.SQLDBTest(TokenForUser_SQL); }
                Console.WriteLine("\n Welcome " + graphAuthnResult.UserInfo.GivenName + " " + graphAuthnResult.UserInfo.FamilyName);
            }
            return TokenForUser_GraphAPI;
        }

        private static UserCredential GetUserCredential()
        {
            string pwd = UserModeConstants.ManualUserPwd;
            string userId = UserModeConstants.ManualUserId;

            SecureString securePassword = new SecureString();

            foreach (char c in pwd) { securePassword.AppendChar(c); }
            securePassword.MakeReadOnly();

            var userCredential = new UserPasswordCredential(userId, securePassword);

            return userCredential;
        }
    }
}
