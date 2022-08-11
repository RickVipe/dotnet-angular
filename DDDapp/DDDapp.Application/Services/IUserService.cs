using DDDapp.Domain.Entities;

namespace DDDapp.Application.Services;

public interface IUserService{
    UserResponse Add(string FirstName, string LastName, string Email);

    UserResponse Update(Guid id, string FirstName, string LastName, string Email);

    UserResponse Delete(Guid id);

    List<User> GetAllUsers();

}