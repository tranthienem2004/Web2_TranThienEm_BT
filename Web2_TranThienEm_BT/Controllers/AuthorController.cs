using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web2_TranThienEm_BT.Data;
using Web2_TranThienEm_BT.Models.DTO;
using Web2_TranThienEm_BT.Models.Sorted;
using Web2_TranThienEm_BT.Repositories;

namespace Web2_TranThienEm_BT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthorRepository _authorrepository;

        public AuthorController(AppDbContext dbContext, IAuthorRepository authorrepository)
        {
            _dbContext = dbContext;
            _authorrepository = authorrepository;
        }

        [HttpGet("get-all-authors/{sorted}")]
        [Authorize(Roles = "Read,Write")]
        public IActionResult GetAll(SortField sorted)
        {
            var allAuthors = _authorrepository.GetAllAuthors(sorted);
            return Ok(allAuthors);
        }

        [HttpGet]
        [Route("get-author-by-id/{id}")]
        [Authorize(Roles = "Read,Write")]

        public IActionResult GetAuthorById([FromRoute] int id)
        {
            var authorWithIdDTO = _authorrepository.GetAuthorById(id);
            return Ok(authorWithIdDTO);
        }
        [HttpPost("add - author")]
        [Authorize(Roles = "Write")]
        public IActionResult AddAuthor([FromBody] AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var authorAdd = _authorrepository.AddAuthor(addAuthorRequestDTO);
            return Ok(authorAdd);
        }
        [HttpPut("update-author-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AddAuthorRequestDTO authorDTO)
        {
            var updateAuthor = _authorrepository.UpdateAuthorById(id, authorDTO);
            return Ok(updateAuthor);
        }
        [HttpDelete("delete-author-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult DeleteAuthorById(int id)
        {
            var deleteAuthor = _authorrepository.DeleteAuthorById(id);
            return Ok(deleteAuthor);
        }
    }
}
