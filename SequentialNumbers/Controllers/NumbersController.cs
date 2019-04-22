using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using SequentialNumbers.Models;

namespace SequentialNumbers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        public const string FilePath = "./sequences.csv";

        [HttpGet]
        public ActionResult<long> Get(string key)
        {
            List<SequenceEntry> records;
            
            using (var reader = new StreamReader(FilePath))
            using (var csv = new CsvReader(reader))
            {
                records = csv.GetRecords<SequenceEntry>().ToList();
            }

            var record = records.First(r => r.Key == key);
            var current = record.Value++;
            
            using (var reader = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(reader))
            {
                csv.WriteRecords(records);
            }

            return current;
        }

        [HttpPost]
        public ActionResult Post(CreateSequence model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            List<SequenceEntry> records = null;
            
            if (System.IO.File.Exists(FilePath))
            {
                using (var reader = new StreamReader(FilePath))
                using (var csv = new CsvReader(reader))
                {
                    records = csv.GetRecords<SequenceEntry>().ToList();
                }
            }

            records = records ?? new List<SequenceEntry>();
            records.Add(new SequenceEntry
            {
                Key = model.Key,
                Value = model.StartValue
            });
            
            using (var reader = new StreamWriter(FilePath))
            using (var csv = new CsvWriter(reader))
            {
                csv.WriteRecords(records);
            }

            return StatusCode((int) HttpStatusCode.Created);
        }
    }
}