using Microsoft.EntityFrameworkCore;
using SafariGo.Core.Dto.Request.Category;
using SafariGo.Core.Dto.Response;
using SafariGo.Core.Models;
using SafariGo.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.DataAccess.Repositories
{
    public class CategoryRepositories : ICategoryRepositories
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepositories(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse> AddCategory(CategoryRequest request)
        {
            var category =  _context.Categories;
            if ( category.Any(c=>c.Name ==request.Name))
                return new CategoryResponse { Error = new {Category ="The Category is exists" } };
            await category.AddAsync(new Category { Name = request.Name });
            _context.SaveChanges();
            return new CategoryResponse { Status = true, Message= "Success category added" };
           
        }

        public async Task<CategoryResponse> DeleteCategory(string Id)
        {
            var category = await _context.Categories.FindAsync(Id);
            if (category is null)
                return new CategoryResponse { Error=new { Category = "There is nothing Category about this Id" } };
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return new CategoryResponse { Status = true, Message = "This category has been successfully deleted" };
        }

        public async Task<CategoryResponse> GetAllCategory()
        {
            var result = await _context.Categories.ToListAsync();
            return new CategoryResponse { Status=true ,Data = result };
        }

        public async Task<CategoryResponse> UpdateCategory(string Id, CategoryRequest request)
        {
            var category = await _context.Categories.FindAsync(Id);
            if (category is null)
                return new CategoryResponse { Error = new { Category = "There is nothing Category about this Id" } };
            category.Name = request.Name;
            //_context.Update(category);
            _context.SaveChanges();
            return new CategoryResponse { Status = true, Message = "The category has been updated successfully" };
            
        }
    }
}
