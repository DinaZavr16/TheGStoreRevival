using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheGStore.Bll.Models;
using TheGStore.BLL.Contracts;
using TheGStore.DAL;
using TheGStore.DAL.Models;
using TheGStore.Helpers;
using TheGStore.PagedList;

namespace TheGStore.BLL.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly TheGStoreDbContext _context;
        private readonly ILogger _logger;

        public OrderManager(TheGStoreDbContext context, ILogger<OrderManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseResult<IPagedList<OrderUpdateModel>>> GetList(int pageIndex, int pageSize)
        {
            ResponseResult<IPagedList<OrderUpdateModel>> responseResult = new();

            try
            {
                var models = await _context.Orders
                    .OrderBy(x => x.Date)
                    .ProjectToType<OrderUpdateModel>()
                    .ToPagedListAsync(pageIndex, pageSize);

                if (models.Items.Count == 0)
                {
                    responseResult.StatusCode = StatusCodes.Status404NotFound;
                    responseResult.Errors.Add("Models are not found");

                    return responseResult;
                }

                responseResult.Data = models;
                responseResult.StatusCode = StatusCodes.Status200OK;
                return responseResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting.");
                responseResult.Errors.Add($"An error occurred while getting. Message: {ex.InnerException?.Message ?? ex.Message}");
                responseResult.StatusCode = StatusCodes.Status500InternalServerError;
                return responseResult;
            }
        }

        public async Task<ResponseResult<OrderUpdateModel>> GetById(int id)
        {
            ResponseResult<OrderUpdateModel> responseResult = new();

            try
            {
                var model = await _context.Orders.ProjectToType<OrderUpdateModel>().FirstOrDefaultAsync(x => x.Id == id);

                if (model is null)
                {
                    responseResult.StatusCode = StatusCodes.Status404NotFound;
                    responseResult.Errors.Add("Model is not found");
                    return responseResult;
                }

                responseResult.Data = model;
                responseResult.StatusCode = StatusCodes.Status200OK;
                return responseResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting.");
                responseResult.Errors.Add($"An error occurred while getting. Message: {ex.InnerException?.Message ?? ex.Message}");
                responseResult.StatusCode = StatusCodes.Status500InternalServerError;
                return responseResult;
            }
        }

        public async Task<ResponseResult<OrderUpdateModel>> Create(OrderModel model)
        {
            ResponseResult<OrderUpdateModel> responseResult = new();

            if (model is null)
            {
                responseResult.StatusCode = StatusCodes.Status400BadRequest;
                responseResult.Errors.Add("Model is null");
                return responseResult;
            }

            try
            {
                var entity = model.Adapt<Order>();

                _context.Orders.Add(entity);
                await _context.SaveChangesAsync();

                responseResult.Data = entity.Adapt<OrderUpdateModel>();
                responseResult.StatusCode = StatusCodes.Status201Created;

                return responseResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while creating.");

                responseResult.StatusCode = StatusCodes.Status500InternalServerError;
                responseResult.Errors.Add($"An error occurred while creating. Message: {ex.InnerException?.Message ?? ex.Message}");
                return responseResult;
            }
        }

        public async Task<ResponseResult<bool>> Update(OrderUpdateModel model)
        {
            ResponseResult<bool> responseResult = new();

            if (model is null)
            {
                responseResult.StatusCode = StatusCodes.Status400BadRequest;
                responseResult.Errors.Add("The model has not been updated. Model is empty.");
                return responseResult;
            }
            if (!await IsExisting(model.Id))
            {
                responseResult.StatusCode = StatusCodes.Status400BadRequest;
                responseResult.Errors.Add("The model has not been updated.");
                responseResult.Errors.Add("The model title with such id does not exist.");

                return responseResult;
            }

            try
            {
                var customer = model.Adapt<Order>();

                _context.Entry(customer).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                responseResult.StatusCode = StatusCodes.Status204NoContent;
                responseResult.Data = true;

                return responseResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating.");
                responseResult.Errors.Add($"An error occurred while updating . Message: {ex.InnerException?.Message ?? ex.Message}");
                responseResult.StatusCode = StatusCodes.Status500InternalServerError;
                return responseResult;
            }
        }

        public async Task<ResponseResult<bool>> Delete(int id)
        {
            ResponseResult<bool> responseResult = new();

            if (id == default)
            {
                responseResult.StatusCode = StatusCodes.Status400BadRequest;
                responseResult.Errors.Add("Invaid ID");
                return responseResult;
            }
            if (!await IsExisting(id))
            {
                responseResult.StatusCode = StatusCodes.Status400BadRequest;
                responseResult.Errors.Add("The entity has not been deleted.");
                responseResult.Errors.Add("The entity with such id does not exist.");

                return responseResult;
            }

            var customer = new Order { Id = id };

            try
            {
                _context.Entry(customer).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                responseResult.StatusCode = StatusCodes.Status204NoContent;
                responseResult.Data = true;

                return responseResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting.");
                responseResult.Errors.Add($"An error occurred while deleting. Message: {ex.InnerException?.Message ?? ex.Message}");
                responseResult.StatusCode = StatusCodes.Status500InternalServerError;

                return responseResult;
            }
        }

        public async Task<bool> IsExisting(int id)
        {
            return await _context.Orders.AnyAsync(x => x.Id == id);
        }
    }
}
