using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheGStore.Bll.Models;
using TheGStore.BLL.Contracts;
using TheGStore.Helpers;
using TheGStore.PagedList;
using Microsoft.AspNetCore.Http;

namespace TheGStore.Controllers.Api
{
    [ApiController]
    [Route("/api/orders")]

    public partial class OrdersApiController : Controller
    {
        private readonly IOrderManager _orderManager;

        public OrdersApiController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpGet("{pageSize=10}/{pageIndex=0}")]
        public async Task<IActionResult> Get([FromRoute] int pageSize, [FromRoute] int pageIndex)
        {
            ResponseResult<IPagedList<OrderUpdateModel>> responseResult = new();

            if (pageIndex < 0 || pageSize < 1)
            {
                responseResult.StatusCode = StatusCodes.Status400BadRequest;
                responseResult.Errors.Add("Page index must be >= 0 and page size >=1");
                return StatusCode(responseResult.StatusCode, responseResult);
            }
            responseResult = await _orderManager.GetList(pageIndex, pageSize);
            return StatusCode(responseResult.StatusCode, responseResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var responseResult = await _orderManager.GetById(id);
            return StatusCode(responseResult.StatusCode, responseResult);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderModel model)
        {
            var responseResult = await _orderManager.Create(model);
            return StatusCode(responseResult.StatusCode, responseResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] OrderUpdateModel model)
        {
            ResponseResult<bool> responseResult = new();

            if (id == default || model is null || id != model.Id)
            {
                responseResult.StatusCode = StatusCodes.Status400BadRequest;
                responseResult.Errors.Add("Id can't be default. Address can't be null. Id didn't match");
                return StatusCode(responseResult.StatusCode, responseResult);
            }

            responseResult = await _orderManager.Update(model);
            return StatusCode(responseResult.StatusCode, responseResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _orderManager.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}