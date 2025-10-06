using Web2_TranThienEm_BT.Models;
using Web2_TranThienEm_BT.Models.DTO;
using Web2_TranThienEm_BT.Models.Sorted;

namespace Web2_TranThienEm_BT.Repositories
{
    public interface IAuthorRepository
    {
        List<Authors> GetAllAuthors(SortField sortedBy);
        Authors GetAuthorById(int id);
        AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
        AddAuthorRequestDTO? UpdateAuthorById(int id, AddAuthorRequestDTO authorDTO);
        Authors? DeleteAuthorById(int id);
    }
}
