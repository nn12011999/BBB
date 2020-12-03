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
    [Route("api/posttag")]
    [ApiController]
    public class PostTagController : ControllerBase
    {
        private readonly IPostTagRepository _PostTagRepository;
        private readonly IPostTagServices _PostTagServices;
        public PostTagController(IPostTagRepository PostTagRepository,
            IPostTagServices PostTagServices)
        {
            _PostTagRepository = PostTagRepository;
            _PostTagServices = PostTagServices;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllPostTag()
        {
            return Ok(_PostTagRepository.GetAllPostTag());
        }

        [HttpPost("delete-PostTag")]
        public IActionResult DeletePostTag([FromBody] DeletePostTagRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }

            if (request.TagId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Tag not found"
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

            var PostTag = _PostTagRepository.FindByPostId_TagId(request.PostId,request.TagId);
            if (PostTag == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "PostTag not found"
                });
            }

            var respone = _PostTagServices.DeletePostTag(PostTag);
            return Ok(respone);
        }

        [HttpPost("update-PostTag")]
        public IActionResult UpdatePostTag([FromBody] UpdatePostTagRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }

            if (request.OldTagId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Tag not found"
                });
            }

            if (request.OldPostId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post not found"
                });
            }

            var PostTag = _PostTagRepository.FindByPostId_TagId(request.OldPostId, request.OldTagId);
            if (PostTag == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "PostTag not found"
                });
            }

            PostTag.PostId = request.NewPostId;
            PostTag.TagId = request.NewTagId;

            var respone = _PostTagServices.UpdatePostTag(PostTag);
            return Ok(respone);
        }
    }
}
