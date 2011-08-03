using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealTimeWebStore.Models;

namespace RealTimeWebStore.Code
{
    public class ProductRepository
    {
        private Dictionary<string, ProductModel> _products = new Dictionary<string, ProductModel>();

        public void AddProduct(ProductModel product)
        {
            _products.Add(product.ProductId, product);
        }

        public ProductModel GetProductById(string id)
        {
            ProductModel product = null;
            if (_products.ContainsKey(id))
            {
                product = _products[id];
            }
            return product;
        }

        public bool Buy(string productId)
        {
            bool bought = false;
            var model = GetProductById(productId);
            if (model != null && model.StockLevel > 0)
            {
                model.StockLevel--;
                bought = true;
            }
            return bought;
        }
    }
}