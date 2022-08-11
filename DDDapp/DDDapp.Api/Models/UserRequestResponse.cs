namespace DDDapp.Api.Models;

public record UserRequestResponse(
    string FirstName,
    string LastName,
    string Email
);