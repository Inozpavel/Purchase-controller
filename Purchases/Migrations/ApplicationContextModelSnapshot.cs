using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Purchases.Data;

namespace Purchases.Migrations
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

            modelBuilder.Entity("Purchases.Entities.Purchase", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<decimal>("Cost")
                    .HasColumnType("numeric");

                b.Property<DateTime>("Date")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<int>("UserId")
                    .HasColumnType("integer");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("Purchases");
            });

            modelBuilder.Entity("Purchases.Entities.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("FirstName")
                    .HasColumnType("text");

                b.Property<string>("LastName")
                    .HasColumnType("text");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnType("character varying(64)");

                b.Property<string>("Patronymic")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.ToTable("Users");
            });

            modelBuilder.Entity("Purchases.Entities.Purchase", b =>
            {
                b.HasOne("Purchases.Entities.User", "User")
                    .WithMany("Purchases")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("User");
            });

            modelBuilder.Entity("Purchases.Entities.User", b => { b.Navigation("Purchases"); });
#pragma warning restore 612, 618
        }
    }
}