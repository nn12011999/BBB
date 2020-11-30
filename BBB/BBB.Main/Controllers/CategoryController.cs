using BBB.Data.DataModel.Request;
using BBB.Data.DataModel.Response;
using BBB.Data.Entities;
using BBB.Main.Repositories;
using BBB.Main.Services;
using Microsoft.AspNetCore.Mvc;

namespace BBB.Main.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryRepository categoryRepository,
            ICategoryServices categoryServices)
        {
            _categoryRepository = categoryRepository;
            _categoryServices = categoryServices;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllCategory()
        {
            return Ok(_categoryRepository.GetAllCategory());
        }

        [HttpPost("add-category")]
        public IActionResult AddCategory([FromBody] AddCategoryRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }

            if (request.ParentId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Parent category not found"
                });
            }

            var categoryQuery = _categoryRepository.FindByName(request.CategoryName);
            if (categoryQuery != null )
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Category have been create"
                });
            }

            var category = new Category()
            {
                Name = request.CategoryName,
                ParentId = request.ParentId,
                Slug = request.Slug
            };

            var respone = _categoryServices.AddCategory(category);
            return Ok(respone);
        }

        [HttpPost("delete-category")]
        public IActionResult DeleteCategory([FromBody] DeleteCategoryRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }

            if (request.CategoryId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Category not found"
                });
            }

            var category = _categoryRepository.FindById(request.CategoryId);
            if (category == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Category not found"
                });
            }

            var respone = _categoryServices.DeleteCategory(category);
            return Ok(respone);
        }

        [HttpPost("update-category")]
        public IActionResult UpdateCategory([FromBody] UpdateCategoryRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }

            if (request.CategoryId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Category not found"
                });
            }

            var category = _categoryRepository.FindById(request.CategoryId);
            if (category == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Category not found"
                });
            }

            var categoryParent = _categoryRepository.FindById(request.ParentId.GetValueOrDefault());
            if (request.ParentId != null)
            { 
                if (categoryParent == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Parent Category not found"
                    });
                } 
            }

            category.ParentId = request.ParentId;
            category.Name = request.CategoryName;
            category.Slug = request.Slug;

            var respone = _categoryServices.UpdateCategory(category);
            return Ok(respone);
        }
    }
}
