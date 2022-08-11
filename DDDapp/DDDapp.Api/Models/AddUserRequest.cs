namespace DDDapp.Api.Models;

public record AddUserRequest(
    string FirstName,
    string LastName,
    string Email
);

/* public class AddUserRequest{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailName { get; set; }
    
} */