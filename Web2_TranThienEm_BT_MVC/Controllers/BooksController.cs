using Microsoft.AspNetCore.Mvc;
using Web2_TranThienEm_BT_MVC.Models.DTO;


namespace Web2_TranThienEm_BT_MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index([FromQuery] string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true)
        {
            List<BookDTO> response = new List<BookDTO>();
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMess = await client.GetAsync("https://localhost:7111/api/Books/get-all-books?" +
                    "filterOn=" + filterOn + "&" +
                    "filterQuery=" + filterQuery + "&" +
                    "sortBy=" + sortBy + "&" +
                    "isAscending=" + isAscending);
                httpResponseMess.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMess.Content.ReadFromJsonAsync<IEnumerable<BookDTO>>());

            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return View(response);
        }

        public IActionResult Try()
        {

            return View();
        }
    }
}
