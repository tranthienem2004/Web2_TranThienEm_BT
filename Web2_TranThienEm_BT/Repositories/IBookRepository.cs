using Web2_TranThienEm_BT.Models.Domain;
using Web2_TranThienEm_BT.Models.DTO;
using Web2_TranThienEm_BT.Models.Sorted;
namespace Web2_TranThienEm_BT.Repositories
{
    public interface IBookRepository
    {
        List<BookWithAuthorAndPublisherDTO> GetAllBooks(SortField sortedBy);
        BookWithAuthorAndPublisherDTO GetBookById(int id);
        AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
        AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
        Books? DeleteBookById(int id);
    }
}
