using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.ViewModel
{
    public class BaseViewModel
    {
        private const string _MASTER_DEFAULT_VIEW_REF = "~/Pages/Shared/_Master.cshtml";
        private const string _MASTER_LIBRARIAN_VIEW_REF = "~/Pages/Shared/_MasterLibrarian.cshtml";
        private const string _MASTER_READER_VIEW_REF = "~/Pages/Shared/_MasterReader.cshtml";
        private const string _MASTER_OWNER_VIEW_REF = "~/Pages/Shared/_MasterOwner.cshtml";

        private Dictionary<string, string> _masterPages = new Dictionary<string, string>
        {
            {"librarian", _MASTER_LIBRARIAN_VIEW_REF},
            {"reader", _MASTER_READER_VIEW_REF },
            {"owner", _MASTER_OWNER_VIEW_REF }
        };

        public string MasterLayoutRef { get; set; }
        public BaseViewModel(HttpContext context)
        {
            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                MasterLayoutRef = _MASTER_DEFAULT_VIEW_REF;
            }
            else
            {
                Claim? claimRole = context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);
                //TODO: null check
                MasterLayoutRef = _masterPages[claimRole.Value];
            }
        }
    }
}
