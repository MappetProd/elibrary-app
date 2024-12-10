using EL.Domain;
using EL.Service.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.ViewModel
{
	public class EditBookViewModel : BaseViewModel
	{
		public EditBookViewModel(HttpContext context) : base(context) { }
	
		public EditBookFormDTO book { get; set; }
		public IEnumerable<string> AllGenres { get; set; }
		public IEnumerable<string> AllPublishers { get; set; }
	}
}
