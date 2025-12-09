using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Keycloak.Example.Dtos;

public class CreateRoleDto
{
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}