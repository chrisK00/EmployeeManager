﻿// <auto-generated />
using System;
using EmployeeManager.DataRepository.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManager.DataRepository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210112175848_hi")]
    partial class hi
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EmployeeManager.DataRepository.Employees.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Salary")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeeManager.DataRepository.Employees.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("BaseSalary")
                        .HasColumnType("TEXT");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("EmployeeManager.DataRepository.Employees.Employee", b =>
                {
                    b.HasOne("EmployeeManager.DataRepository.Employees.Employee", null)
                        .WithMany("ReportsTo")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("EmployeeManager.DataRepository.Employees.Role", b =>
                {
                    b.HasOne("EmployeeManager.DataRepository.Employees.Employee", null)
                        .WithMany("Roles")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("EmployeeManager.DataRepository.Employees.Employee", b =>
                {
                    b.Navigation("ReportsTo");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
