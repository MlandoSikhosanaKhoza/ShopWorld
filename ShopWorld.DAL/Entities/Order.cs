﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShopWorld.DAL;

[Table("Order")]
public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? DateFulfilled { get; set; }

    public DateTime DateCreated { get; set; }

    public Guid OrderReference { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal VAT { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal GrandTotal { get; set; }

    public int? WarehouseId { get; set; }

    [StringLength(500)]
    public string DeliveryAddress { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Orders")]
    public virtual Employee Employee { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}