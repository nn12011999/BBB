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
    [Route("api/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagServices _tagServices;
        public TagController(ITagRepository tagRepository,
            ITagServices tagServices)
        {
            _tagRepository = tagRepository;
            _tagServices = tagServices;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllTag()
        {
            return Ok(_tagRepository.GetAllTag());
        }

        [HttpGet("hello")]
        public IActionResult Hello()
        {
            return Ok("hello world");
        }

        [HttpPost("add-tag")]
        public IActionResult AddTag([FromBody] AddTagRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }


            var tagQuery = _tagRepository.FindByName(request.TagName);
            if (tagQuery != null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Tag have been create"
                });
            }

            var tag = new Tag()
            {
                Name = request.TagName,
                Url = request.Url
            };

            var respone = _tagServices.AddTag(tag);
            return Ok(respone);
        }

        [HttpPost("delete-tag")]
        public IActionResult DeleteTag([FromBody] DeleteTagRequest request)
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

            var tag = _tagRepository.FindById(request.TagId);
            if (tag == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Tag not found"
                });
            }

            var respone = _tagServices.DeleteTag(tag);
            return Ok(respone);
        }

        [HttpPost("update-tag")]
        public IActionResult UpdateTag([FromBody] UpdateTagRequest request)
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

            var tag = _tagRepository.FindById(request.TagId);
            if (tag == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Tag not found"
                });
            }

            tag.Name = request.TagName;
            tag.Url = request.Url;

            var respone = _tagServices.UpdateTag(tag);
            return Ok(respone);
        }
    }
}
