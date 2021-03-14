using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Stores.Api.Entities
{
    public class StoreCategory
    {
        public StoreCategory()
        {
        }

        public StoreCategory(string name) => StoreCategoryName = name;
        public int StoreCategoryId { get; set; }

        [JsonIgnore]
        public int StoreId { get; set; }

        [JsonIgnore]
        public Store Store { get; set; }

        [JsonIgnore]
        public List<Product> Products { get; set; }

        [Required]
        public string StoreCategoryName { get; set; }
    }
}