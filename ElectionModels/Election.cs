using ElectionModels.ChangeLoggingAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ElectionModels
{
    public interface IElection
    {
        Guid Id { get; set; }
        DateTime Date { get; set; }
        DateTime StartDateLocal { get; set; }
        DateTime EndDateLocal { get; set; }
        string Description { get; set; } 
        DateTime CreateDate { get; set; }
        DateTime LastUpdated { get; set; }
        string Version { get; set; }
        bool AllowUpdates { get; set; }

        bool Delete { get; set; }  // Use to identify rows to remove from table
        bool HasBeenCreated { get; set; } // used to identify rows to all to table
        List<Category> CategoryList { get; set; }
        List<Party> PartyList { get; set; }
    }
    public class Election
    {
        [LoggingPrimaryKey]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateLocal { get; set; }
        public DateTime EndDateLocal { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public bool AllowUpdates { get; set; }
        [IgnoreLogging]
        public DateTime CreateDate { get; set; }
        [IgnoreLogging]
        public DateTime LastUpdated { get; set; }
        [IgnoreLogging]
        public bool Delete { get; set; } 
        [IgnoreLogging]
        public bool HasBeenCreated { get; set; }
        [IgnoreLogging]
        public List<Category> CategoryList { get; set; }
        [IgnoreLogging]
        public List<Party> PartyList { get; set; }
    }
}
