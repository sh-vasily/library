using DbDemo;
using Library.Persistance.Repository;

namespace Library.Persistence.Repository;

public interface IUserRepository : IRepository<User>;
public sealed class UserRepository : Repository<User>, IUserRepository;