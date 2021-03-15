using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Stores.Api.Entities
{
    public class CustomCategory
    {
        public CustomCategory()
        {
        }

        public CustomCategory(string name) => CustomCategoryName = name;

        public int CustomCategoryId { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        public string CustomCategoryName { get; set; }

        public string? Description { get; set; }

        [JsonIgnore]
        public List<ProductReceiptInformation> Information { get; set; }
    }
}