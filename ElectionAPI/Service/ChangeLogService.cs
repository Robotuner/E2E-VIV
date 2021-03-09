using ElectionModels;
using ElectionModels.ChangeLoggingAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAPI.Service
{
    // ttps://github.com/exceptionnotfound/ChangeLoggingReflection/tree/master/ChangeLoggingReflection/Services
    public class ChangeLogService
    {
        public List<ChangeLog> GetChanges(object oldEntry, object newEntry)
        {
            List<ChangeLog> logs = new List<ChangeLog>();

            var oldType = oldEntry.GetType();
            var newType = newEntry.GetType();
            if (oldType != newType)
            {
                return logs; //Types don't match, cannot log changes
            }

            var oldProperties = oldType.GetProperties();
            var newProperties = newType.GetProperties();

            var dateChanged = DateTime.Now;
            var primaryKey = (Guid)oldProperties.Where(x => Attribute.IsDefined(x, typeof(LoggingPrimaryKeyAttribute)))?.First().GetValue(oldEntry);
            var className = oldEntry.GetType().Name;

            foreach (var oldProperty in oldProperties)
            {
                var matchingProperty = newProperties.Where(x => !Attribute.IsDefined(x, typeof(IgnoreLoggingAttribute))
                                                                && x.Name == oldProperty.Name
                                                                && x.PropertyType == oldProperty.PropertyType)
                                                    .FirstOrDefault();
                if (matchingProperty == null)
                {
                    continue;
                }
                var oldValue = oldProperty.GetValue(oldEntry)?.ToString();
                var newValue = matchingProperty.GetValue(newEntry)?.ToString();
                if (matchingProperty != null && oldValue != newValue)
                {
                    logs.Add(new ChangeLog()
                    {
                        PrimaryKey = primaryKey,
                        DateChanged = dateChanged,
                        ClassName = className,
                        PropertyName = matchingProperty.Name,
                        OldValue = oldProperty.GetValue(oldEntry)?.ToString(),
                        NewValue = matchingProperty.GetValue(newEntry)?.ToString()
                    });
                }
            }

            return logs;
        }
    }
}
