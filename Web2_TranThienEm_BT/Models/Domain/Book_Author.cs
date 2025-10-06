using Web2_TranThienEm_BT.Models.Domain;
using System.ComponentModel.DataAnnotations;
using Web2_TranThienEm_BT.Models;

namespace Web2_TranThienEm_BT.Models
{
    public class Book_Author
    {
        [Key]
        public int? ID { get; set; }

        public int? BookId { get; set; }
        public int? AuthorId { get; set; }

        public Books? Book { get; set; }
        public Authors? Author { get; set; }
    }
}

