using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class testController : ApiController
    {
        private ProductBASEEEntities4 db = new ProductBASEEEntities4();

        //Creating a method to return Json data   
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                //Prepare data to be returned using Linq as follows  
               
                var query=from t1 in db.Product

                join t2 in db.Author on t1.authorname equals t2.name
                select new
                {
                    t1.idpr,
                    t1.imgUrl,
                    t1.price,
                    t1.productName,
                    t1.description,
                    t1.category,
                    t1.authorname,
                    t2.image
                };
                return Ok(query);
            }
            catch (Exception)
            {
                //If any exception occurs Internal Server Error i.e. Status Code 500 will be returned  
                return InternalServerError();
            }
        }


    }
}
