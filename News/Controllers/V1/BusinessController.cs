using Microsoft.AspNetCore.Mvc;
using News.Contracts.V1;
using News.Data;
using News.Domain;
using System.Threading.Tasks;

namespace News.Controllers.V1
{
    public class BusinessController: Controller
    {

        private readonly DataContext _dataContext;

        [HttpGet(ApiRoutes.Businesses.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_dataContext.Businesses);
        }

        [HttpPost(ApiRoutes.Businesses.Add)]
        public async Task<IActionResult> Add([FromBody] string businessType)
        {
            return Ok(await _dataContext.Businesses.AddAsync(new BusinessType { Name = businessType}));
        }

        [HttpDelete(ApiRoutes.Businesses.Delete)]
        public async Task<IActionResult> Delete([FromBody] string businessType)
        {
            return Ok(await _dataContext.Businesses.AddAsync(new BusinessType { Name = businessType }));
        }
    }

}
