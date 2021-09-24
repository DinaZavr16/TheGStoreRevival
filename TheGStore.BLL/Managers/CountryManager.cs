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
    public class CountryManager : ICountryManager
    {
        private readonly TheGStoreDbContext _context;
        private readonly ILogger _logger;

        public CountryManager(TheGStoreDbContext context, ILogger<CountryManager> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseResult<IPagedList<CountryUpdateModel>>> GetList(int pageIndex, int pageSize)
        {
            ResponseResult<IPagedList<CountryUpdateModel>> responseResult = new();

            try
            {
                var models = await _context.Countries
                    .OrderBy(x => x.Name)
                    .ProjectToType<CountryUpdateModel>()
                    .ToPagedListAsync(pageIndex, pageSize);

                if (models.Items.Count == 0)
                {
                    responseResult.StatusCode = StatusCodes.Status404NotFound;
                    responseResult.Errors.Add("Models are not found");

                    return responseResult;
                }

                responseResult.Data =  models;
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

        public async Task<ResponseResult<CountryUpdateModel>> GetById(int id)
        {
            ResponseResult<CountryUpdateModel> responseResult = new();

            try
            {
                var model = await _context.Countries.ProjectToType<CountryUpdateModel>().FirstOrDefaultAsync(x => x.Id == id);

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

        public async Task<ResponseResult<CountryUpdateModel>> Create(CountryModel model)
        {
            ResponseResult<CountryUpdateModel> responseResult = new();

            if (model is null)
            {
                responseResult.StatusCode = StatusCodes.Status400BadRequest;
                responseResult.Errors.Add("Model is null");
                return responseResult;
            }

            try
            {
                var entity = model.Adapt<Country>();

                _context.Countries.Add(entity);
                await _context.SaveChangesAsync();

                responseResult.Data = entity.Adapt<CountryUpdateModel>();
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

        public async Task<ResponseResult<bool>> Update(CountryUpdateModel model)
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
                var entity = model.Adapt<Country>();

                _context.Entry(entity).State = EntityState.Modified;

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

            var country = new Country { Id = id };

            try
            {
                _context.Entry(country).State = EntityState.Deleted;
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
            return await _context.Countries.AnyAsync(x => x.Id == id);
        }
    }
}
