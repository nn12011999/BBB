using BBB.Data.DataModel.Request;
using BBB.Data.DataModel.Response;
using BBB.Data.Entities;
using BBB.Main.Repositories;
using BBB.Main.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _PostRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostServices _PostServices;
        private readonly ICategoryRepository _categoryRepository;
        public PostController(IPostRepository PostRepository,
            IPostServices PostServices,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository)
        {
            _PostRepository = PostRepository;
            _PostServices = PostServices;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllPost()
        {
            return Ok(_PostRepository.GetAllPost());
        }

        [HttpPost("add-Post")]
        public IActionResult AddPost([FromBody] AddPostRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }


            var PostQuery = _PostRepository.FindByTitle(request.Title);
            if (PostQuery != null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post have been create"
                });
            }

            var User = _userRepository.FindById(request.UserId);
            if (User == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "User not found"
                });
            }

            var Category = _categoryRepository.FindById(request.CategoryId);
            if (Category == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Category not found"
                });
            }

            var Post = new Post()
            {
                Title = request.Title,
                Context = request.Context,
                Url = request.Url,
                TimeStamp = request.TimeStamp          
            };

            var respone = _PostServices.AddPost(Post);
            return Ok(respone);
        }

        //[HttpPost("delete-Post")]
        //public IActionResult DeletePost([FromBody] DeletePostRequest request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest(new ErrorViewModel
        //        {
        //            ErrorCode = "400",
        //            ErrorMessage = "Please provide input information correctly."
        //        });
        //    }

        //    if (request.PostId <= 0)
        //    {
        //        return BadRequest(new ErrorViewModel
        //        {
        //            ErrorCode = "400",
        //            ErrorMessage = "Post not found"
        //        });
        //    }

        //    var Post = _PostRepository.FindById(request.PostId);
        //    if (Post == null)
        //    {
        //        return BadRequest(new ErrorViewModel
        //        {
        //            ErrorCode = "400",
        //            ErrorMessage = "Post not found"
        //        });
        //    }

        //    var respone = _PostServices.DeletePost(Post);
        //    return Ok(respone);
        //}

        //[HttpPost("update-Post")]
        //public IActionResult UpdatePost([FromBody] UpdatePostRequest request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest(new ErrorViewModel
        //        {
        //            ErrorCode = "400",
        //            ErrorMessage = "Please provide input information correctly."
        //        });
        //    }

        //    if (request.PostId <= 0)
        //    {
        //        return BadRequest(new ErrorViewModel
        //        {
        //            ErrorCode = "400",
        //            ErrorMessage = "Post not found"
        //        });
        //    }

        //    var Post = _PostRepository.FindById(request.PostId);
        //    if (Post == null)
        //    {
        //        return BadRequest(new ErrorViewModel
        //        {
        //            ErrorCode = "400",
        //            ErrorMessage = "Post not found"
        //        });
        //    }

        //    Post.Name = request.PostName;
        //    Post.Url = request.Url;

        //    var respone = _PostServices.UpdatePost(Post);
        //    return Ok(respone);
        }
    }
}
