using Web2_TranThienEm_BT.Models;
using Web2_TranThienEm_BT.Models.DTO;
using Web2_TranThienEm_BT.Models.Sorted;

namespace Web2_TranThienEm_BT.Repositories
{
    public interface IPublisherRepository
    {
        List<Publishers> GetAllPublishers(SortField sortedBy);
        Publishers GetPublishersById(int id);
        AddPublisherRequestDTO AddPublishers(AddPublisherRequestDTO addPublisherRequestDTO);
        AddPublisherRequestDTO? UpdatePublisherById(int id, AddPublisherRequestDTO publisherDTO);
        Publishers? DeletePublisherById(int id);
    }
}
