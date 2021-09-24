using System;
using System.Threading.Tasks;
using TheGStore.Bll.Models;
using TheGStore.Helpers;
using TheGStore.PagedList;

namespace TheGStore.BLL.Contracts
{
    public interface IOrderManager
    {
        Task<ResponseResult<IPagedList<OrderUpdateModel>>> GetList(int pageIndex, int pageSize);
        Task<ResponseResult<OrderUpdateModel>> GetById(int id);
        Task<ResponseResult<OrderUpdateModel>> Create(OrderModel model);
        Task<ResponseResult<bool>> Update(OrderUpdateModel model);
        Task<ResponseResult<bool>> Delete(int id);
    }
}
