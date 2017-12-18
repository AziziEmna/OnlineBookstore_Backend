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
    public class ShopCartsController : ApiController
    {
        private ProductBASEEEntities5 db = new ProductBASEEEntities5();

        // GET: api/ShopCarts
        public IQueryable<ShopCart> GetShopCart()
        {
            return db.ShopCart;
        }

        // GET: api/ShopCarts/5
        [ResponseType(typeof(ShopCart))]
        public async Task<IHttpActionResult> GetShopCart(int id)
        {
            ShopCart shopCart = await db.ShopCart.FindAsync(id);
            if (shopCart == null)
            {
                return NotFound();
            }

            return Ok(shopCart);
        }

        // PUT: api/ShopCarts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutShopCart(int id, ShopCart shopCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shopCart.id)
            {
                return BadRequest();
            }

            db.Entry(shopCart).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopCartExists(id))
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

        // POST: api/ShopCarts
        [ResponseType(typeof(ShopCart))]
        public async Task<IHttpActionResult> PostShopCart(ShopCart shopCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShopCart.Add(shopCart);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShopCartExists(shopCart.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shopCart.id }, shopCart);
        }

        // DELETE: api/ShopCarts/5
        [ResponseType(typeof(ShopCart))]
        public async Task<IHttpActionResult> DeleteShopCart(int id)
        {
            ShopCart shopCart = await db.ShopCart.FindAsync(id);
            if (shopCart == null)
            {
                return NotFound();
            }

            db.ShopCart.Remove(shopCart);
            await db.SaveChangesAsync();

            return Ok(shopCart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShopCartExists(int id)
        {
            return db.ShopCart.Count(e => e.id == id) > 0;
        }
    }
}