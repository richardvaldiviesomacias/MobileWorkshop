using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AccessControl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Onboarding.RemoteBudget
{
    public class RemoteBudgetCalls: IRemoteBudgetCalls
    {
        private readonly IAccessControlManager accessControlManager;
        private readonly IRestClient restClient;
        public RemoteBudgetCalls(IAccessControlManager accessControlManager, IRestClient restClient)
        {
            this.accessControlManager =
                accessControlManager ?? throw new ArgumentNullException(nameof(accessControlManager));
            this.restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }
        public Task<string> SignIn(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Budget> CreateNewBudget()
        {
            if (string.IsNullOrWhiteSpace(accessControlManager?.Jwt))
            {
                throw new ArgumentNullException();
            }

            restClient.Timeout = -1;
            var request = new RestRequest("budget/budgets", Method.PUT);
            request.AddHeader("Authorization", $"Bearer {accessControlManager.Jwt}");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("date", DateTime.Now.ToString("yyyy-MM-dd"));
            request.AddParameter("currency", "usd");
            IRestResponse response = await restClient.ExecuteAsync(request);
            
            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception($"Unexpected response from created budget call: {response.StatusCode} - {response.Content}");
            }

            try
            {
                var json = Cleaner.CleanJson(response.Content);
                var jsonObject = JObject.Parse(json);
                var budget = JsonConvert.DeserializeObject<Budget>(json);
                return budget;
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to parse response from Get Budgets: {response.Content}", e);
            }
        }

        public async Task<Budget> GetCurrentBudget()
        {
            var budgets = await GetAllBudgets();

            var budgetId = budgets?[0]?.Id;
            if (budgetId == null)
            {
                return null;
            }

            restClient.Timeout = -1;
            var request = new RestRequest($"budget/budgets/{budgetId}", Method.GET);
            request.AddHeader("Authorization", $"Bearer {accessControlManager.Jwt}");
            IRestResponse response = restClient.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Unexpected response from GetCurrentBudget call: {response.StatusCode} - {response.Content}");
            }

            try
            {
                var json = Cleaner.CleanJson(response.Content);
                var budget = JsonConvert.DeserializeObject<Budget>(json);
                return budget;
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to parse response from GetCurrentBudget call: {response.Content}", e);
            }
        }

        public async Task<List<Budget>> GetAllBudgets()
        {
            restClient.Timeout = -1;
            var request = new RestRequest("budget/budgets", Method.GET);
            request.AddHeader("Authorization", $"Bearer {accessControlManager.Jwt}");
            IRestResponse response = await restClient.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Unexpected response from budgets call: {response.StatusCode} - {response.Content}");
            }

            try
            {
                var json = Cleaner.CleanJson(response.Content);
                var jsonObject = JObject.Parse(json);
                var budgets = JsonConvert.DeserializeObject<List<Budget>>(jsonObject["budgets"].ToString());
                return budgets;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateBudgetItem(string budgetId, string itemId, decimal amount)
        {
            restClient.Timeout = -1;
            var request = new RestRequest($"budget/budgets/{budgetId}/items/{itemId}/amount", Method.PUT);
            request.AddHeader("Authorization", $"Bearer {accessControlManager.Jwt}");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("amount",(amount * 100).ToString("0"));
            IRestResponse response = await restClient.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Unexpected response from UpdateBudgetItemAmount call: {response.StatusCode} - {response.Content}");
            }
        }

        public async Task DeleteBudget(string budgetId)
        {
            restClient.Timeout = - 1;
            var request = new RestRequest($"budget/budgets/{budgetId}", Method.DELETE);
            request.AddHeader("Authorization", $"Bearer {accessControlManager.Jwt}");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = await restClient.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception($"Unexpected response from DeleteBudget call: {response.StatusCode} - {response.Content}");
            }
        }

        public async Task DeleteBudgetItem(string budgetId, string itemId)
        {
            restClient.Timeout = -1;
            var request = new RestRequest($"budget/budgets/{budgetId}/items/{itemId}", Method.DELETE);
            request.AddHeader("Authorization", $"Bearer {accessControlManager.Jwt}");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = await restClient.ExecuteAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(
                    $"Unexpected response from DelegateBudgetItem call: {response.StatusCode} - {response.Content}");
            }
        }

        public async Task CreateBudgetItem(string budgetId, string groupId, BudgetItemType type, string label, decimal amount)
        {
            restClient.Timeout = -1;
            var request = new RestRequest($"budget/budgets/{budgetId}/items", Method.POST);
            request.AddHeader("Authorization", $"Bearer {accessControlManager.Jwt}");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("type", type.ToString());
            request.AddParameter("label", label);
            request.AddParameter("amount", (int)(amount * 100));
            request.AddParameter("budget_group_id", $"urn:everydollar:budget:{budgetId}:group:{groupId}");
            IRestResponse response = await restClient.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception($"Unexpected response from DeleteBudgetItem call: {response.StatusCode} - {response.Content}");
            }
        }
        
        
        [Obsolete("for testing only")]
        internal async Task DeleteAllBudgets()
        {
            // Sign in
            if (string.IsNullOrWhiteSpace(accessControlManager.Jwt))
            {
                await RemoteTesting.SignIn(restClient, accessControlManager);
            }

            // Delete all budgets
            List<Budget> budgets = await GetAllBudgets();

            foreach (var budget in budgets)
            {
                await DeleteBudget(budget.Id);
            }
        }
    }
}