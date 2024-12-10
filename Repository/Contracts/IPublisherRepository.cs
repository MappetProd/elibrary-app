using EL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Repository.Contracts
{
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        Publisher? Get(string name);
    }
}
