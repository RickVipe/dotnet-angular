using DDDapp.Domain.Entities;
using DDDapp.Application.Persistence;

namespace DDDapp.Application.Services;

public class UserService : IUserService{

    private readonly IPersUser persistenceUser;

    public UserService(IPersUser persistenceUser)
    {
        this.persistenceUser = persistenceUser;
    }

    public UserResponse? Add(string FirstName, string LastName, string Email){

        // Validate User doesn't exist
        if(persistenceUser.GetUserByEmail(Email) is not null){
            return null;
        }

        //Create new User
        var user = new User{
            FirstName = FirstName,
            LastName = LastName,
            Email = Email
        };

        // Insert new User into DB
        persistenceUser.Add(user);

        return new UserResponse(FirstName, LastName, "Inside Aplication UserService");

    }

    public UserResponse? Update(Guid id, string FirstName, string LastName, string Email){

        // Validate User doesn't exist
        if(persistenceUser.GetUserById(id) is null){
            return null;
        }

        //Update User
        persistenceUser.Update(id, FirstName, LastName, Email);

        return new UserResponse(FirstName, LastName, Email);

    }

    public UserResponse? Delete(Guid id){
        var user = persistenceUser.GetUserById(id);
        // Validate User doesn't exist
        if(user is null){
            return null;
        }

        //Update User
        persistenceUser.Delete(user);

        return new UserResponse(user.FirstName, user.LastName, user.Email);

    }

    public List<User> GetAllUsers()
    {
        return persistenceUser.GetAllUsers();
    }
}