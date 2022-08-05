using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [EnableCors("AllowAccess_To_API")]
    public class ProductsController : ControllerBase
    {
        
        //Looseley coupled
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            //Startup'ta aşağıdaki commentli satır eklenerek injection yapılır
            //services.AddSingleton<IProductService,ProductManager>();
            _productService = productService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
          
            var result = _productService.GetAll();
            if (result.Success )
            {
                return Ok(result);

            }
            return BadRequest(result);

        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {

            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);

        }

        [HttpGet("getByCategory")]
        public IActionResult GetByCategory(int categoryId)
        {

            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);

        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if(result.Success )
            {
                return Ok(result);
            }
            return BadRequest(result);

        }


        //[HttpGet]
        //public List<Product> Get()
        //{
        //    ////Kötü kod bağımlılık var
        //    ////çalışıyor ama Dependency chain var
        //    //IProductService productService = new ProductManager(new EfProductDal());
        //    //var result = productService.GetAll();

        //    //ConctructorDan injection ile geliyor
        //    var result = _productService.GetAll();
        //    return result.Data;

        //}
    }
}
