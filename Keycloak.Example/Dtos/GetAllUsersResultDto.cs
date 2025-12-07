using System.Text.Json.Serialization;

namespace Keycloak.Example.Dtos;

public class GetAllUsersResultDto
{
    [JsonPropertyName("id")]
    public Guid UserId { get; set; }
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("createdTimestamp")]
    public long CreatedTimestamp { get; set; }
}