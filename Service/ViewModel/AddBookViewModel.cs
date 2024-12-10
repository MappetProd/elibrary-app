using Microsoft.AspNetCore.Http;

namespace EL.Service.ViewModel
{
	public class AddBookViewModel : BaseViewModel
	{
		public AddBookViewModel(HttpContext context) : base(context) { }

		public IEnumerable<string> Genres { get; set; }
		public IEnumerable<string> Publishers { get; set; }

	}
}
