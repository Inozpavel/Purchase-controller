using System;
using System.Collections.Generic;

namespace Purchases.Contracts
{
    public class StorePurchase
    {
        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public List<PurchaseProduct> PurchaseProducts { get; set; }
    }
}