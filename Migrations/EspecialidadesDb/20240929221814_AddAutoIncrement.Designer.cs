﻿// <auto-generated />
using PrestadorOnline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace PrestadorOnline.Migrations.EspecialidadesDb
{
    [DbContext(typeof(EspecialidadesDbContext))]
    [Migration("20240929221814_AddAutoIncrement")]
    partial class AddAutoIncrement
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MarketplaceManuten.Models.Especialidades", b =>
                {
                    b.Property<int>("especialidadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("especialidadeId"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("especialidadeId");

                    b.ToTable("Especialidades");
                });
#pragma warning restore 612, 618
        }
    }
}
