using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Stores.Api.Data;
using Stores.Api.DTOs;
using Stores.Api.Entities;
using Stores.Api.Exceptions;

namespace Stores.Api.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IMapper _mapper;

        private readonly IProductRepository _productRepository;
        
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository, IProductRepository productRepository,
            IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Purchase> AddAsync(PurchaseRequest request)
        {
            var paymentMethod = await _purchaseRepository.FindPaymentMethodByIdAsync(request.PaymentMethodId);

            if (paymentMethod == null)
                throw new ApiException("Payment method was not found!");

            var purchase = _mapper.Map<Purchase>(request);
            purchase.PaymentMethod = paymentMethod;

            var list = new List<Product>();

            var updatedProducts = new List<Product>();
            foreach (var requestReceiptPosition in request.ReceiptPositions)
            {
                var product = await _productRepository.Find(request.StoreId, requestReceiptPosition.ProductId);

                if (product == null)
                    throw new ApiException($"Product with id {requestReceiptPosition.ProductId} was not found!");
                if (product.CountInStock < requestReceiptPosition.Count)
                    throw new ApiException($"{product.CountInStock} items left" +
                                           $" for product with id {product.ProductId}" +
                                           $". It is impossible to take {requestReceiptPosition.Count}");

                list.Add(product);
                product.CountInStock -= requestReceiptPosition.Count;
                updatedProducts.Add(product);
            }

            int userId = 0;
            purchase.ReceiptPositions = list.Select((x, i) => new ProductReceiptInformation
            {
                Product = x,
                Count = request.ReceiptPositions[i].Count,
                CustomCategories = request.ReceiptPositions[i].CustomCategories.Select(async name =>
                {
                    var customCategory = await _purchaseRepository.FindCustomCategoryASync(userId, name);
                    return new CustomCategoryForProduct
                    {
                        CustomCategory = customCategory ?? new CustomCategory(name)
                    };
                }).Select(t => t.Result).ToList()
            }).ToList();


            var addedPurchase = await _purchaseRepository.AddAsync(purchase);
            await _purchaseRepository.SaveChangesAsync();

            updatedProducts.ForEach(x => _productRepository.Update(x));
            await _productRepository.SaveChangesAsync();

            return addedPurchase;
        }

        public async Task<IEnumerable<Purchase>> FindAllPurchasesAsync(int userId) =>
            await _purchaseRepository.FindAllAsync(userId);
    }
}