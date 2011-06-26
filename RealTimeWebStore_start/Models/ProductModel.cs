using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealTimeWebStore.Models
{
    public class ProductModel
    {
        public string ProductId
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public ProductImage[] Images
        {
            get;
            set;
        }

        public int StockLevel
        {
            get;
            set;
        }

        public string StockStatus
        {
            get
            {
                return (StockLevel > 0 ? "In Stock" : "Out of Stock");
            }
        }
    }
}