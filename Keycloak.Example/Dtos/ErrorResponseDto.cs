using System.Text.Json.Serialization;

namespace Keycloak.Example.Dtos;

public sealed class ErrorResponseDto
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = string.Empty;
    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; set; } = string.Empty;
}
public sealed class BadRequestErrorResponseDto
{
    [JsonPropertyName("field")]
    public string Field { get; set; } = string.Empty;
    [JsonPropertyName("errorMeessage")]
    public string ErrorMessage { get; set; } = string.Empty;
}