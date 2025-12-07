using Keycloak.Example.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keycloak.Example.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class UserController(KeycloakService keycloakService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken = default)
    {
        var results = await keycloakService.GetAllUsers(cancellationToken);
        return Ok(results);
    }
}