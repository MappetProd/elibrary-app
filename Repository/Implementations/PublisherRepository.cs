using EL.Domain;
using EL.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Repository.Implementations
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(ElibraryContext context) : base(context) { }

        public Publisher? Get(string name)
        {
            List<Publisher> publishers = (from p in entities
                                          where p.Name == name
                                          select p).ToList();

            if (publishers.Count == 0)
                return null;

            //TODO: if publishers more than 1 (throw exception?)
            return publishers[0];
        }
    }
}
