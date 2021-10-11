using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using purchase_api.Models;
using purchase_api.Models.Dto;
using purchase_api.Services;

namespace purchase_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly PurchaseContext _context;
        private readonly PurchaseService _purchaseService;

        public PurchasesController(PurchaseService purchaseService, PurchaseContext context)
        {
            _purchaseService = purchaseService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDTO>>> GetPurchases()
        {
            return await _purchaseService.FindAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDTO>> GetPurchase(long id)
        {
            var purchaseDTO = await _purchaseService.Find(id);

            if (purchaseDTO == null)
            {
                return NotFound();
            }

            return purchaseDTO;
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseDTO>> PostPurchase(PurchaseDTO purchaseDTO)
        {
            var purchase = _purchaseService.FromDTO(purchaseDTO);

            var obj = await _purchaseService.Insert(purchase);

            return CreatedAtAction(nameof(GetPurchase), new { id = obj.Id }, obj);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchase(long id, PurchaseDTO purchaseDTO)
        {
            if (id != purchaseDTO.Id)
            {
                return BadRequest();
            }

            var purchase = await _context.Purchases.FindAsync(id);

            if (purchase == null)
            {
                return NotFound();
            }

            purchase.Name = purchaseDTO.Name;
            purchase.Value = purchaseDTO.Value;
            purchase.BuyDate = purchaseDTO.BuyDate;

            _context.Entry(purchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(long id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseExists(long id)
        {
            return _context.Purchases.Any(e => e.Id == id);
        }

        private static PurchaseDTO ToDTO(Purchase purchase) =>
            new PurchaseDTO
            {
                Id = purchase.Id,
                Name = purchase.Name,
                Value = purchase.Value,
                BuyDate = purchase.BuyDate
            };
    }
}
