using Car.DLL.Entities;
using Car.DLL.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Car.BLLayer.Extension
{   
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           // builder.HasOne(o => o.ShipToAddress).WithOne().HasForeignKey<Order>(o => o.Id);
        
            builder.OwnsOne(o => o.ShipToAddress, io => io.WithOwner());
            builder.Property(s => s.OrderStatus).HasConversion(o => o.ToString(),
                o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
            );
          //  builder.Navigation(a => a.ShipToAddress).IsRequired();
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(o => o.SubTotal)
            .HasColumnType("decimal(18, 2)");
        }
    }
}
