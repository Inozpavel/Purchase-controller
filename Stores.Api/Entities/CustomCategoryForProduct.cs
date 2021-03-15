using System.Text.Json.Serialization;

namespace Stores.Api.Entities
{
    public class CustomCategoryForProduct
    {
        public int ProductReceiptInformationId { get; set; }

        [JsonIgnore]
        public ProductReceiptInformation ProductReceiptInformation { get; set; }

        [JsonIgnore]
        public int CustomCategoryId { get; set; }

        public CustomCategory CustomCategory { get; set; }
    }
}