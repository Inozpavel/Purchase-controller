using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Stores.Api.Entities
{
    public class ProductReceiptInformation
    {
        [JsonIgnore]
        public int ProductReceiptInformationId { get; set; }

        [JsonIgnore]
        public int PurchaseId { get; set; }

        [JsonIgnore]
        public Purchase Purchase { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Count { get; set; }


        public List<CustomCategoryForProduct> CustomCategories { get; set; }
    }
}