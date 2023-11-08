using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class TagRepository:ITagRepository
    {
        private readonly BloggieDbContext db;
        public TagRepository(BloggieDbContext db)
        {
            this.db= db;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var d=await db.Tag.ToListAsync();
            return d.DistinctBy(x => x.Name.ToLower());

        }
    }
}
