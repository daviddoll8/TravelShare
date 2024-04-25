using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelShare.UserManagement;

namespace TravelShare.Data;

public class ApplicationDbContext : IdentityDbContext<TravelShareUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}