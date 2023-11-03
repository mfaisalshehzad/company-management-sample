using System.Text.Json.Serialization;

namespace CompanyManagement.API.ViewModels
{
    public class OpenIdConfigDto
    {
        [JsonPropertyName("jwks_uri")]
        public string JWKS { get; set; }
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }
    }

    public class OpenIdJWKSKey
    {
        [JsonPropertyName("kty")]
        public string KeyType { get; set; }
        [JsonPropertyName("use")]
        public string Use { get; set; }
        [JsonPropertyName("alg")]
        public string Algorithm { get; set; }
        [JsonPropertyName("e")]
        public string Exponent { get; set; }
        [JsonPropertyName("n")]
        public string Moduluos { get; set; }
    }

    public class OpenIdJWKSKeys
    {
        [JsonPropertyName("keys")]
        public OpenIdJWKSKey[] Keys { get; set; }
    }
}
