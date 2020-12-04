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
        private readonly ITagRepository _tagRepository;
        public PostController(IPostRepository PostRepository,
            IPostServices PostServices,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository)
        {
            _PostRepository = PostRepository;
            _PostServices = PostServices;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllPost()
        {
            return Ok(_PostRepository.GetAllPost());
        }

        [HttpPost("add-post")]
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


            var tags = new List<Tag>();

            if (request.TagIds != null)
            {
                if (request.TagIds.Count != request.TagIds.Distinct().Count())
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Duplicate tag"
                    });
                }

                foreach (var item in request.TagIds)
                {
                    var tag = _tagRepository.FindById(item);
                    if (tag == null)
                    {
                        return BadRequest(new ErrorViewModel
                        {
                            ErrorCode = "400",
                            ErrorMessage = "Category not found"
                        });
                    }
                    tag.Posts = null;
                    tags.Add(tag);
                }
            }

            var Post = new Post()
            {
                Title = request.Title,
                Context = request.Context,
                Url = request.Url,
                TimeStamp = request.TimeStamp,
                Tags = tags,
                CategoryId = request.CategoryId,
                UserId = request.UserId
            };

            var respone = _PostServices.AddPost(Post);
            return Ok(respone);
        }

        [HttpPost("delete-Post")]
        public IActionResult DeletePost([FromBody] DeletePostRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }

            if (request.PostId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post not found"
                });
            }

            var Post = _PostRepository.FindById(request.PostId);
            if (Post == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post not found"
                });
            }

            var respone = _PostServices.DeletePost(Post);
            return Ok(respone);
        }

        [HttpPost("update-Post")]
        public IActionResult UpdatePost([FromBody] UpdatePostRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }

            if (request.PostId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post not found"
                });
            }

            var Post = _PostRepository.FindById(request.PostId);
            if (Post == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post not found"
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

            var tags = new List<Tag>();

            if (request.TagIds != null)
            {
                if (request.TagIds.Count != request.TagIds.Distinct().Count())
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Duplicate tag"
                    });
                }

                foreach (var item in request.TagIds)
                {
                    var tag = _tagRepository.FindById(item);
                    if (tag == null)
                    {
                        return BadRequest(new ErrorViewModel
                        {
                            ErrorCode = "400",
                            ErrorMessage = "Category not found"
                        });
                    }
                    tag.Posts = null;
                    tags.Add(tag);
                }
            }

            Post.Tags = tags;
            Post.Title = request.Title;
            Post.Context = request.Context;
            Post.Url = request.Url;
            Post.TimeStamp = request.TimeStamp;
            Post.CategoryId = request.CategoryId;
            Post.UserId = request.UserId;

            var respone = _PostServices.UpdatePost(Post);
            return Ok(respone);
        }
    }
}