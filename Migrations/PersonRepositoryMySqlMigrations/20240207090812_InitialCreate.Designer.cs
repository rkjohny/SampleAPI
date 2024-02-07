﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleAPI.Repository;

#nullable disable

namespace SampleAPI.Migrations.PersonRepositoryMySqlMigrations
{
    [DbContext(typeof(PersonRepositoryMySql))]
    [Migration("20240207090812_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SampleAPI.Models.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("varchar(35)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(35)
                        .HasColumnType("varchar(35)")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_updated_at");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("longblob")
                        .HasColumnName("row_version");

                    b.Property<long>("SyncVersion")
                        .HasColumnType("bigint")
                        .HasColumnName("sync_version");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("LastUpdatedAt");

                    b.ToTable("Person", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}