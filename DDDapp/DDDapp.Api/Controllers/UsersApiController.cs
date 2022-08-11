using DDDapp.Api.Models;
using DDDapp.Application.Services;
using Microsoft.AspNetCore.Mvc;
namespace DDDapp.Api.Controllers;

[ApiController]
[Route("api")]

public class UsersApiController : ControllerBase{

    private readonly IUserService userService;

    public UsersApiController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public IActionResult GetUsers(){
        return Ok(userService.GetAllUsers());
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddUser(AddUserRequest request){
        var userAddResult = userService.Add(request.FirstName, request.LastName, request.Email);
        if(userAddResult == null){
            return Conflict(request);
        }
        var response = new UserRequestResponse(
            userAddResult.FirstName,
            userAddResult.LastName,
            userAddResult.Email
        );

        return Ok(response);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateUser([FromRoute] Guid id, UpdateUserRequest request){
        var userUpdateResult = userService.Update(id, request.FirstName, request.LastName, request.Email);

        if(userUpdateResult == null){
            return NotFound();
        }

        var response = MapUserRequestResponse(userUpdateResult);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteUser([FromRoute] Guid id){
        var userDeleteResult = userService.Delete(id);

        if(userDeleteResult == null){
            return NotFound();
        }

        var response = MapUserRequestResponse(userDeleteResult);

        return Ok(response);
    }
    
    private static UserRequestResponse MapUserRequestResponse(UserResponse user){
        return new UserRequestResponse(
            user.FirstName,
            user.LastName,
            user.Email
        );
    }

}