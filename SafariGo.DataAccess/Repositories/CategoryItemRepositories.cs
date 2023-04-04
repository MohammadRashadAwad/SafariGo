using Microsoft.EntityFrameworkCore;
using SafariGo.Core.Dto.Request.CategoryItem;
using SafariGo.Core.Dto.Response;
using SafariGo.Core.Models;
using SafariGo.Core.Repositories;
using SafariGo.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.DataAccess.Repositories
{
    public class CategoryItemRepositories : ICategoryItemRepositories
    {
        private readonly ApplicationDbContext _context;
        private readonly ICloudinaryServices _cloudinaryServices;

        public CategoryItemRepositories(ApplicationDbContext context, ICloudinaryServices cloudinaryServices)
        {
            _context = context;
            _cloudinaryServices = cloudinaryServices;
        }

        public async Task<BaseResponse> AddCategoryItem(CategoryItemRequest request)
        {
            if (await _context.Categories.FindAsync(request.CategoryId) is null)
                return new BaseResponse { Errors = new { Category = "There is no category" } };
            var upload = await _cloudinaryServices.UploadAsync(request.Cover);
            if (!upload.Status)
                return new BaseResponse { Errors = new { Cover = upload.Message } };
            var categoryItem = new CategoryItem
            {
                Title = request.Title,
                Description = request.Description,
                Cover = upload.Data.ToString(),
                CategoryId = request.CategoryId,

            };
            await _context.CategoryItems.AddAsync(categoryItem);
            _context.SaveChanges();
            return new BaseResponse { Status = true, Message = "Success category added" };
        }

        public async Task<BaseResponse> DeleteCategoryItem(string id)
        {
            var categoryItem = await _context.CategoryItems.FindAsync(id);
            if (categoryItem is null)
                return new BaseResponse { Errors = new { CategoryItem = "There is nothing Category Item about this Id" } };
            _context.CategoryItems.Remove(categoryItem);
            _context.SaveChanges();
            await _cloudinaryServices.DeleteResorceAsync(categoryItem.Cover);
            return new BaseResponse { Status = true, Message = "This category Item has been successfully deleted" };
        }

        public async Task<BaseResponse> GetAllCategoryItem()
        {
            var result = await _context.CategoryItems.Select(ci=>new {ci.Id,ci.Title,ci.Description,ci.Cover,ci.CreateAt}).ToListAsync();
            return new BaseResponse { Status = true, Data = result };
        }

        public async Task<BaseResponse> UpdateCategoryItem(string id, CategoryItemRequest request)
        {
            var categoryItem = await _context.CategoryItems.FindAsync(id);
            if (categoryItem is null)
                return new BaseResponse { Errors = new { CategoryItem = "There is nothing Category Item about this Id" } };

            if (await _context.Categories.FindAsync(request.CategoryId) is null)
                return new BaseResponse { Errors = new { Category = "There is no category" } };

            var update = await _cloudinaryServices.UpdateAsync(categoryItem.Cover,request.Cover);
            if (!update.Status)
                return new BaseResponse { Errors = new { Cover = update.Message } };
            
            categoryItem.Title = request.Title;
            categoryItem.Description = request.Description;
            categoryItem.Cover = update.Data.ToString();
            categoryItem.CategoryId = request.CategoryId;
            _context.SaveChanges();
            return new BaseResponse { Status = true, Message = "The category item has been updated successfully" };
           
        }
    }
}
