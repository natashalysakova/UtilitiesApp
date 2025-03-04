﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations;

[DbContext(typeof(UtilitiesDbContext))]
[Migration("20250131010251_AnyUpdates")]
partial class AnyUpdates
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "9.0.1")
            .HasAnnotation("Relational:MaxIdentifierLength", 64);

        MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

        modelBuilder.Entity("Infrastructure.Models.Check", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("char(36)");

                b.Property<decimal>("Amount")
                    .HasColumnType("decimal(65,30)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime(6)");

                b.Property<DateTime>("Date")
                    .HasColumnType("datetime(6)");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("datetime(6)");

                b.Property<Guid>("HomeId")
                    .HasColumnType("char(36)");

                b.Property<bool>("IsArchived")
                    .HasColumnType("tinyint(1)");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("datetime(6)");

                b.HasKey("Id");

                b.HasIndex("HomeId");

                b.ToTable("Checks");
            });

        modelBuilder.Entity("Infrastructure.Models.Home", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("char(36)");

                b.Property<string>("Apartment")
                    .HasMaxLength(32)
                    .HasColumnType("varchar(32)");

                b.Property<double>("Area")
                    .HasColumnType("double");

                b.Property<string>("Building")
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnType("varchar(96)");

                b.Property<string>("City")
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnType("varchar(96)");

                b.Property<string>("Country")
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnType("varchar(96)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime(6)");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("datetime(6)");

                b.Property<bool>("IsArchived")
                    .HasColumnType("tinyint(1)");

                b.Property<bool>("IsDefault")
                    .HasColumnType("tinyint(1)");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("datetime(6)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnType("varchar(32)");

                b.Property<string>("Region")
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnType("varchar(96)");

                b.Property<Guid>("Scope")
                    .HasColumnType("char(36)");

                b.Property<string>("Street")
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnType("varchar(96)");

                b.HasKey("Id");

                b.ToTable("Homes");
            });

        modelBuilder.Entity("Infrastructure.Models.Record", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("char(36)");

                b.Property<decimal>("AdditionalSum")
                    .HasColumnType("decimal(65,30)");

                b.Property<Guid>("CheckId")
                    .HasColumnType("char(36)");

                b.Property<decimal>("Cost")
                    .HasColumnType("decimal(65,30)");

                b.Property<decimal>("Difference")
                    .HasColumnType("decimal(65,30)");

                b.Property<bool>("IsArchived")
                    .HasColumnType("tinyint(1)");

                b.Property<decimal>("Measure")
                    .HasColumnType("decimal(65,30)");

                b.Property<decimal>("PreviousMeasure")
                    .HasColumnType("decimal(65,30)");

                b.Property<Guid>("TariffId")
                    .HasColumnType("char(36)");

                b.HasKey("Id");

                b.HasIndex("CheckId");

                b.HasIndex("TariffId");

                b.ToTable("Records");
            });

        modelBuilder.Entity("Infrastructure.Models.Tariff", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("char(36)");

                b.Property<decimal?>("AdditionalFeeCost")
                    .HasColumnType("decimal(65,30)");

                b.Property<string>("AdditionalFeeName")
                    .HasColumnType("longtext");

                b.Property<decimal>("Cost")
                    .HasColumnType("decimal(65,30)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime(6)");

                b.Property<DateTime?>("DeletedAt")
                    .HasColumnType("datetime(6)");

                b.Property<DateTime?>("EndDate")
                    .HasColumnType("datetime(6)");

                b.Property<decimal?>("FixedPay")
                    .HasColumnType("decimal(65,30)");

                b.Property<string>("FixedPayName")
                    .HasColumnType("longtext");

                b.Property<Guid>("HomeId")
                    .HasColumnType("char(36)");

                b.Property<bool>("IsArchived")
                    .HasColumnType("tinyint(1)");

                b.Property<DateTime?>("ModifiedAt")
                    .HasColumnType("datetime(6)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(96)
                    .HasColumnType("varchar(96)");

                b.Property<int>("Order")
                    .HasColumnType("int");

                b.Property<DateTime>("StartDate")
                    .HasColumnType("datetime(6)");

                b.Property<int>("TariffType")
                    .HasColumnType("int");

                b.Property<string>("Units")
                    .HasColumnType("longtext");

                b.Property<bool>("UseAdditionalFee")
                    .HasColumnType("tinyint(1)");

                b.Property<bool>("UseFixedPay")
                    .HasColumnType("tinyint(1)");

                b.Property<bool>("UseLimits")
                    .HasColumnType("tinyint(1)");

                b.HasKey("Id");

                b.HasIndex("HomeId");

                b.ToTable("Tariffs");
            });

        modelBuilder.Entity("Infrastructure.Models.TariffLimit", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("char(36)");

                b.Property<decimal>("CostAfterLimit")
                    .HasColumnType("decimal(65,30)");

                b.Property<bool>("IsArchived")
                    .HasColumnType("tinyint(1)");

                b.Property<decimal>("Limit")
                    .HasColumnType("decimal(65,30)");

                b.Property<Guid>("TariffId")
                    .HasColumnType("char(36)");

                b.HasKey("Id");

                b.HasIndex("TariffId", "Limit")
                    .IsUnique();

                b.ToTable("Limits");
            });

        modelBuilder.Entity("Infrastructure.Models.Check", b =>
            {
                b.HasOne("Infrastructure.Models.Home", "Home")
                    .WithMany("Checks")
                    .HasForeignKey("HomeId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Home");
            });

        modelBuilder.Entity("Infrastructure.Models.Record", b =>
            {
                b.HasOne("Infrastructure.Models.Check", "Check")
                    .WithMany("Records")
                    .HasForeignKey("CheckId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Infrastructure.Models.Tariff", "Tariff")
                    .WithMany()
                    .HasForeignKey("TariffId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Check");

                b.Navigation("Tariff");
            });

        modelBuilder.Entity("Infrastructure.Models.Tariff", b =>
            {
                b.HasOne("Infrastructure.Models.Home", "Home")
                    .WithMany("Tariffs")
                    .HasForeignKey("HomeId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Home");
            });

        modelBuilder.Entity("Infrastructure.Models.TariffLimit", b =>
            {
                b.HasOne("Infrastructure.Models.Tariff", "Tariff")
                    .WithMany("Limits")
                    .HasForeignKey("TariffId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Tariff");
            });

        modelBuilder.Entity("Infrastructure.Models.Check", b =>
            {
                b.Navigation("Records");
            });

        modelBuilder.Entity("Infrastructure.Models.Home", b =>
            {
                b.Navigation("Checks");

                b.Navigation("Tariffs");
            });

        modelBuilder.Entity("Infrastructure.Models.Tariff", b =>
            {
                b.Navigation("Limits");
            });
#pragma warning restore 612, 618
    }
}
