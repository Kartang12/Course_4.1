using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> CreateBusinessAsync(string bName)
        {
            var existingB = await _dataContext.Businesses.AsNoTracking().SingleOrDefaultAsync(x => x.Name == bName);
            if (existingB != null)
                return true;

            await _dataContext.Businesses.AddAsync(new BusinessType{ Name = bName.ToLower()});
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<BusinessType> GetBusinessByNameAsync(string bName)
        {
            return await _dataContext.Businesses.AsNoTracking().SingleOrDefaultAsync(x => x.Name == bName.ToLower());
        }

        public async Task<bool> DeleteBusinessAsync(string bName)
        {
            var business = await _dataContext.Businesses.AsNoTracking().SingleOrDefaultAsync(x => x.Name == bName.ToLower());

            if (business == null)
                return false;

            var tags = await _dataContext.BusinessTags.Where(x => x.businessId == business.Id).ToListAsync();

            _dataContext.Businesses.Remove(business);
            _dataContext.BusinessTags.RemoveRange(tags);
            return await _dataContext.SaveChangesAsync() > _dataContext.BusinessTags.Count();
        }

        public async Task<BusinessType> GetBusinessOfUser(string userId)
        {
            var temp = await _dataContext.UserBusiness.SingleOrDefaultAsync(x => x.userId == userId);
            return temp != null ? await _dataContext.Businesses.SingleOrDefaultAsync(x => x.Id.ToString() == temp.sphereId) : null;
        }
    }
}
