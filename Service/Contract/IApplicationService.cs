using EL.Domain;
using EL.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.Contract
{
    public interface IApplicationService
    {
        bool Create(string userId);

        bool Resolve(string applicationId, string resolvedByUserId);

        bool End(string applicationId, string endedByUserId);

        public IEnumerable<ReaderApplicationDTO> GetSentAplications(string userId);

        public IEnumerable<ReaderApplicationDTO> GetResolvedApplications(string userId);

        public IEnumerable<ReaderApplicationDTO> GetEndedApplications(string userId);

        IEnumerable<ModeratingApplicationDTO> GetResolveRequiredApplications();

        IEnumerable<ModeratingApplicationDTO> GetEndRequiredApplications();

        IEnumerable<ModeratingApplicationDTO> GetArchiveApplications();
    }
}
