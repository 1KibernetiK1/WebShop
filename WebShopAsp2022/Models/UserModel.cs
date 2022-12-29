using WebShopAsp2022.UsersRoles;

namespace WebShopAsp2022.Models
{
    public class UserModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        
        public string Username { get; set; }

        public UserModel()
        { }

        public UserModel(ApplicationUser user)
        {
            Firstname = user.FirstName;
            Lastname = user.LastName;
            Username = user.UserName;
        }
    }
}
