using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Dell.Boomi.Client.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dell.Boomi.Client
{
    public class BoomiGenericClient<T>
    {
        public string AccountId { get; set; }

        public BoomiGenericClient(string accountId, string username, string password)
        {
            // URL Encode the accountId. There shouldn't be any special characters in it,
            // but if someone accidentally fat-fingers an accountId, we want a good exception to be
            // thrown instead of some weird error related to use of accountId on the Request URL.
            AccountId = Uri.EscapeDataString(accountId); 
            _authenticationHeaderValue = new AuthenticationHeaderValue("Basic", Base64Encode(string.Format("{0}:{1}", username, password)));
        }

        public async Task<T> Create(T newItem)
        {
            using (var httpClient = CreateHttpClient())
            {
                var stringContent = CreateJSONStringContent(newItem);
                var response = await httpClient.PostAsync("", stringContent);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(content);
                }

                throw new BoomiException(response.StatusCode, ParseErrorMessage(response.StatusCode, content));
            }   
        }

        public async Task<bool> Delete(string id)
        {
            using (var httpClient = CreateHttpClient())
            {
                var response = await httpClient.DeleteAsync(id);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return true;
                    
                    // Boomi currently returns a badly formatted response.  When they fix
                    // their API, we might do something like the following:
                    // return JsonConvert.DeserializeObject<bool>(content);
                }

                throw new BoomiException(response.StatusCode, ParseErrorMessage(response.StatusCode, content));
            }
        }

        public async Task<T> Get(string id)
        {
            using (var httpClient = CreateHttpClient())
            {
                var response = await httpClient.GetAsync(id);
                var content = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(content);
                }

                throw new BoomiException(response.StatusCode, ParseErrorMessage(response.StatusCode, content));
            }
        }

        public async Task<T> Update(string id, T newItem)
        {
            using (var httpClient = CreateHttpClient())
            {
                var stringContent = CreateJSONStringContent(newItem);
                var response = await httpClient.PostAsync(id +"/update", stringContent);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(content);
                }

                throw new BoomiException(response.StatusCode, ParseErrorMessage(response.StatusCode, content));
            }  
        }

        public async Task<IEnumerable<T>> Query(QueryFilter queryFilter)
        {
            using (var httpClient = CreateHttpClient())
            {
                var stringContent = CreateJSONStringContent(new { QueryFilter = queryFilter });
                var response = await httpClient.PostAsync("query", stringContent);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new BoomiException(response.StatusCode, ParseErrorMessage(response.StatusCode, content));
                }

                var allResultPages = new List<T>();
                var queryResult = JsonConvert.DeserializeObject<QueryResult<T>>(content);
                allResultPages.AddRange(queryResult.Results.AsEnumerable());

                // TODO: perhaps in the futuer we might want to do something smarter, but for
                // TODO: now, just read all of the query pages.
                while (queryResult.QueryToken != null)
                {
                    stringContent = new StringContent(queryResult.QueryToken);
                    response = await httpClient.PostAsync("queryMore", stringContent);
                    if (response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();
                        queryResult = JsonConvert.DeserializeObject<QueryResult<T>>(content);
                        allResultPages.AddRange(queryResult.Results.AsEnumerable());
                    }
                    else
                    {
                        throw new BoomiException(response.StatusCode, ParseErrorMessage(response.StatusCode, content));
                    }
                }

                return allResultPages;
            }    
        }

        public HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient {BaseAddress = new Uri(BaseUrl)};
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = _authenticationHeaderValue;
            return httpClient;
        }

        protected string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        protected string BaseUrl
        {
            get { return "https://api.boomi.com/partner/api/rest/v1/" + AccountId + "/" + typeof (T).Name + "/"; }
        }

        protected string ParseErrorMessage(HttpStatusCode statusCode, string content)
        {
            if (statusCode == HttpStatusCode.BadRequest)
            {
                var error = JsonConvert.DeserializeObject<Error>(content);
                return error.Message;
            }

            if (statusCode == HttpStatusCode.Forbidden)
            {
                return "Invalid username/password credentials";
            }

            return content;
        }

        private StringContent CreateJSONStringContent(Object obj)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            settings.Converters.Add(new StringEnumConverter { CamelCaseText = false });
            settings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeStyles = DateTimeStyles.AdjustToUniversal,
                DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ssK"
            });

            var stringContent = new StringContent(JsonConvert.SerializeObject(obj, settings), Encoding.UTF8, "application/json");
            stringContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            return stringContent;
        }

        private readonly AuthenticationHeaderValue _authenticationHeaderValue;
    }
}
