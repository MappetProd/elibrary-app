using EL.Service.InputModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.DTO
{
    public class EditBookFormDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<AuthorInputModel> Authors { get; set; }
        
        public string PublisherName { get; set; }

        public int PublishingYear { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public string ISBN { get; set; }

        public string? ImagePath { get; set; }

        public int Amount { get; set; }
    }
}
