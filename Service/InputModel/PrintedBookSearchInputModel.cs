using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.InputModel
{
    public class PrintedBookSearchInputModel
    {
        [FromQuery(Name = "genres")]
        public string? SelectedGenres { get; set; }

        [FromQuery(Name = "publishers")]
        public string? SelectedPublishers { get; set; }

        [FromQuery(Name = "authors")]
        public string? SelectedAuthors { get; set; }
    }
}
