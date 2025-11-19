using ASP.NETTask.Models;

namespace ASP.NETTask.Service
{
    public class UserService
    {
        public List<UserViewModel> MapUsersToListViewModel(List<User> users)
        {
            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = MapUserToViewModel(user);
                userViewModels.Add(userViewModel);
            }
            return userViewModels;
        }
        private UserViewModel MapUserToViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Website = user.Website,
                Address = new AddressViewModel
                {
                    Street = user.Address.Street,
                    Suite = user.Address.Suite,
                    City = user.Address.City,
                    Zipcode = user.Address.Zipcode,
                    Lat = user.Address.Lat,
                    Lng = user.Address.Lng
                }
            };
        }
    }
}
