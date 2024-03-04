using Newtonsoft.Json;
using Rainfall.Contracts.DTO;
using Rainfall.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.ReferenceAPI
{
    public class RainfallApiLib : IRainfallApiLib
    {

        private readonly IHttpClientFactory apiClientFactory;
        private HttpResponseMessage? _httpResponseMessage;
        string _root = "http://environment.data.gov.uk/flood-monitoring";

        public RainfallApiLib(IHttpClientFactory srcFactory)
        {
            apiClientFactory = srcFactory;
        }

        private HttpClient ApiClient(string server = "rainfallapi")
        {
            // problem with readiing the base uri, it does not read full address
            return apiClientFactory.CreateClient(server);
        }
        private HttpClient ApiDefaultClient()
        {
            return apiClientFactory.CreateClient();
        }

        private ResultResponse CreateResponse(string statusText, bool error = false, int responseState = 200, object data = null)
        {
            return new ResultResponse()
            {
                Error = error,
                ResponseState = responseState,
                StatusText = statusText,
                Data = data
            };
        }

        private ResultResponse CatchResponseMessage(HttpResponseMessage httpResponse)
        {
            if (httpResponse is null)
                throw new ArgumentNullException(nameof(httpResponse));

            int _statusCode = (int)httpResponse.StatusCode;
            var response = CreateResponse(httpResponse.ReasonPhrase ?? "Unexpected error occured.", true, _statusCode);

            if (_statusCode == 200)
            {
                response.Data = httpResponse.Content;
                response.Error = false;
                return response;
            }

            if (_statusCode > 200)
            {
                response.Data = httpResponse.Content.ReadAsStringAsync().Result;
                response.Error = true;
                return response;
            }

            return response;

        }

        #region asset
        public async Task<ResultResponse> GetStationsReading(int id)
        {
            var response = await GetRequest($"/flood-monitoring/id/stations/{id}/readings");
            
            if (!response.Error)
            {
                var res = GetContentDataFromResultResponse<EnvirontmentDTO>(response);
            }

            return response;
        }
        #endregion

        protected T GetContentData<T>(HttpContent response)
        {
            var result = JsonConvert.DeserializeObject<T>(response.ReadAsStringAsync().Result);

            return result ?? throw new ArgumentNullException(nameof(result));
        }

        protected T GetContentDataFromResultResponse<T>(ResultResponse response)
        {
            var content = (HttpContent)response.Data;
            string cleanJSON = content.ReadAsStringAsync().Result.Replace("@", string.Empty);

            var result = JsonConvert.DeserializeObject<T>(cleanJSON);

            return result ?? throw new ArgumentNullException(nameof(result));
        }

        #region http request

        protected async Task<ResultResponse> GetRequest(string requestURI)
        {
            try
            {
                _httpResponseMessage = await ApiClient().GetAsync(requestURI);
                var response = CatchResponseMessage(_httpResponseMessage);

                return response;
            }
            catch (TimeoutException)
            {
                throw new Exception("Network has timed out. Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
