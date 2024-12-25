using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.User.Add;
using Users.Application.User.Delete;
using Users.Application.User.GetAll;
using Users.Application.User.GetById;
using Users.Application.User.Login;
using Users.Domain.Entities;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllUsersQuery();
        var res = await _sender.Send(query);
        return Ok(res);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var query = new GetUserByIdQuery(id);
        var res = await _sender.Send(query);
        if (res is null) return NotFound();
        return Ok(res);
    }


    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] AddUserRequest userRequest)
    {
        var user = userRequest.Adapt<User>();
        var command = new AddUserCommand(user);
        var res = await _sender.Send(command);
        return Ok(res);
    }

    [Authorize]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var command = new DeleteUserCommand(id);
        var res = await _sender.Send(command);
        return Ok(res);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var command = new LoginCommand(loginRequest.Email, loginRequest.Password);
        var res = await _sender.Send(command);
        if (res is null) return NotFound();
        return Ok(res);
    }
}