using System;
using WebShopAsp2022.Domains;

namespace WebShopAsp2022.BusinessLogic
{
    public class ManagerOrderSales
    {
        // TODO: try-catch и статус
        public OrderTransactionResult TransactionOrder(OrderTransactionRequest request)
        {
            // 1) создаём заказ
            var order = new Order
            {
                UserId = request.User.Id,
                CreationDate = DateTime.Now,
                DeliveryAddress = request.DeliveryAddress,
                Cost = request.UserCart.TotalCost,
                ContactPhone = request.ContactPhone
            };
            request.RepositoryOrder.Create(order);
            // 2) добавляем все позиции по заказу
            foreach (var record in request.UserCart.Records)
            {
                var entityProduct = request
                    .RepositoryProduct
                    .Read(record.Product.ProductId);

                var orderRecord = new OrderRecord
                {
                    Quantity = record.Quantity,
                    Product = entityProduct,
                    OrderForRecord = order
                };
                request.RepositoryOrderRecord.Create(orderRecord);
            }

            var result = new OrderTransactionResult
            {
                Order = order,
                User = request.User,
                IsSuccess = true,
                Message = "Успешная регистрация заказа"
            };

            return result;
        }
    }
}
