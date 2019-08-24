using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace VerySimpleRestClient
{
    public static partial class SimpleClient
    {
        public static async Task<TResult> PostAsync<TResult>(string url, Query query = null, Body body = null, HttpClient client = null)
            where TResult : class =>
            (await ClientInternal.RestActionAsync<TResult>(async c =>
            {
                if (body == null) return await c.PostAsync(ClientInternal.BuildUrl(url, query), null);
                using (var content = body.GetContent())
                    return await c.PostAsync(ClientInternal.BuildUrl(url, query), content);
            }, client)).Item1;

        public static async Task<TResult> PostHttpContentAsync<TResult>(string url, Query query = null, HttpContent content = null, HttpClient client = null)
            where TResult : class =>
            (await ClientInternal.RestActionAsync<TResult>(async c => await c.PostAsync(ClientInternal.BuildUrl(url, query), content), client)).Item1;

        public static async Task<JObject> PostAsync(string url, Query query = null, Body body = null, HttpClient client = null) 
            => await PostAsync<JObject>(url, query, body, client);

        public static async Task<JObject> PostHttpContentAsync(string url, Query query = null, HttpContent content = null, HttpClient client = null)
            => await PostHttpContentAsync<JObject>(url, query, content, client);
    }

    public static partial class Client
    {
        public static async Task<(TResult, SimpleResponse)> PostAsync<TResult>(string url, Query query = null, Body body = null, HttpClient client = null)
            where TResult : class =>
            await ClientInternal.RestActionAsync<TResult>(async c =>
            {
                if (body == null) return await c.PostAsync(ClientInternal.BuildUrl(url, query), null);
                using (var content = body.GetContent())
                    return await c.PostAsync(ClientInternal.BuildUrl(url, query), content);
            }, client);

        public static async Task<(TResult, SimpleResponse)> PostHttpContentAsync<TResult>(string url, Query query = null, HttpContent content = null, HttpClient client = null)
            where TResult : class =>
            await ClientInternal.RestActionAsync<TResult>(async c => await c.PostAsync(ClientInternal.BuildUrl(url, query), content), client);

        public static async Task<(JObject, SimpleResponse)> PostAsync(string url, Query query = null, Body body = null, HttpClient client = null)
            => await PostAsync<JObject>(url, query, body, client);

        public static async Task<(JObject, SimpleResponse)> PostHttpContentAsync(string url, Query query = null, HttpContent content = null, HttpClient client = null)
            => await PostHttpContentAsync<JObject>(url, query, content, client);
    }
}
