using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreWeb.Data;
using BookStoreWeb.Models.Domain;

namespace BookStoreWeb.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Orders.Include(o => o.User);
            return View(await dataContext.ToListAsync());
        }

        // GET: Orders
        public async Task<IActionResult> Temp()
        {
            var dataContext = _context.Orders.Include(o => o.User);
            return View(await dataContext.ToListAsync());
        }


        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(order => order.User)
                .Include(order => order.OrderDetails)
                    .ThenInclude(orderProduct => orderProduct.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // Action Done -> khách hàng đã thanh toán và đang giao hàng
        public async Task<IActionResult> Delivered(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(x => x.OrderDetails).FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }
            order.Status = "đã giao hàng";

            _context.Update(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(Temp));
        }

        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }
            order.Status = "chờ lấy hàng";
            
            _context.Update(order);
            _context.SaveChanges();

            return RedirectToAction(nameof(Temp));
        }

        public async Task<IActionResult> Delivering(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }
            order.Status = "đang giao hàng";
            _context.Update(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(Temp));
        }

        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }
            order.Status = "khách hủy đơn";

            foreach (var item in order.OrderDetails)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);

                if (product != null)
                {
                    product.ProducQuantity += item.Quantity;
                    _context.Products.Update(product);
                }
            }

            _context.Update(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(Temp));
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
