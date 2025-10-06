using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Web2_TranThienEm_BT.Data;
using Web2_TranThienEm_BT.Models.DTO;
using Web2_TranThienEm_BT.Models.Sorted;
using Web2_TranThienEm_BT.Repositories;

namespace Web2_TranThienEm_BT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublisherController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IPublisherRepository _publisherRepository;

        public PublisherController(AppDbContext dbContext, IPublisherRepository publisherRepository)
        {
            _dbContext = dbContext;
            _publisherRepository = publisherRepository;
        }

        [HttpGet("get-all-publishers")]
        [Authorize(Roles = "Read,Write")]
        public IActionResult GetAllPublishers(SortField sorted,
            string? publisherName = null,
            int page = 1, int pageSize = 10
            )
        {
            var allPublishers = _publisherRepository.GetAllPublishers(sorted);

            if (!string.IsNullOrEmpty(publisherName))
            {
                allPublishers = allPublishers.Where(p => p.Name.Contains(publisherName)).ToList();
            }
            var totalItems = allPublishers.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var booksOnPage = allPublishers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var paginationMetadata = new
            {
                totalItems,
                pageSize,
                currentPage = page,
                totalPages
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(booksOnPage);
        }

        [HttpGet]
        [Route("get-publisher-by-id/{id}")]
        [Authorize(Roles = "Read,Write")]
        public IActionResult GetPublisherById([FromRoute] int id)
        {
            var publisherWithIdDTO = _publisherRepository.GetPublishersById(id);
            return Ok(publisherWithIdDTO);
        }

        [HttpPost("add-publisher")]
        [Authorize(Roles = "Write")]
        public IActionResult AddPublisher([FromBody] AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var publisherAdd = _publisherRepository.AddPublishers(addPublisherRequestDTO);
            return Ok(publisherAdd);
        }

        [HttpPut("update-publisher-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult UpdatePublisherById(int id, [FromBody] AddPublisherRequestDTO publisherDTO)
        {
            var updatePublisher = _publisherRepository.UpdatePublisherById(id, publisherDTO);
            return Ok(updatePublisher);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult DeletePublisherById(int id)
        {
            var deletePublisher = _publisherRepository.DeletePublisherById(id);
            return Ok(deletePublisher);
        }

    }
}
