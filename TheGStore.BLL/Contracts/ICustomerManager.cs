using System;
using System.Threading.Tasks;
using TheGStore.Bll.Models;
using TheGStore.Helpers;
using TheGStore.PagedList;

namespace TheGStore.BLL.Contracts
{
    public interface ICustomerManager
    {
        Task<ResponseResult<IPagedList<CustomerUpdateModel>>> GetList(int pageIndex, int pageSize);
        Task<ResponseResult<CustomerUpdateModel>> GetById(int id);
        Task<ResponseResult<CustomerUpdateModel>> Create(CustomerModel model);
        Task<ResponseResult<bool>> Update(CustomerUpdateModel model);
        Task<ResponseResult<bool>> Delete(int id);
    }
}
