using DDDapp.Application.Persistence;
using DDDapp.Domain.Entities;

namespace DDDapp.Infrastructure.Persistence;

public class PersUser : IPersUser
{
    private readonly UsersAPIDbContext dbContext;

    public PersUser(UsersAPIDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Add(User user)
    {
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
    }

    public void Update(Guid id, string FirstName, string LastName, string Email){
        var user = dbContext.Users.Find(id);
        user!.FirstName = FirstName;
        user!.LastName = LastName;
        user!.Email = Email;

        dbContext.SaveChanges();
    }

    public void Delete(User user){
        dbContext.Remove(user);

        dbContext.SaveChanges();
    }

    public List<User> GetAllUsers()
    {
        return dbContext.Users.ToList();
    }

    public User? GetUserById(Guid id)
    {
        return dbContext.Users.Find(id);
    }

    public User? GetUserByEmail(string email)
    {
        return dbContext.Users.Where(u => u.Email == email).SingleOrDefault();
    }
}