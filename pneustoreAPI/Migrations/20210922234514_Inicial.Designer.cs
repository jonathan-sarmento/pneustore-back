﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pneustoreAPI.Data;

namespace pneustoreAPI.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20210922234514_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("pneustoreAPI.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("imagemUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imagemUrlMarca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("marca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("preco")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.ToTable("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
