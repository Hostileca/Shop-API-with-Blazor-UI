using Microsoft.AspNetCore.Identity;

namespace Shop.DAL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public BuyerCard BuyerCard { get; set; }
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }
        public List<CartElement> CartElements { get; set; }
    }
}
