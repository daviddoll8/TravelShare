using Microsoft.AspNetCore.Identity;

namespace TravelShare.UserManagement;

public class TravelShareUser : IdentityUser
{
    [PersonalData]
    public override string UserName { get; set; }
    //public ICollection<Post> Posts { get; set; }
}