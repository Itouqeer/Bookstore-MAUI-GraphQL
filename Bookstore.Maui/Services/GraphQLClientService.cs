using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace Bookstore.Maui.Services
{
    public class GraphQLClientService
    {
        private readonly IAlertService _alertService;
        private readonly GraphQLHttpClient _client;

        public GraphQLClientService(string endpoint, IAlertService alertService)
        {
            _alertService = alertService;
            _client = new GraphQLHttpClient(endpoint, new NewtonsoftJsonSerializer());
        }

        public async Task<T> SendRequestAsync<T>(string query, object variables = null)
        {
            try
            {
                var request = new GraphQLRequest
                {
                    Query = query,
                    Variables = variables
                };

                var response = await _client.SendQueryAsync<T>(request);

                if (response.Errors?.Any() == true)
                {
                    var errorMessages = response.Errors.Select(error => error.Message);
                    throw new Exception($"GraphQL errors: {string.Join(", ", errorMessages)}");
                }

                return response.Data;
            }
            catch (GraphQLHttpRequestException httpEx)
            {
                await _alertService.ShowAlert("Network Error", "An error occurred while contacting the server.");
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlert("Request Failed", "An error occurred while processing the request.");
            }

            return default;
        }
    }
}
