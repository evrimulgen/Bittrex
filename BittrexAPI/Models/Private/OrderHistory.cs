﻿using System;

namespace BittrexAPI.Models
{
    /// <summary>
    /// Retrieves the users order history
    /// </summary>
    public class OrderHistory
    {
        public string OrderUuid { get; set; }
        public string Exchange { get; set; }
        public DateTime TimeStamp { get; set; }
        public string OrderType { get; set; }
        public decimal Limit { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityRemaining { get; set; }
        public decimal Commision { get; set; }
        public decimal Price { get; set; }
        public decimal PricePerUnit { get; set; }
        public bool IsConditional { get; set; }
        public string Condition { get; set; }
        public string ConditionTarget { get; set; }
        public bool ImmediateOrCancel { get; set; }
    }
}