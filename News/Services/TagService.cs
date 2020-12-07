using Microsoft.EntityFrameworkCore;
using News.Data;
using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public class TagService: ITagService
    {
        private readonly DataContext _dataContext;

        public TagService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            var a = await _dataContext.Tags.AsNoTracking().ToListAsync();
            return a;
        }

        public async Task<bool> CreateTagAsync(string tagName)
        {
            if (tagName == null)
                return false;

            var existingTag = await _dataContext.Tags.AsNoTracking().SingleOrDefaultAsync(x => x.Name == tagName);
            if (existingTag != null)
                return false;

            await _dataContext.Tags.AddAsync(new Tag { Name = tagName});
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Tag> GetTagByNameAsync(string tagName)
        {
            return await _dataContext.Tags.AsNoTracking().SingleOrDefaultAsync(x => x.Name == tagName.ToLower());
        }

        public async Task<bool> DeleteTagAsync(string id)
        {
            var tag = await _dataContext.Tags.AsNoTracking().SingleOrDefaultAsync(x => x.Id.ToString() == id);

            _dataContext.Tags.Remove(tag);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
