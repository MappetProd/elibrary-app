using EL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Repository.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? GetUserByAuthData(string login, string password);
        User? Get(string id);
        IEnumerable<User> GetAllByRole(string role);
    }
}
