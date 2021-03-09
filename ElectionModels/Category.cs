using ElectionModels.ChangeLoggingAttributes;
using System;
using System.Collections.Generic;

namespace ElectionModels
{
    public interface ICategory
    {
        Guid Id { get; set; }
        Guid ElectionId { get; set; }
        CategoryTypeEnum CategoryTypeId { get; set; }
        string Heading { get; set; }
        string Title { get; set; }
        string SubTitle { get; set; }
        string Information { get; set; }
        int JudgePosition { get; set; }
        int Sequence { get; set; }
        Guid? Selection { get; set; }

        bool Delete { get; set; }  // Use to identify rows to remove from table
        bool HasBeenCreated { get; set; }  // used to identify rows to all to table
        List<Ticket> Tickets { get; set; }
    }

    public class Category
    {
        [LoggingPrimaryKey]
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public CategoryTypeEnum CategoryTypeId { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public int JudgePosition { get; set; }
        public string Information { get; set; }
        public string SubTitle { get; set; }
        public int Sequence { get; set; }
        public Guid? Selection { get; set; }

        [IgnoreLogging]
        public bool Delete { get; set; }
        [IgnoreLogging]
        public bool HasBeenCreated { get; set; }
        [IgnoreLogging]
        public List<Ticket> Tickets { get; set; }             
    }
}
