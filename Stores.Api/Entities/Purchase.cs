using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Stores.Api.Entities
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        public DateTime TimeOfPurchase { get; set; }

        public List<ProductReceiptInformation> ReceiptPositions { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}