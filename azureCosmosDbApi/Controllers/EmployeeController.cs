using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using azureCosmosDbApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace azureCosmosDbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        string database = "maindb";
        string containerName = "employee";
        DocumentClient dc;

        Uri endpoint = new Uri("https://ap-azure-cosmos-db.documents.azure.com:443/");
        string key = "U2gNEWL4AJqNX9IVLo0bXmbXatdQWpgiwKYtkARgXHJ4hR8vNEfpToZaDn4shggtxoWqPYHutR830v3wVKtaAg==";

        // GET: api/Employee
        [HttpGet]
        public IEnumerable<EmployeeEntity> Get()
        {
            dc = new DocumentClient(endpoint, key);
            ////enumeration operations in the Azure Cosmos DB service
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = 10, EnableCrossPartitionQuery = true };

            IQueryable<EmployeeEntity> query = dc.CreateDocumentQuery<EmployeeEntity>(
                UriFactory.CreateDocumentCollectionUri(database, containerName), queryOptions)
                .Where(x => x.age > 0);

            var vList = new List<EmployeeEntity>();

            if(query != null)
            {
                foreach(var row in query)
                {
                    vList.Add(row);
                }
            }

            return vList;
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeEntity obj)
        {
            dc = new DocumentClient(endpoint, key);
            EmployeeEntity emp = new EmployeeEntity();
            emp.firstName = obj.firstName;
            emp.lastName = obj.lastName;
            emp.age = obj.age;
            emp.department = obj.department;

            var result = await dc.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(database, containerName),
                emp);

            if (result != null)
                return Ok(result.StatusCode);
            else
                return BadRequest();
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

}
