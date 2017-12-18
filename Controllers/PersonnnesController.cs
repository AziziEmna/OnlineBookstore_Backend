using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi;

namespace WebApi.Controllers
{
    public class PersonnnesController : ApiController
    {
        private ProductBASEEEntities4 db = new ProductBASEEEntities4();

        // GET: api/Personnnes
        public IQueryable<Personnne> GetPersonnne()
        {
            return db.Personnne;
        }

        // GET: api/Personnnes/5
        [ResponseType(typeof(Personnne))]
        public async Task<IHttpActionResult> GetPersonnne(string id)
        {
            Personnne personnne = await db.Personnne.FindAsync(id);
            if (personnne == null)
            {
                return NotFound();
            }

            return Ok(personnne);
        }

        // PUT: api/Personnnes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPersonnne(string id, Personnne personnne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personnne.login)
            {
                return BadRequest();
            }

            db.Entry(personnne).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonnneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Personnnes
        [ResponseType(typeof(Personnne))]
        public async Task<IHttpActionResult> PostPersonnne(Personnne personnne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Personnne.Add(personnne);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonnneExists(personnne.login))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = personnne.login }, personnne);
        }

        // DELETE: api/Personnnes/5
        [ResponseType(typeof(Personnne))]
        public async Task<IHttpActionResult> DeletePersonnne(string id)
        {
            Personnne personnne = await db.Personnne.FindAsync(id);
            if (personnne == null)
            {
                return NotFound();
            }

            db.Personnne.Remove(personnne);
            await db.SaveChangesAsync();

            return Ok(personnne);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonnneExists(string id)
        {
            return db.Personnne.Count(e => e.login == id) > 0;
        }
    }
}