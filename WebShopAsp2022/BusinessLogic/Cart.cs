using System.Collections.Generic;
using System.Linq;
using WebShopAsp2022.Models;

namespace WebShopAsp2022.BusinessLogic
{
    public class Cart
    {
        public Cart()
        {
            Records = new List<CartRecord>();
        }

        public int ProductCount => Records.Sum(r => r.Quantity);

        public int TotalCost => Records.Sum(r => r.Cost);

        public void RemoveProduct(ProductModel model)
        {
            var record = Records
                .FirstOrDefault(r => r.Product.ProductId == model.ProductId);

            if (record != null)
            {
                record.Quantity--;
                if (record.Quantity == 0)
                {
                    Records.Remove(record);
                }   
            }
        }

        public void AddProduct(ProductModel model)
        {
            var record = Records
                .FirstOrDefault(r => r.Product.ProductId == model.ProductId);

            if (record == null)
            {
                Records.Add(new CartRecord {Product = model, Quantity = 1 });
            }
            else
            {
                record.Quantity++;
            }
        }

        public List<CartRecord> Records { get; set; }
    }

    public class CartRecord
    {
        public int Cost => Product.Price * Quantity;

        public ProductModel Product { get; set; }

        public int Quantity { get; set; }
    }
}
