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

        public async Task<List<BusinessTag>> GetAllTagsAsync()
        {
            return await _dataContext.BusinessTags.AsNoTracking().ToListAsync();
        }

        public async Task<bool> CreateTagAsync(string tagName)
        {
            var existingTag = await _dataContext.Tags.AsNoTracking().SingleOrDefaultAsync(x => x.TagName == tagName);
            if (existingTag != null)
                return true;

            await _dataContext.Tags.AddAsync(new Tag { TagName = tagName});
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Tag> GetTagByNameAsync(string tagName)
        {
            return await _dataContext.Tags.AsNoTracking().SingleOrDefaultAsync(x => x.TagName == tagName.ToLower());
        }

        public async Task<bool> DeleteTagAsync(string tagName)
        {
            var tag = await _dataContext.Tags.AsNoTracking().SingleOrDefaultAsync(x => x.TagName == tagName.ToLower());

            if (tag == null)
                return false;

            var businessTags = await _dataContext.BusinessTags.Where(x => x.tagId == tag.TagId).ToListAsync();

            _dataContext.BusinessTags.RemoveRange(businessTags);
            _dataContext.Tags.Remove(tag);
            return await _dataContext.SaveChangesAsync() > businessTags.Count;
        }
    }
}
