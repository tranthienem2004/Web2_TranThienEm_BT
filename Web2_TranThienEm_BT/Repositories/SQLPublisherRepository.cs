using Web2_TranThienEm_BT.Data;
using Web2_TranThienEm_BT.Models;
using Web2_TranThienEm_BT.Models.DTO;
using Web2_TranThienEm_BT.Models.Sorted;
using Web2_TranThienEm_BT.Repositories;

namespace Web2_TranThienEm_BT.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _dbContext;
        public SQLPublisherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Publishers> GetAllPublishers(SortField sortedBy)
        {
            IQueryable<Publishers> query = _dbContext.Publishers;

            switch (sortedBy)
            {
                case SortField.Name:
                    query = query.OrderBy(publisher => publisher.Name);
                    break;
                case SortField.ID:
                    query = query.OrderBy(publisher => publisher.ID);
                    break;
                default:
                    break;
            }

            var AllPublishers = query.Select(publisher => new Publishers()
            {
                ID = publisher.ID,
                Name = publisher.Name,
            }).ToList();

            return AllPublishers;
        }
        public Publishers GetPublishersById(int id)
        {
            var publisherWithDomain = _dbContext.Publishers.Where(n => n.ID == id);

            var publisherWithIdDTO = publisherWithDomain.Select(publishers => new Publishers()
            {
                ID = publishers.ID,
                Name = publishers.Name,
            }).FirstOrDefault();
            return publisherWithIdDTO;
        }

        public AddPublisherRequestDTO AddPublishers(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var publisherDomainModel = new Publishers
            {
                Name = addPublisherRequestDTO.Name,
            };

            _dbContext.Publishers.Add(publisherDomainModel);
            _dbContext.SaveChanges();
            return addPublisherRequestDTO;
        }

        public AddPublisherRequestDTO? UpdatePublisherById(int id, AddPublisherRequestDTO publisherDTO)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.ID == id);
            if (publisherDomain != null)
            {
                publisherDomain.Name = publisherDTO.Name;
                _dbContext.SaveChanges();
            }

            var booksToRemove = _dbContext.Books.Where(b => b.PublisherId == id).ToList();
            if (booksToRemove != null)
            {
                foreach (var book in booksToRemove)
                {
                    book.PublisherId = null;
                }
                _dbContext.SaveChanges();
            }

            if (publisherDTO != null)
            {
                return publisherDTO;
            }
            else
            {
                foreach (var bookId in publisherDTO.BookIds)
                {
                    var book = _dbContext.Books.FirstOrDefault(b => b.ID == bookId);
                    if (book != null)
                    {
                        book.PublisherId = id;
                    }
                }
                _dbContext.SaveChanges();
            }
            return publisherDTO;
        }

        public Publishers? DeletePublisherById(int id)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.ID == id);
            if (publisherDomain != null)
            {
                _dbContext.Publishers.Remove(publisherDomain);
                _dbContext.SaveChanges();
            }
            return publisherDomain;
        }
    }
}
