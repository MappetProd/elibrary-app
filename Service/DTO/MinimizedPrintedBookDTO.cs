using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.DTO
{
    public class MinimizedPrintedBookDTO
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<AuthorDTO> Authors { get; set; }

        public string PublisherName { get; set; }

        public int PublishingYear { get; set; }
    }
}
