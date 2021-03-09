using ElectionModels.ChangeLoggingAttributes;
using System;

namespace ElectionModels
{
    public interface ITicket
    {
        Guid Id { get; set; }
        Guid ElectionId { get; set; }
        Guid CategoryId { get; set; }
        string Description { get; set; }
        int? PartyId { get; set; }
        TicketTypeEnum TicketType { get; set; }
        string Information { get; set; }
        int Sequence { get; set; }
        DateTime CreateDate { get; set; }
        DateTime LastUpdated { get; set; }

        bool Delete { get; set; }  // Use to identify rows to remove from table
        bool HasBeenCreated { get; set; }  // used to identify rows to all to table
        string Party { get; set; }
    }


    public class Ticket
    {
        [LoggingPrimaryKey]
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public int? PartyId { get; set; }

        public TicketTypeEnum TicketType { get; set; }
        public string Information { get; set; }
        public int Sequence { get; set; }
        [IgnoreLogging]
        public DateTime CreateDate { get; set; }
        [IgnoreLogging]
        public DateTime LastUpdated { get; set; }

        [IgnoreLogging]
        public bool Delete { get; set; }
        [IgnoreLogging]
        public bool HasBeenCreated { get; set; }
        [IgnoreLogging]
        public string Party { get; set; }
    }
}
