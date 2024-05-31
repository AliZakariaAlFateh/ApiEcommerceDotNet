﻿namespace WebApiCrud.ViewModels
{
    public class DtoOrderItem
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int Count { get; set; }
        public string? ImageName { get; set; }

    }
}
