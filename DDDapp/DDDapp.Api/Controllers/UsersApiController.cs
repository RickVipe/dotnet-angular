using DDDapp.Api.Models;
using DDDapp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using DDDapp.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace DDDapp.Api.Controllers;

[ApiController]
[Route("api")]

public class UsersApiController : ControllerBase{

    private readonly IUserService userService;
    private readonly IValidator<User> _validator;

    public UsersApiController(IUserService userService, IValidator<User> validator)
    {
        this.userService = userService;
        _validator = validator;
    }

    [HttpGet]
    public IActionResult GetUsers(){
        return Ok(userService.GetAllUsers());
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddUserAsync(AddUserRequest request){

        var cu = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        ValidationResult result = await _validator.ValidateAsync(cu);

        if(!result.IsValid){
            return BadRequest(result);
        }

        var userAddResult = userService.Add(request.FirstName, request.LastName, request.Email);
        if(userAddResult == null){
            //return BadRequest(request);
            return Problem("Email already exists.", statusCode: 409);
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
    public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, UpdateUserRequest request){

        var cu = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        ValidationResult result = await _validator.ValidateAsync(cu);

        if(!result.IsValid){
            return BadRequest(result.Errors);
        }

        var userUpdateResult = userService.Update(id, request.FirstName, request.LastName, request.Email);

        if(userUpdateResult == null){
            return Problem("User not found.", statusCode: 404);
        }

        var response = MapUserRequestResponse(userUpdateResult);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteUser([FromRoute] Guid id){
        var userDeleteResult = userService.Delete(id);

        if(userDeleteResult == null){
            return Problem("User not found.", statusCode: 404);
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