using System.Security.Claims;
using EL.Domain;
using EL.Repository.Contracts;
using EL.Service.DTO;
using EL.Service.Contract;
using EL.Service.InputModel;
using EL.Service.Security;
using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EL.Service.Implementation
{
    public class AccountService : IAccountService
    {
        private IUserRepository _userRepository;
        private IRepository<Person> _personRepository;
        private readonly Dictionary<string, string> _roleTranslation = new Dictionary<string, string>
            {
                {"owner", "Владелец"},
                {"librarian", "Библиотекарь"},
                {"reader", "Читатель"},
            };

        public AccountService(IUserRepository userRepository,
            IRepository<Person> personRepository)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
        }

        public void AddUser(UserInputModel userInputData)
        {
            string userPasswordHash = PasswordHasher.Hash(userInputData.Password);

            User user = new User
            {
                Id = NewId.NextGuid(),
                Name = userInputData.Name,
                Surname = userInputData.Surname,
                Patronymic = userInputData.Patronymic,
                Login = userInputData.Login,
                Password = userPasswordHash,
                BirthDate = userInputData.BirthDate,
                StatusId = (int)UserStatus.READER,
                PhoneNumber = userInputData.PhoneNumber
            };

            _personRepository.Insert((Person)user);
            _userRepository.Insert(user);
        }

        public ClaimsPrincipal? TryLogin(string login, string password)
        {
            User? validatedUser = _userRepository.GetUserByAuthData(login, password);
            if (validatedUser == null)
                return null;

            var claims = new List<Claim>();
            claims.Add(new Claim("user_id", validatedUser.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, validatedUser.Status.Name));
            claims.Add(new Claim(ClaimTypes.Name, validatedUser.Name));
            claims.Add(new Claim(ClaimTypes.Surname, validatedUser.Surname));
            //TODO: JWT tokens

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return claimsPrincipal;
        }

        public UserDTO ClaimsToDTO(IEnumerable<Claim> claims)
        {
            string role = claims.Single(c => c.Type == ClaimTypes.Role).Value;
            return new UserDTO
            {
                Name = claims.Single(c => c.Type == ClaimTypes.Name).Value,
                Surname = claims.Single(c => c.Type == ClaimTypes.Surname).Value,
                Role = role,
                RoleTranslation = _roleTranslation[role]
            };
        }
    }
}
