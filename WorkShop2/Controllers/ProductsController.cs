using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkShop2.Models;

namespace WorkShop2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Kruweb1Context _context;


        public ProductsController(Kruweb1Context context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<object> GetProduct()
        {
            var Product = from pd in _context.Products
                          select new
                          {
                              //แสดงค่าที่มีอยู่ใน Product
                              pd.Code,
                              pd.Name,
                              pd.Description,
                              pd.Price,
                              pd.UnitPerprice,
                              pd.Qty,
                              pd.Status,
                              pd.UnitCode,
                              //เปรียบเทียบเพื่อเลือกแสดงเฉพาะค่า Name จากตาราง Unit
                              UnitName = _context.Units.Where(e => e.UnitCode == pd.UnitCode).Select(e => e.Name).FirstOrDefault(),
                              //เปรียบเทียบเพื่อเลือกแสดงเฉพาะค่า CatName จากตาราง Category
                              CatName = _context.Categories.Where(c => c.CatId == pd.CatId).Select(c => c.CatNeme).FirstOrDefault()
                          };
            return Product; //ส่งค่า Product กลับไป
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] string id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Code)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.Code))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.Code }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.Code == id);
        }
    }
}