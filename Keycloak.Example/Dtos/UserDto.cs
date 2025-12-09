using System.Text.Json.Serialization;

namespace Keycloak.Example.Dtos;

public class UserDto
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
    [JsonPropertyName("emailVerified")]
    public bool EmailVerified { get; set; }
}