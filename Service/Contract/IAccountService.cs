using EL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using EL.Service.InputModel;
using EL.Service.DTO;

namespace EL.Service.Contract
{
    public interface IAccountService
    {
        ClaimsPrincipal? TryLogin(string login, string password);
        void AddUser(UserInputModel userViewModel);

        UserDTO ClaimsToDTO(IEnumerable<Claim> claims);
    }
}
