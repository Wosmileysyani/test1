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
    public class CustomersController : ControllerBase
    {
        //สร้างตัวแปล _context จาก Class Model Kruweb1Context
        private readonly Kruweb1Context _context;

        //สร้าง Constructor โดยมีค่า Parameter 1 ตัว 
        public CustomersController(Kruweb1Context context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet] 
        public IEnumerable<object> GetCustomer() //เป็นการระบุว่าจะส่งข้อมูลแบบ Get ไม่มีค่า Parameter
        {
            var Customer = from ct in _context.Customers
                           select new
                           {
                               ct.CustId,
                               ct.InitialCode,
                               InitialName = _context.Initials.Where(e => e.InitialCode == ct.InitialCode).Select(p => p.InitialName).FirstOrDefault(),
                               ct.Name,
                               ct.LastName,
                               ct.CustType
                           };
            return Customer;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")] 
        //สามารถทำงานได้พร้อมกันหลายๆงาน
        public async Task<IActionResult> GetCustomer([FromRoute] string id) //เป็นการระบุว่าจะส่งข้อมูลแบบ Get มีค่า Parameter 1 ตัว
        {
            //เช็คเงื่อนไข ใน Model ว่ามีข้อมูลมาหรือไม่ ถ้าไม่มีก็จะให้ส่งค่ากลับเป็น BadRequest
            if (!ModelState.IsValid)//ModelState เอาไว้ตรวจสอบข้อมูล
            {
                return BadRequest(ModelState);
            }

            //await รอให้ค้นหาให้เจอก่อนค่อยไปทำงานอื่น
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")] //ส่งข้อมูลมาแบบ Put เพื่อเปลี่ยนแปลงข้อมูล
        public async Task<IActionResult> PutCustomer([FromRoute] string id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustId)
            {
                return BadRequest();
            }

            //ปรับปรุงข้อมูล
            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                //บันทึกข้อมูลที่มีการเปลี่ยนแปลง
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomer", new { id = customer.CustId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.CustId == id);
        }
    }
}