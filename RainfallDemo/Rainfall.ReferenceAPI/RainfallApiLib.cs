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
        
        //initialize the httpclients
        //i put all api request services on this layer

        public RainfallApiLib(IHttpClientFactory srcFactory)
        {
            apiClientFactory = srcFactory;
        }

        private HttpClient ApiClient(string server = "rainfallapi")
        {
            //creates httpclient based on the named httpclient on DI
            return apiClientFactory.CreateClient(server);
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
            //get and set the http response of the requested api, transfering the data to the dato transfer objects and defining its success or failed status.

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
                response.Data = GetContentDataFromResultResponse<EnvirontmentDTO>(response);
            }

            return response;
        }
        #endregion

        protected T GetContentDataFromResultResponse<T>(ResultResponse response)
        {
            //generic converter of data for convienience
            //catches the data content transfered then removing the @ character as it is not a compatible naming for a class properties, if not it may not be read during conversion.

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
                //http get call request to the api
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
