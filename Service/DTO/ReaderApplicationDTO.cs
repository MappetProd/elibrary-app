﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.DTO
{
    public class ReaderApplicationDTO
    {
        public Guid Id { get; set; }

        public DateTime CreationDtm { get; set; }

        public int ApplicationNumber { get; set; }

        public IEnumerable<MinimizedPrintedBookDTO> MinimizedPrintedBookDTOs { get; set; }

        public DateTime? ResolveDtm { get; set; }

        public DateOnly? DeadlineDate { get; set; }

        public DateTime? ActualReturnDate { get; set; }
    }
}
