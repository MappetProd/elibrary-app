using EL.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.DTO
{
    public class PrintedBookDTO
    {
        public string Id { get; set; }

        public List<AuthorDTO> Authors { get; set; }

        public string Title { get; set; }

        public int AmountLeft { get; set; }

		public string PublisherName { get; set; }

		public int PublishingYear { get; set; }

		public string Genres { get; set; }

		public string ISBN { get; set; }
	}
}
