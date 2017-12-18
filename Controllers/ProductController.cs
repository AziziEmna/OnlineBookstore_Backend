using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {
        public IEnumerable<Product> Get()
        {
            using (ProductBASEEEntities4 db = new ProductBASEEEntities4())
            {

                return db.Product.ToList();

            }

        }

        public IEnumerable<Product> Get(string code)
        {
            using (ProductBASEEEntities4 entities = new ProductBASEEEntities4())

            {
                List<Product> list = new List<Product>();

                foreach (Product p in entities.Product.ToList())
                {
                    
                    if (p.category == code)
                    {
                        list.Add(p);

                    }
                    
                    bool contains = p.productName.IndexOf(code, StringComparison.OrdinalIgnoreCase) >= 0;
                    if (contains==true)
                    {
                        list.Add(p);

                    }
                }
                IEnumerable<Product> enu = list;
                return enu; 





                /*entities.Product.FirstOrDefault(e => e.category== code)*/
            }

        }
    }
}
