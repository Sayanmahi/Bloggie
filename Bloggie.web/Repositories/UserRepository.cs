using Bloggie.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext db;
        private readonly UserManager<IdentityUser> um;
        public UserRepository(AuthDbContext db,UserManager<IdentityUser> um)
        {
            this.db = db;
            this.um = um;
        }

        public async Task<bool> Add(IdentityUser user, string password, List<string> roles)
        {
            var result=await um.CreateAsync(user,password);
            if(result.Succeeded)
            {
                result=await um.AddToRolesAsync(user,roles);
                if(result.Succeeded)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task Delete(Guid userId)
        {
            var user=await um.FindByIdAsync(userId.ToString());
            if(user !=null)
            {
                await um.DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users=await db.Users.ToListAsync();
            var superadminusers = await db.Users.FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");
            if(superadminusers != null)
            {
                users.Remove(superadminusers);
            }
            return users;
        }
    }
}
