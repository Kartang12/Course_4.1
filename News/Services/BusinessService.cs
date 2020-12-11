using Microsoft.EntityFrameworkCore;
using News.Contracts.V1.Requests;
using News.Data;
using News.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly DataContext _dataContext;

        public BusinessService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<BusinessType>> GetAllBusinesseAsync()
        {
            return await _dataContext.Businesses.AsNoTracking().ToListAsync();
        }

        public async Task<bool> CreateBusinessAsync(string bName, List<Tag> tags)
        {
            var existingB = await _dataContext.Businesses.AsNoTracking().SingleOrDefaultAsync(x => x.Name == bName);
            if (existingB != null)
                return false;

            var a = _dataContext.Businesses.Add(new BusinessType { Name = bName.ToLower() });
            await _dataContext.SaveChangesAsync();
            
            a.Entity.tags = tags;
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<BusinessType> GetBusinessByIdAsync(string bId)
        {
            return await _dataContext.Businesses.AsNoTracking().SingleOrDefaultAsync(x => x.Id.ToString() == bId);
        }

        public async Task<BusinessType> GetBusinessByNameAsync(string bName)
        {
            return await _dataContext.Businesses.AsNoTracking().SingleOrDefaultAsync(x => x.Name == bName.ToLower());
        }

        public async Task<bool> DeleteBusinessAsync(string bId)
        {
            var business = await _dataContext.Businesses.SingleOrDefaultAsync(x => x.Id.ToString() == bId);

            if (business == null)
                return false;

            _dataContext.Businesses.Remove(business);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateBusinessAsync(string bId, CreateBusinessRequest request)
        {
            var business = await _dataContext.Businesses.SingleOrDefaultAsync(x => x.Id.ToString() == bId);

            if (business == null)
                return false;
            if(!string.IsNullOrEmpty(request.Name))
                business.Name = request.Name;
            if(request.Tags.Count > 0)
                business.tags = request.Tags;
            
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<List<BusinessType>> GetBusinessOfUser(string userId)
        {
            var user = await _dataContext.Users.Include(x=> x.businessTypes).SingleOrDefaultAsync(x => x.Id == userId);
            return await Task.FromResult(user.businessTypes);
        }
    }
}
