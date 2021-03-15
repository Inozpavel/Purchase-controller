using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stores.Api.DTOs
{
    public class ReceiptPosition
    {
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Count { get; set; }

        public List<string>? CustomCategories { get; set; }
    }
}