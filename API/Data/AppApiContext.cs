using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Data
{
    public class AppApiContext :IdentityDbContext<User>
    {
        public AppApiContext(DbContextOptions<AppApiContext>options) :base(options)
        {
            
        }
    }
}
