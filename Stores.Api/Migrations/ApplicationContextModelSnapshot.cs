using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stores.Api.Data;

namespace Stores.Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    internal class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ProductStoreCategory", b =>
            {
                b.Property<int>("CategoriesStoreCategoryId")
                    .HasColumnType("integer");

                b.Property<int>("ProductsProductId")
                    .HasColumnType("integer");

                b.HasKey("CategoriesStoreCategoryId", "ProductsProductId");

                b.HasIndex("ProductsProductId");

                b.ToTable("ProductStoreCategory");
            });

            modelBuilder.Entity("Stores.Api.Entities.Product", b =>
            {
                b.Property<int>("ProductId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<int>("CountInStock")
                    .HasColumnType("integer");

                b.Property<string>("Description")
                    .HasColumnType("text");

                b.Property<decimal>("Price")
                    .HasColumnType("numeric");

                b.Property<string>("ProductName")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<int>("StoreId")
                    .HasColumnType("integer");

                b.HasKey("ProductId");

                b.HasIndex("StoreId");

                b.ToTable("Products");
            });

            modelBuilder.Entity("Stores.Api.Entities.Store", b =>
            {
                b.Property<int>("StoreId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("Phone")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("StoreName")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("StoreId");

                b.ToTable("Stores.Api");
            });

            modelBuilder.Entity("Stores.Api.Entities.StoreCategory", b =>
            {
                b.Property<int>("StoreCategoryId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("StoreCategoryName")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<int>("StoreId")
                    .HasColumnType("integer");

                b.HasKey("StoreCategoryId");

                b.HasIndex("StoreId");

                b.ToTable("StoreCategories");
            });

            modelBuilder.Entity("ProductStoreCategory", b =>
            {
                b.HasOne("Stores.Api.Entities.StoreCategory", null)
                    .WithMany()
                    .HasForeignKey("CategoriesStoreCategoryId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Stores.Api.Entities.Product", null)
                    .WithMany()
                    .HasForeignKey("ProductsProductId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Stores.Api.Entities.Product", b =>
            {
                b.HasOne("Stores.Api.Entities.Store", null)
                    .WithMany("Products")
                    .HasForeignKey("StoreId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Stores.Api.Entities.StoreCategory", b =>
            {
                b.HasOne("Stores.Api.Entities.Store", "Store")
                    .WithMany("Categories")
                    .HasForeignKey("StoreId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Store");
            });

            modelBuilder.Entity("Stores.Api.Entities.Store", b =>
            {
                b.Navigation("Categories");

                b.Navigation("Products");
            });
#pragma warning restore 612, 618
        }
    }
}