using Keycloak.Example.Dtos;
using Keycloak.Example.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keycloak.Example.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class RoleController(KeycloakService keycloakService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRoles(CancellationToken cancellationToken)
    {
        var result = await keycloakService.GetAllRoles(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{roleName}")]
    public async Task<IActionResult> GetRoleByName(string roleName, CancellationToken cancellationToken)
    {
        var result = await keycloakService.GetRoleByName(roleName, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto,CancellationToken cancellationToken)
    {
        await keycloakService.CreateRole(createRoleDto, cancellationToken);
        return Ok();
    }

    [HttpDelete("{roleName}")]
    public async Task<IActionResult> DeleteRole(string roleName, CancellationToken cancellationToken)
    {
        await keycloakService.DeleteRole(roleName, cancellationToken);
        return Ok();
    }
}