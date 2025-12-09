using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Keycloak.Example.Dtos;

public class RoleDto
{
    [Required]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}