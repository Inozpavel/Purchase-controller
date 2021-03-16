using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Purchases.Api.Data;
using Purchases.Api.Entities;
using Purchases.Contracts;

namespace Purchases.Api.MassTransit
{
    public class StorePurchaseConsumer : IConsumer<StorePurchase>
    {
        private readonly IPurchasesRepository _repository;

        public StorePurchaseConsumer(IPurchasesRepository repository) => _repository = repository;

        public async Task Consume(ConsumeContext<StorePurchase> context)
        {
            int userId = context.Message.UserId;
            var date = context.Message.Date;
            var purchases = context.Message.PurchaseProducts.Select(x => new Purchase
            {
                UserId = userId,
                Name = x.Name + $" x{x.Count}",
                Price = x.Price * x.Count,
                Date = date
            });
            foreach (var purchase in purchases)
                await _repository.AddAsync(purchase);

            await _repository.SaveChangesAsync();
        }
    }
}