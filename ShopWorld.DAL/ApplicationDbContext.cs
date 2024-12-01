using Microsoft.EntityFrameworkCore;

namespace ShopWorld.DAL
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }

        public virtual DbSet<Employee> Employee { get; set; }

        public virtual DbSet<Item> Item { get; set; }

        public virtual DbSet<Logistic> Logistics { get; set; }

        public virtual DbSet<Order> Order { get; set; }

        public virtual DbSet<OrderItem> OrderItem { get; set; }

        public virtual DbSet<StockItem> StockItem { get; set; }

        public virtual DbSet<Supplier> Supplier { get; set; }

        public virtual DbSet<SupplierLocation> SupplierLocation { get; set; }

        public virtual DbSet<Warehouse> Warehouse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("PK_Customers");

                entity.HasIndex(e => e.Mobile, "UQ__Customer__6FAE07824D3A9B73").IsUnique();

                entity.Property(e => e.Mobile).HasMaxLength(11);
                entity.Property(e => e.Name).HasMaxLength(40);
                entity.Property(e => e.Surname).HasMaxLength(40);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId).HasName("PK_Employees");

                
                entity.Property(e => e.Name).HasMaxLength(40);
                
                entity.Property(e => e.Surname).HasMaxLength(40);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemId).HasName("PK_Items");

                entity.Property(e => e.Description).HasMaxLength(40);
                entity.Property(e => e.ImageName).HasMaxLength(300);
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Logistic>(entity =>
            {
                entity.HasKey(e => e.LogisticsId).HasName("PK__Logistic__15C9051BF0B956FD");

                entity.Property(e => e.DateDelivered).HasColumnType("datetime");
                entity.Property(e => e.DueDate).HasColumnType("datetime");
                entity.Property(e => e.FromString)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.SupplierCost).HasColumnType("decimal(10, 3)");
                entity.Property(e => e.VehicleDescription)
                    .IsRequired()
                    .HasMaxLength(300);
                entity.Property(e => e.VehicleNumberPlate)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.WarehouseDispatchDate).HasColumnType("datetime");

                entity.HasOne(d => d.SupplierFrom).WithMany(p => p.Logistics)
                    .HasForeignKey(d => d.SupplierFromId)
                    .HasConstraintName("FK__Logistics__Suppl__403A8C7D");

                entity.HasOne(d => d.WarehouseDelivery).WithMany(p => p.LogisticWarehouseDeliveries)
                    .HasForeignKey(d => d.WarehouseDeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Logistics__Wareh__412EB0B6");

                entity.HasOne(d => d.WarehouseFrom).WithMany(p => p.LogisticWarehouseFroms)
                    .HasForeignKey(d => d.WarehouseFromId)
                    .HasConstraintName("FK__Logistics__Wareh__3F466844");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("PK_Orders");

                entity.Property(e => e.DeliveryAddress).HasMaxLength(500);
                entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.VAT).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers_CustomerId");

                entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_Employees_EmployeeId");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.OrderItemId).HasName("PK_OrderItems");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.QuantityPacked).HasDefaultValueSql("((0))");
                entity.Property(e => e.QuantityReserved).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Item).WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_OrderItems_Items_ItemId");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderItems_Orders_OrderId");
            });

            modelBuilder.Entity<StockItem>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.StockItemId).ValueGeneratedOnAdd();
                entity.Property(e => e.StockItemType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item).WithMany()
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StockItem__ItemI__46E78A0C");

                entity.HasOne(d => d.Logistics).WithMany()
                    .HasForeignKey(d => d.LogisticsId)
                    .HasConstraintName("FK__StockItem__Logis__45F365D3");

                entity.HasOne(d => d.Supplier).WithMany()
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__StockItem__Suppl__440B1D61");

                entity.HasOne(d => d.Warehouse).WithMany()
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK__StockItem__Wareh__4316F928");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666B49566983F");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(400);
                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplierLocation>(entity =>
            {
                entity.HasKey(e => e.SupplierLocationId).HasName("PK__Supplier__9315C4398E43896B");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierLocations)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SupplierL__Suppl__3C69FB99");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(e => e.WarehouseId).HasName("PK__Warehous__2608AFF9C72BF592");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}