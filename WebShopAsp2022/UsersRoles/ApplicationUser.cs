using Microsoft.AspNetCore.Identity;

namespace WebShopAsp2022.UsersRoles
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PostAddress { get; set; }

        public string Vk { get; set; }
    }
}