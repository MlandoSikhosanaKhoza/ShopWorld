﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShopWorld.DAL;

[Table("Warehouse")]
public partial class Warehouse
{
    [Key]
    public int WarehouseId { get; set; }

    [Required]
    [StringLength(300)]
    public string Name { get; set; }

    [Required]
    [StringLength(1000)]
    public string Description { get; set; }

    [Required]
    [StringLength(500)]
    public string Address { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    [InverseProperty("WarehouseDelivery")]
    public virtual ICollection<Logistic> LogisticWarehouseDeliveries { get; set; } = new List<Logistic>();

    [InverseProperty("WarehouseFrom")]
    public virtual ICollection<Logistic> LogisticWarehouseFroms { get; set; } = new List<Logistic>();
}