﻿namespace WebApiCrud.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int Count { get; set; }
        public string? ImageName { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
