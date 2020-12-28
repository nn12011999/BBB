using BBB.Data.DataModel.Request;
using BBB.Data.DataModel.Response;
using BBB.Data.Entities;
using BBB.Main.Repositories;
using BBB.Main.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RoleDefine.Admin)]
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

            var response = _categoryServices.AddCategory(category);
            if (response != "OK")
            {
                return BadRequest("Can not execute. Plz contact admin");
            }
            return Ok(response);
        }

        [HttpPost("delete-category")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RoleDefine.Admin)]
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

            var response = _categoryServices.DeleteCategory(category);
            if (response != "OK")
            {
                return BadRequest("Can not execute. Plz contact admin");
            }
            return Ok(response);
        }

        [HttpPost("update-category")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RoleDefine.Admin)]
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

            var response = _categoryServices.UpdateCategory(category);
            if (response != "OK")
            {
                return BadRequest("Can not execute. Plz contact admin");
            }
            return Ok(response);
        }

        [HttpGet("get-by-id")]
        public IActionResult GetCategoryById([FromBody] RequestById request)
        {
            var response = _categoryRepository.FindById(request.Id);
            if (response == null)
            {
                return BadRequest("Category not found");
            }    
            return Ok(response);
        }

        [HttpGet("get-by-url")]
        public IActionResult GetCategoryByUrl([FromBody] RequestByUrl request)
        {
            var response = _categoryRepository.FindByUrl(request.Url);
            if (response == null)
            {
                return BadRequest("Category not found");
            }
            return Ok(response);
        }
    }
}
