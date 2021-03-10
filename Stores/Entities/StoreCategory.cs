using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stores.Entities
{
    public class StoreCategory
    {
        public int StoreCategoryId { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public List<Product> Products { get; set; }

        [Required]
        public string StoreCategoryName { get; set; }
    }
}