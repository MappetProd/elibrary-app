using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.InputModel
{
    public class PrintedBookInputModel
    {

        [FromForm(Name = "authors")]
        public List<AuthorInputModel> Authors { get; set; }

        [FromForm(Name = "title")]
        public string Title { get; set; }

        [FromForm(Name = "publisher")]
        public string PublisherName { get; set; }

        [FromForm(Name = "publishing-year")]
        public int PublishingYear { get; set; }

        [FromForm(Name = "genres")]
        public string Genres { get; set; }

        [FromForm(Name = "isbn")]
        public string ISBN { get; set; }

        [FromForm(Name = "image")]
        public IFormFile? Image { get; set; }

        [FromForm(Name = "amount")]
        public int Amount { get; set; }
    }
}
