using Keycloak.Example.Dtos;
using Keycloak.Example.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keycloak.Example.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class UserRolesController(KeycloakService keycloakService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AssignRolesToUser(Guid userId, IEnumerable<RoleDto> roleDto,
        CancellationToken cancellationToken)
    {
        await keycloakService.AssignRolesToUser(userId, roleDto, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveRolesFromUser(Guid userId, IEnumerable<RoleDto> roleDto,
        CancellationToken cancellationToken)
    {
        await keycloakService.RemoveRolesFromUser(userId, roleDto, cancellationToken);
        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserRoles(Guid userId, CancellationToken cancellationToken)
    {
        var results = await keycloakService.GetUserRoles(userId, cancellationToken);
        return Ok(results);
    }
}