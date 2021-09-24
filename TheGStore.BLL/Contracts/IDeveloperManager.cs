using System;
using System.Threading.Tasks;
using TheGStore.Bll.Models;
using TheGStore.Helpers;
using TheGStore.PagedList;

namespace TheGStore.BLL.Contracts
{
    public interface IDeveloperManager
    {
        Task<ResponseResult<IPagedList<DeveloperUpdateModel>>> GetList(int pageIndex, int pageSize);
        Task<ResponseResult<DeveloperUpdateModel>> GetById(int id);
        Task<ResponseResult<DeveloperUpdateModel>> Create(DeveloperModel model);
        Task<ResponseResult<bool>> Update(DeveloperUpdateModel model);
        Task<ResponseResult<bool>> Delete(int id);
    }
}
