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
    [Route("/api/games")]

    public class GamesApiController : Controller
    {
        private readonly IGameManager _gameManager;

        public GamesApiController(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        [HttpGet("{pageSize=10}/{pageIndex=0}")]
        public async Task<IActionResult> Get([FromRoute] int pageSize, [FromRoute] int pageIndex)
        {
            ResponseResult<IPagedList<GameUpdateModel>> responseResult = new();

            if (pageIndex < 0 || pageSize < 1)
            {
                responseResult.StatusCode = StatusCodes.Status400BadRequest;
                responseResult.Errors.Add("Page index must be >= 0 and page size >=1");
                return StatusCode(responseResult.StatusCode, responseResult);
            }
            responseResult = await _gameManager.GetList(pageIndex, pageSize);
            return StatusCode(responseResult.StatusCode, responseResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var responseResult = await _gameManager.GetById(id);
            return StatusCode(responseResult.StatusCode, responseResult);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] GameModel model)
        {
            var responseResult = await _gameManager.Create(model);
            return StatusCode(responseResult.StatusCode, responseResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GameUpdateModel model)
        {
            ResponseResult<bool> responseResult = new();

            if (id == default || model is null || id != model.Id)
            {
                responseResult.StatusCode = StatusCodes.Status400BadRequest;
                responseResult.Errors.Add("Id can't be default. Address can't be null. Id didn't match");
                return StatusCode(responseResult.StatusCode, responseResult);
            }

            responseResult = await _gameManager.Update(model);
            return StatusCode(responseResult.StatusCode, responseResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _gameManager.Delete(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}