﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkTracker.Gateways.MySQL.Contexts;

#nullable disable

namespace WorkTracker.Gateways.MySQL.Migrations
{
    [DbContext(typeof(ClockContext))]
    partial class ClockContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Clock")
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WorkTracker.Clock.Domain.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Email");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Hash");

                    b.HasKey("Id");

                    b.ToTable("Employee", "Clock");
                });

            modelBuilder.Entity("WorkTracker.Clock.Domain.Models.Punch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<string>("EmployeeHash")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("EmployeeHash");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsApproved");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("Timestamp");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Type");

                    b.Property<DateTime?>("UpdatedTimestamp")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("UpdatedTimestamp");

                    b.HasKey("Id");

                    b.ToTable("Punch", "Clock");
                });
#pragma warning restore 612, 618
        }
    }
}
