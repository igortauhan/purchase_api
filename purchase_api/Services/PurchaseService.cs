using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using purchase_api.Models;
using purchase_api.Models.Dto;

namespace purchase_api.Services
{
    public class PurchaseService
    {
        private readonly PurchaseContext _context;

        public PurchaseService(PurchaseContext context)
        {
            _context = context;
        }

        public async Task<List<PurchaseDTO>> FindAllAsync()
        {
            return await _context.Purchases.Select(x => ToDTO(x)).ToListAsync();
        }

        public async Task<PurchaseDTO> Find(long id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return null;
            }

            return ToDTO(purchase);
        }

        public async Task<PurchaseDTO> Insert(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return ToDTO(purchase);
        }

        public Purchase FromDTO(PurchaseDTO purchaseDTO) =>
            new Purchase
            {
                Name = purchaseDTO.Name,
                Value = purchaseDTO.Value,
                BuyDate= purchaseDTO.BuyDate
            };

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
