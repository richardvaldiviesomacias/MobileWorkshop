using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Onboarding.RemoteBudget
{
  public static class Cleaner
    {
        public static string CleanJson(string json)
        {
            var jObject = JObject.Parse(json);

            EliminateEmbeddedTokens(jObject);
            GenerateIds(jObject);
            FlattenCurrencies(jObject);
            CleanCollectionNames(jObject);

            var output = jObject.ToString();
            return output;
        }

        public static void Rename(this JToken token, string newName)
        {
            if (token == null)
                throw new ArgumentNullException("token", "Cannot rename a null token");

            JProperty property;

            if (token.Type == JTokenType.Property)
            {
                if (token.Parent == null)
                    throw new InvalidOperationException("Cannot rename a property with no parent");

                property = (JProperty)token;
            }
            else
            {
                if (token.Parent == null || token.Parent.Type != JTokenType.Property)
                    throw new InvalidOperationException("This token's parent is not a JProperty; cannot rename");

                property = (JProperty)token.Parent;
            }

            // Note: to avoid triggering a clone of the existing property's value,
            // we need to save a reference to it and then null out property.Value
            // before adding the value to the new JProperty.  
            // Thanks to @dbc for the suggestion.

            var existingValue = property.Value;
            property.Value = null;
            var newProperty = new JProperty(newName, existingValue);
            property.Replace(newProperty);
        }

        static void EliminateEmbeddedTokens(JToken token)
        {
            if (token == null)
            {
                return;
            }

            // Search children for _embedded
            List<JToken> children = token.Children().ToList();
            foreach (var childToken in children)
            {
                EliminateEmbeddedTokens(childToken);
            }

            var tokenProperty = token as JProperty;
            if (tokenProperty == null)
            {
                return;
            }

            if (tokenProperty.Name != "_embedded")
            {
                return;
            }

            // Bring up all child nodes to this level
            foreach (var childToken in children)
            {

                if (childToken is JProperty)
                {
                    var childProperty = childToken as JProperty;
                    if (childProperty != null)
                    {
                        token.AddAfterSelf(childProperty);
                    }
                }

                if (childToken is JObject)
                {
                    var jObject = childToken as JObject;
                    foreach (var childProperty in jObject.Properties().ToList())
                    {
                        token.AddAfterSelf(childProperty);
                    }
                }
            }

            // SLAUGHTER THE _embedded!!!!
            token.Remove();
        }

        static void GenerateIds(JToken token)
        {
            if (token == null)
            {
                return;
            }

            // Search children for ids
            List<JToken> children = token.Children().ToList();
            foreach (var childToken in children)
            {
                GenerateIds(childToken);
            }

            var jObject = token as JObject;
            if (jObject == null)
            {
                return;
            }

            if (jObject["id"] != null)
            {
                return;
            }

            if (jObject["_links"] != null && jObject["_links"]["self"] != null)
            {
                string id = jObject["_links"]["self"]["href"].ToString();

                // Grab the last part
                id = id.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Last();

                jObject.Add(new JProperty("id", id));
            }
        }

        static void FlattenCurrencies(JToken token)
        {
            if (token == null)
            {
                return;
            }

            // Search children for _embedded
            List<JToken> children = token.Children().ToList();
            foreach (var childToken in children)
            {
                FlattenCurrencies(childToken);
            }

            var tokenProperty = token as JProperty;
            if (tokenProperty == null)
            {
                return;
            }

            if (tokenProperty.Name != "usd")
            {
                return;
            }

            var parent = tokenProperty.Parent as JObject;
            var parentProperty = parent.Parent as JProperty;
            parentProperty.Value = tokenProperty.Value;


            //foreach (var childToken in children)
            //{
            //    var childObject = childToken as JObject;

            //    if (childObject == null)
            //    {
            //        continue;
            //    }

            //    double amount = childObject["usd"].Value<double>();
            //    tokenProperty.Value = amount;
            //    return;
            //}
        }

        static void CleanCollectionNames(JToken token)
        {
            if (token == null)
            {
                return;
            }

            // Search children for _embedded
            List<JToken> children = token.Children().ToList();
            foreach (var childToken in children)
            {
                CleanCollectionNames(childToken);
            }

            var tokenProperty = token as JProperty;
            if (tokenProperty == null)
            {
                return;
            }

            if (tokenProperty.Name == "budget")
            {
                tokenProperty.Rename("budgets");
                return;
            }

            if (tokenProperty.Name == "budget-group")
            {
                tokenProperty.Rename("budget_groups");
                return;
            }

            if (tokenProperty.Name == "budget-item")
            {
                tokenProperty.Rename("budget_items");
                return;
            }
        }
    }
}