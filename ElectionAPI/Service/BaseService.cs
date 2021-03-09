using ElectionModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ElectionAPI.Service
{
    public class BaseService
    {
        protected ChangeLogService changeLogService = new ChangeLogService();
        protected List<ChangeLog> Changes { get; set; }

        //public bool HasChanges<T, T1>(T obj1, T1 obj2)
        //{
        //    PropertyInfo[] obj1Fields = typeof(T).GetProperties();
        //    PropertyInfo[] obj2Fields = typeof(T1).GetProperties();
        //    foreach (PropertyInfo fi in obj1Fields)
        //    {
        //        PropertyInfo fi2 = obj2Fields.SingleOrDefault(n => n.Name == fi.Name);
        //        if (fi2 != null)
        //        {
        //            var ans1 = fi.GetValue(obj1, null).ToString();
        //            var ans2 = fi2.GetValue(obj2, null).ToString();
        //            if (ans1 != ans2)
        //                return true;
        //        }
        //    }

        //    return false;
        //}


    }
}
