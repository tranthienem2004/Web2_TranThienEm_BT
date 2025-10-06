using System.ComponentModel.DataAnnotations;

namespace Web2_TranThienEm_BT.Models.DTO
{
    public class AddAuthorRequestDTO
    {
        [Key]
        public string? FullName { get; set; }

        public List<int> AuthorIds { get; set; }
    }
}
