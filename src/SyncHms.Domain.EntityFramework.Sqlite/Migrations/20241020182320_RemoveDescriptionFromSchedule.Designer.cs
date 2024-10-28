﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SyncHms.Domain.EntityFramework.Sqlite.Infrastructure;

#nullable disable

namespace SyncHms.Domain.EntityFramework.Sqlite.Migrations
{
    [DbContext(typeof(SqliteDomainContext))]
    [Migration("20241020182320_RemoveDescriptionFromSchedule")]
    partial class RemoveDescriptionFromSchedule
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

            modelBuilder.Entity("SyncHms.Domain.EntityFramework.Models.AppOptions", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AppOptions");
                });

            modelBuilder.Entity("SyncHms.Domain.Log", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("HandlerName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEnd")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsError")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LogDataId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<string>("TaskId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TaskName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LogDataId")
                        .IsUnique();

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("SyncHms.Domain.LogData", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Error")
                        .HasColumnType("TEXT");

                    b.Property<string>("InputObjectJson")
                        .HasColumnType("TEXT");

                    b.Property<string>("LogId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StackTrace")
                        .HasColumnType("TEXT");

                    b.Property<string>("TaskId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LogDatas");
                });

            modelBuilder.Entity("SyncHms.Domain.Schedule", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("IntervalSeconds")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Last")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("SyncHms.Domain.Service", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("JsonOptions")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("SyncHms.Domain.Log", b =>
                {
                    b.HasOne("SyncHms.Domain.LogData", "LogData")
                        .WithOne("Log")
                        .HasForeignKey("SyncHms.Domain.Log", "LogDataId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("LogData");
                });

            modelBuilder.Entity("SyncHms.Domain.LogData", b =>
                {
                    b.Navigation("Log")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
