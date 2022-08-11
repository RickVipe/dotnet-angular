namespace DDDapp.Api.Models;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email
);