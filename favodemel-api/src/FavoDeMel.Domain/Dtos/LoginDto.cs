using FavoDeMel.Domain.Helpers;
using FavoDeMel.Domain.Interfaces;

namespace FavoDeMel.Domain.Dtos
{
    public class LoginDto : IDtoBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get { return StringHelper.CalculateMD5Hash(Password); } }
    }
}
