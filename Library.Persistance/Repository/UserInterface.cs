using Library.Persistance.Models;

namespace Library.Persistance.Repository;

public interface IUserRepository : IRepository<User>;
public sealed class UserRepository(LibraryContext libraryContext) 
    : Repository<User>(libraryContext), IUserRepository;