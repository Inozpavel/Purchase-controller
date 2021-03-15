using System;
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

            modelBuilder.Entity("Stores.Api.Entities.CustomCategory", b =>
            {
                b.Property<int>("CustomCategoryId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("CustomCategoryName")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("Description")
                    .HasColumnType("text");

                b.Property<int>("UserId")
                    .HasColumnType("integer");

                b.HasKey("CustomCategoryId");

                b.ToTable("CustomCategories");
            });

            modelBuilder.Entity("Stores.Api.Entities.CustomCategoryForProduct", b =>
            {
                b.Property<int>("CustomCategoryId")
                    .HasColumnType("integer");

                b.Property<int>("ProductReceiptInformationId")
                    .HasColumnType("integer");

                b.HasKey("CustomCategoryId", "ProductReceiptInformationId");

                b.HasIndex("ProductReceiptInformationId");

                b.ToTable("CustomCategoriesForProducts");
            });

            modelBuilder.Entity("Stores.Api.Entities.PaymentMethod", b =>
            {
                b.Property<int>("PaymentMethodId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("MethodDescription")
                    .HasColumnType("text");

                b.Property<string>("MethodName")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("PaymentMethodId");

                b.ToTable("PaymentMethods");
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

            modelBuilder.Entity("Stores.Api.Entities.ProductReceiptInformation", b =>
            {
                b.Property<int>("ProductReceiptInformationId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<int>("Count")
                    .HasColumnType("integer");

                b.Property<int?>("CustomCategoryId")
                    .HasColumnType("integer");

                b.Property<int>("ProductId")
                    .HasColumnType("integer");

                b.Property<int>("PurchaseId")
                    .HasColumnType("integer");

                b.HasKey("ProductReceiptInformationId");

                b.HasIndex("CustomCategoryId");

                b.HasIndex("ProductId");

                b.HasIndex("PurchaseId");

                b.ToTable("ProductsReceiptInformation");
            });

            modelBuilder.Entity("Stores.Api.Entities.Purchase", b =>
            {
                b.Property<int>("PurchaseId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<int?>("PaymentMethodId")
                    .HasColumnType("integer");

                b.Property<DateTime>("TimeOfPurchase")
                    .HasColumnType("timestamp without time zone");

                b.Property<int>("UserId")
                    .HasColumnType("integer");

                b.HasKey("PurchaseId");

                b.HasIndex("PaymentMethodId");

                b.ToTable("Purchases");
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

                b.ToTable("Stores");
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

            modelBuilder.Entity("Stores.Api.Entities.CustomCategoryForProduct", b =>
            {
                b.HasOne("Stores.Api.Entities.CustomCategory", "CustomCategory")
                    .WithMany()
                    .HasForeignKey("CustomCategoryId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Stores.Api.Entities.ProductReceiptInformation", "ProductReceiptInformation")
                    .WithMany("CustomCategories")
                    .HasForeignKey("ProductReceiptInformationId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("CustomCategory");

                b.Navigation("ProductReceiptInformation");
            });

            modelBuilder.Entity("Stores.Api.Entities.Product", b =>
            {
                b.HasOne("Stores.Api.Entities.Store", null)
                    .WithMany("Products")
                    .HasForeignKey("StoreId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Stores.Api.Entities.ProductReceiptInformation", b =>
            {
                b.HasOne("Stores.Api.Entities.CustomCategory", null)
                    .WithMany("Information")
                    .HasForeignKey("CustomCategoryId");

                b.HasOne("Stores.Api.Entities.Product", "Product")
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Stores.Api.Entities.Purchase", "Purchase")
                    .WithMany("ReceiptPositions")
                    .HasForeignKey("PurchaseId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Product");

                b.Navigation("Purchase");
            });

            modelBuilder.Entity("Stores.Api.Entities.Purchase", b =>
            {
                b.HasOne("Stores.Api.Entities.PaymentMethod", "PaymentMethod")
                    .WithMany()
                    .HasForeignKey("PaymentMethodId");

                b.Navigation("PaymentMethod");
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

            modelBuilder.Entity("Stores.Api.Entities.CustomCategory", b => { b.Navigation("Information"); });

            modelBuilder.Entity("Stores.Api.Entities.ProductReceiptInformation",
                b => { b.Navigation("CustomCategories"); });

            modelBuilder.Entity("Stores.Api.Entities.Purchase", b => { b.Navigation("ReceiptPositions"); });

            modelBuilder.Entity("Stores.Api.Entities.Store", b =>
            {
                b.Navigation("Categories");

                b.Navigation("Products");
            });
#pragma warning restore 612, 618
        }
    }
}