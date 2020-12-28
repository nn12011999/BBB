using BBB.Data.DataModel.Request;
using BBB.Data.DataModel.Response;
using BBB.Data.Entities;
using BBB.Main.Repositories;
using BBB.Main.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostServices _postServices;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IFileSaveServices _fileSaveServices;
        private readonly IFileSaveRepository _fileSaveRepository;
        public PostController(IPostRepository PostRepository,
            IPostServices PostServices,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IFileSaveServices fileSaveServices,
            IFileSaveRepository fileSaveRepository)
        {
            _postRepository = PostRepository;
            _postServices = PostServices;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _fileSaveServices = fileSaveServices;
            _fileSaveRepository = fileSaveRepository;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllPost()
        {
            return Ok(_postRepository.GetAllPost());
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


            var PostQuery = _postRepository.FindByTitle(request.Title);
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

            var response = _postServices.AddPost(Post);
            if (response != "OK")
            {
                return BadRequest("Can not execute. Plz contact admin");
            }    
            return Ok(response);
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

            var Post = _postRepository.FindById(request.PostId);
            if (Post == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post not found"
                });
            }

            var response = _postServices.DeletePost(Post);
            if (response != "OK")
            {
                return BadRequest("Can not execute. Plz contact admin");
            }
            return Ok(response);
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

            var Post = _postRepository.FindById(request.PostId);
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

            var response = _postServices.UpdatePost(Post);
            if (response != "OK")
            {
                return BadRequest("Can not execute. Plz contact admin");
            }
            return Ok(response);
        }

        [HttpGet("get-by-id")]
        public IActionResult GetPostById([FromBody] int Id)
        {
            var response = _postRepository.FindById(Id);
            if (response == null)
            {
                return BadRequest("Post not found");
            }
            return Ok(response);
        }

        [HttpGet("get-by-url")]
        public IActionResult GetPostByUrl([FromBody] string Url)
        {
            var response = _postRepository.FindByUrl(Url);
            if (response == null)
            {
                return BadRequest("Post not found");
            }
            return Ok(response);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file)
        {
            string response = "";
            try
            {
                if (file.Length > 0 && file.ContentType.Contains("video"))
                {

                    using (var ms = new MemoryStream())
                    {
                        FileSave f = new FileSave();
                        file.CopyTo(ms);
                        f.FileName = file.FileName;
                        f.FileType = file.ContentType;
                        f.FileData = ms.ToArray();
                        response = await _fileSaveServices.AddFileSave(f);
                        if(response != "OK")
                        {
                            return BadRequest("Type format is not a video. Plz contact admin");
                        }
                        response = f.Id.ToString();
                    }
                }
                else
                { 
                    return BadRequest("Type format is not a video. Plz contact admin"); 
                }
            }
            catch
            {
                return BadRequest("Can not upload. Plz contact admin");
            }

            return Ok();
        }

        [HttpGet("get-video-by-id")]
        public ActionResult GetVideoById(int Id)
        {
            var response = _fileSaveRepository.GetById(Id);
            if(response == null)
            {
                return BadRequest("Video not found");
            }
            return File(response.FileData,response.FileType);
        }

        [HttpGet("get-video-by-url")]
        public ActionResult GetVideoByUrl(string Url)
        {
            var response = _fileSaveRepository.GetByUrl(Url);
            if (response == null)
            {
                return BadRequest("Video not found");
            }
            return File(response.FileData, response.FileType);
        }
    }
}