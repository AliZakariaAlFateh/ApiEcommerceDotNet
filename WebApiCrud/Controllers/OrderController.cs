using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApiCrud.Entyites;
using WebApiCrud.Models;
using WebApiCrud.ViewModels;

namespace WebApiCrud.Controllers
{
    //[Route("api/[controller]/[action]")]
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {

        private readonly MyDbContext _db;
        public OrderController(MyDbContext db)
        {
            _db = db;
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[FromBody]
        //[HttpPost("addOrder")]
        //public ActionResult addOrder(DtoCreateOrder order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return Ok(new { message = "Successfully Order Added......." });
        //    }
        //    else
        //    {
        //        return BadRequest(new { message = "This Model Dismatch........." });
        //    }

        //}


        [HttpPost("addOrder")]
        [AllowAnonymous]
        public async Task<IActionResult> PostOrder(DtoOrder orderViewModel)
        {
            if (ModelState.IsValid)
            {
                using var transaction = await _db.Database.BeginTransactionAsync();
                try
                {
                    // Create the Order entity
                    var order = new Order
                    {
                        UserId = orderViewModel.UserId,
                        Username=orderViewModel.Username,
                        OrderItems = orderViewModel.OrderItems.Select(oi => new OrderItem
                        {
                            ProductName = oi.ProductName,
                            ProductId=oi.ProductId,
                            Price = oi.Price,
                            Count = oi.Count,
                            ImageName = oi.ImageName
                        }).ToList()
                    };

                    // Add the Order entity to the DbContext
                    _db.Orders.Add(order);

                    // Update the Qty for each product based on the OrderItems
                    foreach (var orderItem in order.OrderItems)
                    {
                        var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductName == orderItem.ProductName);
                        if (product != null)
                        {
                            if (product.Qty >= orderItem.Count)
                            {
                                product.Qty -= orderItem.Count;
                                product.soldCount += orderItem.Count;
                            }
                            else
                            {
                                return BadRequest(new { message = $"Not enough stock for product: {product.ProductName}" });
                            }
                        }
                        else
                        {
                            return BadRequest(new { message = $"Product not found: {orderItem.ProductName}" });
                        }
                    }

                    // Save changes to the database
                    await _db.SaveChangesAsync();

                    // Commit the transaction
                    await transaction.CommitAsync();

                    return Ok(new { message = "Successfully Order Added......." });
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { message = "An error occurred while processing the order.", error = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "This Model Dismatch........." });
            }
        }



        //all orders with its items for all users .........
        [HttpGet("GetAllOrdersUsers")]
        public async Task<IActionResult> GetAllOrdersForUsers()
        {
            var orders = await _db.Orders
                .Include(o => o.OrderItems)
                .Select(o => new DtoOrder
                {
                    id = o.Id,
                    UserId = o.UserId,
                    OrderItems = o.OrderItems.Select(oi => new DtoOrderItem
                    {
                        Id = oi.Id,
                        ProductId=oi.ProductId,
                        ProductName = oi.ProductName,
                        Price = oi.Price,
                        Count = oi.Count,
                        ImageName = oi.ImageName
                    }).ToList()
                }).ToListAsync();

            return Ok(orders);
        }

        //return all orders for specific user ........
        //[HttpGet("orders/user/{userId}")]
        //[HttpGet("GetOrdersByUser")]
        [HttpGet("GetOrdersByUser/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            var orders = await _db.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .Select(o => new DtoOrder
                {
                    id = o.Id,
                    UserId = o.UserId,
                    OrderItems = o.OrderItems.Select(oi => new DtoOrderItem
                    {
                        Id = oi.Id,
                        ProductName = oi.ProductName,
                        Price = oi.Price,  // Explicit conversion from float to decimal?
                        Count = oi.Count,
                        ImageName = oi.ImageName
                    }).ToList()
                }).ToListAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound(new { message = "No orders found for this user." });
            }

            return Ok(orders);
        }



        //An endpoint that returns the number of orders that include a specific product, based on either productId or productName.
        //[HttpGet("orders/count")]
        [HttpGet("GetOrderCountsByProduct")]
        public async Task<IActionResult> GetOrderCountByProduct([FromQuery] int? productId, [FromQuery] string? productName)
        {
            if (!productId.HasValue && string.IsNullOrWhiteSpace(productName))
            {
                return BadRequest(new { message = "Either productId or productName must be provided." });
            }

            var query = _db.OrderItems.AsQueryable();

            if (productId.HasValue)
            {
                query = query.Where(oi => oi.ProductId == productId.Value);
            }
            else if (!string.IsNullOrWhiteSpace(productName))
            {
                query = query.Where(oi => oi.ProductName == productName);
            }

            var orderCount = await query.Select(oi => oi.OrderId).Distinct().CountAsync();

            return Ok(new { orderCount });
        }




        [HttpGet("GetLastOrderByUser/{userId}")]
        public async Task<IActionResult> GetLastOrderByUserId(string userId)
        {
            var order = await _db.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.Id) // Assuming Id is auto-incremented and represents order time
                .Include(o => o.OrderItems)
                .Select(o => new DtoOrder
                {
                    id = o.Id,
                    //UserId = o.UserId,
                    Username=o.Username,
                    OrderItems = o.OrderItems.Select(oi => new DtoOrderItem
                    {
                        Id = oi.Id,
                        ProductName = oi.ProductName,
                        Price = (decimal?)oi.Price,  // Explicit conversion from float to decimal
                        Count = oi.Count,
                        ImageName = oi.ImageName
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound(new { message = "No orders found for this user." });
            }

            return Ok(order);
        }








    }


}
