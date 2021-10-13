using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using purchase_api.Models;
using purchase_api.Models.Dto;
using purchase_api.Services.Exceptions;

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

        public async Task<Purchase> Update(Purchase newPurchase)
        {
            var purchase = await _context.Purchases.FindAsync(newPurchase.Id);
            if (purchase == null)
            {
                return null;
            }

            UpdateData(purchase, newPurchase);
            _context.Entry(purchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

            return purchase;
        }

        public async Task Delete(long id)
        {
            var purchase = await _context.Purchases.FindAsync(id);

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
        }

        // update the data of old purchase that came from db for the new purchase data that came from request
        public void UpdateData(Purchase purchase, Purchase newPurchase)
        {
            purchase.Name = newPurchase.Name;
            purchase.Value = newPurchase.Value;
            purchase.BuyDate = newPurchase.BuyDate;
        }

        public Purchase FromDTO(PurchaseDTO purchaseDTO) =>
            new Purchase
            {
                Id = purchaseDTO.Id,
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
