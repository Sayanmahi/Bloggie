using Microsoft.AspNetCore.Identity;

namespace Bloggie.web.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<IdentityUser>> GetAll();
        public Task<bool> Add(IdentityUser user,string password,List<string> roles);
        public Task Delete(Guid userId);
    }
}
