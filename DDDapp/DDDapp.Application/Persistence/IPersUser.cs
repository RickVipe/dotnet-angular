using DDDapp.Domain.Entities;
namespace DDDapp.Application.Persistence;

public interface IPersUser{
    User?GetUserById(Guid id);
    User?GetUserByEmail(string email);
    void Add(User user);
    void Update(Guid id, string FirstName, string LastName, string Email);
    void Delete(User user);
    List<User>GetAllUsers();
}