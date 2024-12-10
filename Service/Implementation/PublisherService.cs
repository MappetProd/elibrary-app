using EL.Domain;
using EL.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.Implementation
{
    public class PublisherService
    {
        private readonly IRepository<Publisher> _repository;
        public PublisherService(IRepository<Publisher> repository)
        {
            _repository = repository;
        }

        public Publisher? GetByName(string name)
        {
            IEnumerable<Publisher> allPublishers = _repository.GetAll();
            return allPublishers.SingleOrDefault(p => p.Name == name);
        }
    }
}
