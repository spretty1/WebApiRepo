using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewWebApi.Models;

namespace NewWebApi.Controllers
{
    public class NumberController : ApiController
    {
        private Entries context = new Entries();
        // GET api/<controller>
        public IEnumerable<Entries> Get()
        {
            IEnumerable<Entries> list = null;

            list = (from e in context.series select new Entries { Series = e.Series, Number = e.number }.AsEnumerable<Entries>());
            return list; 
        }

        // GET api/<controller>/5
        public string Get(string number)
        {
            return (from e in context.series
                    where e.number == number
                    select new Entries
                    {
                        number = e.number,
                        series = e.series
                    }).FirstOrDefault<Entry>();
        }

        // POST api/<controller>
        public void Post(Entry entry)
        {
            context.AddToEntries(new Entry
            {
                number = entry.number,
                series = entry.series
            });
            context.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);
        }

        // PUT api/<controller>/5
        public void Put(string number, Entries entry)
        {
            var ent = context.series.First(e => e.number == number);
            ent.series = Entries.series;
            context.SaveChanges();
        }

        // DELETE api/<controller>/5
        public void Delete(string number)
        {
            var ent = context.Entries.First(e => e.number == number);
            context.Entries.DeleteObject(ent);
            context.SaveChanges();
        }
    }
}