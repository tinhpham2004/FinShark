using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var comments = await _commentRepo.GetAllAsync();
            
            var commentDto = comments.Select(x => x.ToCommentDto());

            return Ok(commentDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comments = await _commentRepo.GetByIdAsync(id);

            if(comments == null)
            {
                return NotFound();
            }

            return Ok(comments.ToCommentDto());
        }
    }
}
