using System;

namespace ElectionModels
{
    public class ChangeLog
    {
        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        public Guid PrimaryKey { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime DateChanged { get; set; }
    }
}
