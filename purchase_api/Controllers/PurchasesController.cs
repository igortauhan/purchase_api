using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using purchase_api.Models;

namespace purchase_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly PurchaseContext _context;

        public PurchasesController(PurchaseContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchase(long id)
        {
            var purchase = await _context.Purchases.FindAsync(id);

            if (purchase == null)
            {
                return NotFound();
            }

            return purchase;
        }

        [HttpPost]
        public async Task<ActionResult<Purchase>> PostPurchase(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPurchase), new { id = purchase.Id }, purchase);
        }
    }
}
