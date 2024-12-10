using EL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.Contract
{
    public interface IPublisherService
    {
        public Publisher? GetByName(string name);
    }
}
