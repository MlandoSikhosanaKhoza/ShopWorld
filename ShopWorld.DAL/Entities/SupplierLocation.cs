﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShopWorld.DAL;

[Table("SupplierLocation")]
public partial class SupplierLocation
{
    [Key]
    public int SupplierLocationId { get; set; }

    public int SupplierId { get; set; }

    [Required]
    [StringLength(500)]
    public string Address { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    [ForeignKey("SupplierId")]
    [InverseProperty("SupplierLocations")]
    public virtual Supplier Supplier { get; set; }
}