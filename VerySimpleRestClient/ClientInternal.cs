using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace VerySimpleRestClient
{
    internal static class ClientInternal
    {
        internal static async Task<(TResult, SimpleResponse)> RestActionAsync<TResult>(
            Func<HttpClient, Task<HttpResponseMessage>> func, HttpClient client = null)
            where TResult : class
        {
            async Task<(TResult, SimpleResponse)> ExecuteAction(HttpClient c)
            {
                using (var response = await func(c))
                {
                    TResult result;
                    var responseJson = await response.Content.ReadAsStringAsync();
                    if (typeof(TResult) == typeof(string))
                    {
                        result = responseJson as TResult;
                    }
                    else if (typeof(TResult) == typeof(object))
                    {
                        result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    }
                    else
                    {
                        result = JsonConvert.DeserializeObject<TResult>(responseJson);
                    }
                    return (
                        result,
                        new SimpleResponse
                        {
                            ContentType = response.Content.Headers?.ContentType?.ToString(),
                            Headers = response.Headers,
                            IsSuccessStatusCode = response.IsSuccessStatusCode,
                            ReasonPhrase = response.ReasonPhrase,
                            StatusCode = response.StatusCode
                        }
                    );
                }
            }
            if (client != null)
            {
                return await ExecuteAction(client);
            }
            using (var c = new HttpClient())
            {
                return await ExecuteAction(c);
            }
        }

        internal static string BuildUrl(string url, object query)
        {
            if (query == null)
            {
                return url;
            }
            var values = query
                .GetType()
                .GetProperties()
                .ToDictionary(
                    key => key.Name, 
                    value => Convert.ToString(value.GetValue(query, null))
                    );

            return QueryHelpers.AddQueryString(url, values);
        }
    }
}
