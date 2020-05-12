using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace azureCosmosDbApi.Model
{
    public class EmployeeEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string department { get; set; }
    }
}
