namespace Stores.Api.Entities
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
        }

        public PaymentMethod(string methodName) => MethodName = methodName;

        public int PaymentMethodId { get; set; }

        public string MethodName { get; set; }

        public string? MethodDescription { get; set; }
    }
}