﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductManagement.Models;

#nullable disable

namespace ProductManagement.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240318080731_again")]
    partial class again
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductManagement.Models.Order", b =>
                {
                    b.Property<int>("MaDH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDH"));

                    b.Property<int>("MaSP")
                        .HasColumnType("int");

                    b.Property<double>("SoLuongDH")
                        .HasColumnType("float");

                    b.Property<string>("TenKhachHang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ThanhTien")
                        .HasColumnType("float");

                    b.HasKey("MaDH");

                    b.HasIndex("MaSP");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ProductManagement.Models.Products", b =>
                {
                    b.Property<int>("MaSP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaSP"));

                    b.Property<string>("ChatLieu")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<double>("GiaCa")
                        .HasColumnType("float");

                    b.Property<string>("KichThuoc")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("KieuDang")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("MauSac")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<double>("SoLuongTong")
                        .HasColumnType("float");

                    b.Property<string>("TenSP")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ThuongHieu")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("MaSP");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductManagement.Models.Order", b =>
                {
                    b.HasOne("ProductManagement.Models.Products", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("MaSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductManagement.Models.Products", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
