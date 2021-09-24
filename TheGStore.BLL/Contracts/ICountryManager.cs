using System;
using System.Threading.Tasks;
using TheGStore.Bll.Models;
using TheGStore.Helpers;
using TheGStore.PagedList;

namespace TheGStore.BLL.Contracts
{
    public interface ICountryManager
    {
        Task<ResponseResult<IPagedList<CountryUpdateModel>>> GetList(int pageIndex, int pageSize);
        Task<ResponseResult<CountryUpdateModel>> GetById(int id);
        Task<ResponseResult<CountryUpdateModel>> Create(CountryModel model);
        Task<ResponseResult<bool>> Update(CountryUpdateModel model);
        Task<ResponseResult<bool>> Delete(int id);
    }
}
