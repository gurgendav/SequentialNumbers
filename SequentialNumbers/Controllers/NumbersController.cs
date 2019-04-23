using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using SequentialNumbers.Database;
using SequentialNumbers.Models;

namespace SequentialNumbers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        private readonly SqDbContext _dbContext;

        public NumbersController(SqDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<long> Get(string key)
        {
            var entry = _dbContext.SequenceEntries.FirstOrDefault(s => s.Key == key);

            if (entry == null)
            {
                entry = new SequenceEntry
                {
                    Key = key,
                    Value = 0
                };
                _dbContext.Add(entry);
            }
            
            var current = entry.Value++;

            _dbContext.SaveChanges();
            
            return current;
        }

        [HttpPost]
        public ActionResult Post(CreateSequence model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = new SequenceEntry
            {
                Key = model.Key,
                Value = model.StartValue
            };

            _dbContext.Add(entry);

            _dbContext.SaveChanges();

            return StatusCode((int) HttpStatusCode.Created);
        }
    }
}