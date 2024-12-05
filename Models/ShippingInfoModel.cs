﻿using System.ComponentModel.DataAnnotations;

namespace ApplicationToSellThings.BlazorUI.Models
{
    public class ShippingInfoModel
    {
        [Key]
        public Guid ShippingInfoId { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public int DeliveryStatusId { get; set; }
        public StatusModel DeliveryStatus { get; set; }
    }
}
