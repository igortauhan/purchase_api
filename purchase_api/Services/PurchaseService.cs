﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
