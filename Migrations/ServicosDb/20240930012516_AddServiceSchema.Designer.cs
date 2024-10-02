﻿// <auto-generated />
using PrestadorOnline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace PrestadorOnline.Migrations.ServicosDb
{
    [DbContext(typeof(ServicosDbContext))]
    [Migration("20240930012516_AddServiceSchema")]
    partial class AddServiceSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MarketplaceManuten.Models.Servico", b =>
                {
                    b.Property<int>("servicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("servicoId"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("servicoId");

                    b.ToTable("Servico");
                });
#pragma warning restore 612, 618
        }
    }
}
