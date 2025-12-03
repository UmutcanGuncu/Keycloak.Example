using System.Text.Json.Serialization;

namespace Keycloak.Example.Dtos;

public sealed class GetAccessTokenResponseDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;
}