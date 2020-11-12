using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public interface ITagService
    {
        Task<List<BusinessTag>> GetAllTagsAsync();

        Task<bool> CreateTagAsync(string tagName);

        Task<Tag> GetTagByNameAsync(string tagName);

        Task<bool> DeleteTagAsync(string tagName);
    }
}
