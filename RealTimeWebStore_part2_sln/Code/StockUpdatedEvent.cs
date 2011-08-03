using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealTimeWebStore.Models;

namespace RealTimeWebStore.Code
{
    public class StockUpdatedEvent: ProductModel
    {
        public StockUpdatedEvent(ProductModel model, string socketId)
        {
            this.Title = model.Title;
            this.Description = model.Description;
            this.Images = model.Images;
            this.ProductId = model.ProductId;
            this.StockLevel = model.StockLevel;
            this.SocketId = socketId;
        }

        public string SocketId { get; set; }
    }
}