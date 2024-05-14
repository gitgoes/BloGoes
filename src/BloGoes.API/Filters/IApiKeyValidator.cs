namespace BloGoes.API.Filters
{
    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}
