using System;

namespace ElectionModels
{
    public interface IParty
    {
        int Id { get; set; }
        string Description { get; set; }
        bool Active { get; set; }
        DateTime CreateDate { get; set; }
        DateTime LastUpdated { get; set; }
    }

    public class Party
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
