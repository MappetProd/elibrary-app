using EL.Domain;
using EL.Repository.Contracts;
using EL.Repository.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Repository.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ElibraryContext context) : base(context) { }

        public User? GetUserByAuthData(string login, string password)
        {
            User? user = GetAll().SingleOrDefault(
                u => u.Login == login && 
                PasswordHasher.VerifyHashedPassword(u.Password, password)
            );

            return user;
        }

        public User? Get(string id)
        {
            Guid guid = Guid.Parse(id);
            User? user = entities.SingleOrDefault(u => u.Id.Equals(guid));
            return user;
        }
    }
}
