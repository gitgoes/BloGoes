namespace BloGoes.API.Filters
{
    public class ApiKeyValidator : IApiKeyValidator
    {
        public bool IsValid(string apiKey)
        {
            // Retrieve the expected API key from the app settings
            var expectedApiKey = "BAD20F48-9586-44C5-9D5F-E130FC5E0D2C";

            // Compare the provided API key with the expected API key
            return apiKey == expectedApiKey;
        }
    }
}
