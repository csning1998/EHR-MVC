using EHR_MVC.Models.Patient;
using EHR_MVC.Models.Users;
using EHR_MVC.Repositories;
using System.Reflection;

namespace EHR_MVC.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserDBModel ConvertUserViewModel2DBModel(UserViewModel viewModel) 
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(viewModel.Password);

            return new UserDBModel
            {
                UserId = viewModel.UserId,
                UserEmail = viewModel.UserEmail,
                PasswordHashed = hashedPassword,
                FamilyName = viewModel.FamilyName,
                GivenName = viewModel.GivenName,
                CreatedAt = DateTime.UtcNow
            };
        }
        public async Task<long> RegisterUserAsync(UserDBModel user)
        {
            return await _userRepository.InsertUserAsync(user);
        }

        public async Task<UserDBModel?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public UserViewModel ConvertUserDBModel2ViewModel(UserDBModel dbModel)
        {
            return new UserViewModel()
            {
                UserEmail = dbModel.UserEmail,
            };
        }
    }
}
