using System.Text.Json.Serialization;

namespace Keycloak.Example.Dtos;

public class GetUserRolesDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("composite")]
    public bool Composite { get; set; }
    [JsonPropertyName("clientRole")]
    public bool ClientRole { get; set; }
    [JsonPropertyName("containerId")]
    public Guid ContainerId { get; set; }
}