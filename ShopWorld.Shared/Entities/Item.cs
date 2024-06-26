﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShopWorld.Shared.Entities
{
    public partial class Item
    {
        public Item()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        [Key]
        public int ItemId { get; set; }
        [StringLength(300)]
        public string ImageName { get; set; }
        [StringLength(40)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty("Item")]
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}