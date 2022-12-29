using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebShopAsp2022.BusinessLogic;
using WebShopAsp2022.UsersRoles;

namespace WebShopAsp2022.Models
{
    public class RegisterOrderViewModel
    {
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required()]
        [Display(Name="Адрес доставки")]
        public string PostAddress { get; set; }

        [Required()]
        [Display(Name = "Контактный телефон")]
        public string ContactPhone { get; set; }

        public List<CartRecord> Records { get; set; }

        public RegisterOrderViewModel()
        {
            Records = new List<CartRecord>();
        }

        public RegisterOrderViewModel(ApplicationUser user, Cart cart)
        {
            UserName = user.UserName;
            Records = cart.Records;
            PostAddress = user.PostAddress;
            ContactPhone = user.PhoneNumber;
        }
    }
}
