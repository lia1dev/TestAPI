﻿namespace TestAPI.Models
{
    public abstract class BaseViewModel
    {
        public long Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
    }
}