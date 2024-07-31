﻿// <auto-generated />
using System;
using FinalProject.Infraestructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinalProject.Infraestructure.Persistance.Migrations
{
    [DbContext(typeof(FinalProjectContext))]
    partial class FinalProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.FavoriteUserProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("PropertyId"), false);

                    b.ToTable("FavoriteUserProperties");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.Perk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Perks");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.Property", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AgentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AmountOfBathrooms")
                        .HasColumnType("int");

                    b.Property<int>("AmountOfBedrooms")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("PropertyPrice")
                        .HasColumnType("Decimal(18,2)");

                    b.Property<int>("PropertyTypeId")
                        .HasColumnType("int");

                    b.Property<int>("SellTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("SizeInMeters")
                        .HasColumnType("Decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PropertyCode");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("PropertyCode"), false);

                    b.HasIndex("PropertyTypeId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("PropertyTypeId"), false);

                    b.HasIndex("SellTypeId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("SellTypeId"), false);

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.PropertyImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("PropertyId"), false);

                    b.ToTable("PropertyImages");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.PropertyPerk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PerkId")
                        .HasColumnType("int");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PerkId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("PerkId"), false);

                    b.HasIndex("PropertyId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("PropertyId"), false);

                    b.ToTable("PropertyPerks");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.PropertyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PropertyTypes");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.SellType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SellTypes");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.FavoriteUserProperty", b =>
                {
                    b.HasOne("FinalProject.Core.Domain.Entities.Property", "Property")
                        .WithMany("FavoriteUsersProperties")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.Property", b =>
                {
                    b.HasOne("FinalProject.Core.Domain.Entities.PropertyType", "PropertyType")
                        .WithMany("Properties")
                        .HasForeignKey("PropertyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProject.Core.Domain.Entities.SellType", "SellType")
                        .WithMany("Properties")
                        .HasForeignKey("SellTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PropertyType");

                    b.Navigation("SellType");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.PropertyImage", b =>
                {
                    b.HasOne("FinalProject.Core.Domain.Entities.Property", "Property")
                        .WithMany("PropertyImages")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.PropertyPerk", b =>
                {
                    b.HasOne("FinalProject.Core.Domain.Entities.Perk", "Perk")
                        .WithMany("PropertyPerks")
                        .HasForeignKey("PerkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProject.Core.Domain.Entities.Property", "Property")
                        .WithMany("PropertyPerks")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Perk");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.Perk", b =>
                {
                    b.Navigation("PropertyPerks");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.Property", b =>
                {
                    b.Navigation("FavoriteUsersProperties");

                    b.Navigation("PropertyImages");

                    b.Navigation("PropertyPerks");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.PropertyType", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("FinalProject.Core.Domain.Entities.SellType", b =>
                {
                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}
