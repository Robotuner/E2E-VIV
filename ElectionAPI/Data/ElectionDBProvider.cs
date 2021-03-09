using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAPI.Data
{
    public class ElectionDBProvider : SqlServerConnectionProvider
    {
        public ElectionDBProvider(string connectionString) : base(connectionString)
        {

        }
    }
}
