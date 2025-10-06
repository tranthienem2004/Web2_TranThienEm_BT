using System.ComponentModel.DataAnnotations;

namespace Web2_TranThienEm_BT.Models.DTO
{
    public class AddPublisherRequestDTO
    {
        [Key]
        public string? Name { get; set; }

        public List<int> BookIds { get; set; }
    }
}
