using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csv
{
    class Repo
    {
        public List<Data> GetDatabaseDataAsync()
        {
            var stringBuilder = new StringBuilder();
            var data = new List<Data>()
            {
             new Data() { DataUsar = DateTime.Now},
             new Data() { DataUsar = DateTime.Now },
             new Data() { DataUsar = DateTime.Now },
             new Data() { DataUsar = DateTime.Now },
             new Data() { DataUsar = DateTime.Now },
             new Data() {DataUsar = DateTime.Parse("14/02/2001")}
        };

            return data;
        }
    }
}
