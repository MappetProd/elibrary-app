
namespace EL.Service.DTO
{
    public class ReaderEndedApplicationDTO : ReaderApprovedApplicationDTO
    {
        /*public Guid Id { get; set; }

        public DateTime CreationDtm { get; set; }

        public int ApplicationNumber { get; set; }

        public IEnumerable<MinimizedPrintedBookDTO> MinimizedPrintedBookDTOs { get; set; }

        public DateTime ResolveDtm { get; set; }

        public DateOnly DeadlineDate { get; set; }*/

        public DateTime ActualReturnDate { get; set; }
    }
}
