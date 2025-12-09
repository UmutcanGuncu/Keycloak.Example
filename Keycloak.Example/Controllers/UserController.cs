using Keycloak.Example.Dtos;
using Keycloak.Example.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keycloak.Example.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[Authorize("yetkili")]
public class UserController(KeycloakService keycloakService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken = default)
    {
        var results = await keycloakService.GetAllUsers(cancellationToken);
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id, CancellationToken cancellationToken = default)
    {
        var result = await keycloakService.GetById(id, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetUserByUsername(string username, CancellationToken cancellationToken = default)
    {
        var result = await keycloakService.GetByUsername(username, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id,[FromBody]UserDto userDto, CancellationToken cancellationToken)
    {
        await keycloakService.UpdateUser(id, userDto, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id, CancellationToken cancellationToken)
    {
        await keycloakService.DeleteUser(id, cancellationToken);
        return Ok();
    }
}