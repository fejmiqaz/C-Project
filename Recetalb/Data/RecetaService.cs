namespace Recetalb.Data

{
    public class RecetaService
    {
        private readonly HttpClient _httpClient;
        private string? _apiBaseUrl;
        private readonly string _recetaService = "";

        public RecetaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }


        public async Task<T> GetAsync<T>(string endpoint, T response)
        {
            var respone = await _httpClient.GetFromJsonAsync<T>(_recetaService + endpoint);
            return response;
        }
    }
}
