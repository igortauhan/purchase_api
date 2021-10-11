using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using purchase_api.Models.Dto;
using purchase_api.Services;

namespace purchase_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly PurchaseService _purchaseService;

        public PurchasesController(PurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
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

            var obj = _purchaseService.FromDTO(purchaseDTO);
            var purchase = await _purchaseService.Update(obj);
            if (purchase == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(long id)
        {
            var purchaseDTO = await _purchaseService.Find(id);
            if (purchaseDTO == null)
            {
                return NotFound();
            }

            await _purchaseService.Delete(id);
            return NoContent();
        }
    }
}
