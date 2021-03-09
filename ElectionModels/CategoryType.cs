using System;

namespace ElectionModels
{
    public class CategoryType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
