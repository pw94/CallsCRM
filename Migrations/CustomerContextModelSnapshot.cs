﻿// <auto-generated />
using CallsCRM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CallsCRM.Migrations
{
    [DbContext(typeof(CustomerContext))]
    partial class CustomerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CallsCRM.Models.Call", b =>
                {
                    b.Property<int>("CallId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CallerId");

                    b.Property<int>("CustomerId");

                    b.HasKey("CallId");

                    b.HasIndex("CallerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Calls");
                });

            modelBuilder.Entity("CallsCRM.Models.Caller", b =>
                {
                    b.Property<int>("CallerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LastName");

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("CallerId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Callers");
                });

            modelBuilder.Entity("CallsCRM.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("CustomerId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CallsCRM.Models.Call", b =>
                {
                    b.HasOne("CallsCRM.Models.Caller", "Caller")
                        .WithMany("Calls")
                        .HasForeignKey("CallerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CallsCRM.Models.Customer", "Callee")
                        .WithMany("Calls")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("CallsCRM.Models.CallTime", "Time", b1 =>
                        {
                            b1.Property<int>("CallId");

                            b1.Property<DateTime>("EndTime");

                            b1.Property<DateTime>("StartTime");

                            b1.ToTable("Calls");

                            b1.HasOne("CallsCRM.Models.Call")
                                .WithOne("Time")
                                .HasForeignKey("CallsCRM.Models.CallTime", "CallId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
