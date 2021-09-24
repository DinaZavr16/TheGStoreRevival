using System;
using System.Threading.Tasks;
using TheGStore.Bll.Models;
using TheGStore.Helpers;
using TheGStore.PagedList;

namespace TheGStore.BLL.Contracts
{
    public interface IGameManager
    {
        Task<ResponseResult<IPagedList<GameUpdateModel>>> GetList(int pageIndex, int pageSize);
        Task<ResponseResult<GameUpdateModel>> GetById(int id);
        Task<ResponseResult<GameUpdateModel>> Create(GameModel model);
        Task<ResponseResult<bool>> Update(GameUpdateModel model);
        Task<ResponseResult<bool>> Delete(int id);
    }
}
