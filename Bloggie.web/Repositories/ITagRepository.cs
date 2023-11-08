using Bloggie.web.Models.Domain;

namespace Bloggie.web.Repositories
{
    public interface ITagRepository
    {
        public Task<IEnumerable<Tag>> GetAllAsync();
    }
}
