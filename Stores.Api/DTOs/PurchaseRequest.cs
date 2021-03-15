using System;
using System.Collections.Generic;

namespace Stores.Api.DTOs
{
    public class PurchaseRequest
    {
        public int StoreId { get; set; }

        public DateTime TimeOfPurchase { get; set; } = DateTime.Now;

        public int PaymentMethodId { get; set; }

        public List<ReceiptPosition> ReceiptPositions { get; set; }
    }
}