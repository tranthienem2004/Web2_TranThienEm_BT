using System.ComponentModel.DataAnnotations;
using Web2_TranThienEm_BT.Models;
using Web2_TranThienEm_BT.Models.Domain;

namespace Web2_TranThienEm_BT.Models.Domain
{
    public class Books
    {
        [Required]
        [StringLength(10)]
        public int? ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        [Range(0, 5, ErrorMessage = "From 0 to 5")]
        public int? Rate { get; set; }
        public int? Genre { get; set; }
        public string? CoverUrl { get; set; }
        public DateTime? DateAdded { get; set; }

        public int? PublisherId { get; set; }
        public Publishers? Publisher { get; set; }
        public int? AuthorId { get; set; }
        public Authors? Authors { get; set; }
        public List<Book_Author>? Book_Authors { get; set; }
    }
}