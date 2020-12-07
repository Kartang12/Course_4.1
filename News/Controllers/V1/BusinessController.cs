using Microsoft.AspNetCore.Mvc;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Services;
using System.Threading.Tasks;

namespace News.Controllers.V1
{
    public class BusinessController : Controller
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService bs)
        {
            _businessService = bs;
        }

        [HttpGet(ApiRoutes.Businesses.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _businessService.GetAllBusinesseAsync());
        }
        
        [HttpGet(ApiRoutes.Businesses.Get)]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            return Ok(await _businessService.GetBusinessByIdAsync(id));
        }

        [HttpPost(ApiRoutes.Businesses.Add)]
        public async Task<IActionResult> Add([FromBody] CreateBusinessRequest businessType)
        {
            return Ok(await _businessService.CreateBusinessAsync(businessType.Name, businessType.Tags));
        }

        [HttpDelete(ApiRoutes.Businesses.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            return Ok(await _businessService.DeleteBusinessAsync(id));
        }

        [HttpPut(ApiRoutes.Businesses.Update)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] CreateBusinessRequest request)
        {
            return Ok(await _businessService.UpdateBusinessAsync(id, request));
        }
    }

}
