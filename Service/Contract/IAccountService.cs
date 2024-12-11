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
        bool IsUserPasswordCorrect(string userId, string password);
        bool ChangedPassword(string userId, string newPassword);
    }
}
