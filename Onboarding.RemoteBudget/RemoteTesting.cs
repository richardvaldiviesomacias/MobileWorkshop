using System;
using System.Net;
using System.Threading.Tasks;
using AccessControl;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Onboarding.RemoteBudget
{
    public class RemoteTesting
    {
        #region Testing methods

        [Obsolete("for testing only")]
        public static async Task SignIn(IRestClient restClient, IAccessControlManager accessControlManager)
        {
            string jwt;

            restClient.Timeout = -1;
            var request = new RestRequest("user/user/sign-in", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("email", "rstestkh@gmail.com");
            request.AddParameter("password", "Password1!");
            IRestResponse response = await restClient.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Unexpected response from SignIn call: {response.StatusCode} - {response.Content}");
            }

            try
            {
                var jObject = JObject.Parse(response.Content);
                jwt = jObject["oauth2"]["jwt"].ToString();
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to parse response from SignIn: {response.Content}:", e);
            }

            // Store sign-in JWT
            var userAccess = new UserAccess(jwt, "", null);
            accessControlManager.SetUserAccess(userAccess);
        }

        [Obsolete("for testing only")]
        public static IAccessControlManager SignInForTesting()
        {
            var accessControlManager = new AccessControlManager(Application.EveryDollar, ApiEnvironment.Production);
            var task = Task.Run(async ()
                => await SignIn(new RestClient("https://api.everydollar.com/"), accessControlManager));
            task.Wait();
            return accessControlManager;
        }

        #endregion Testing methods
    }
}