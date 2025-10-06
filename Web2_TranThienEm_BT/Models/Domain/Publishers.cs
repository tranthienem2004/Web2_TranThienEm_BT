using Web2_TranThienEm_BT.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Web2_TranThienEm_BT.Models
{
    public class Publishers
    {
        [Key]
        public int? ID { get; set; }
        public string? Name { get; set; }

        public List<Books>? Books { get; set; }
    }
}
