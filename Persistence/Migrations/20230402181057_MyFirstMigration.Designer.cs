﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.Contexts;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230402181057_MyFirstMigration")]
    partial class MyFirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<string>("Addres")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("Birthday")
                        .HasMaxLength(80)
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateUser")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("DocumentComplement")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifyUser")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("SecondLastName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("ClientId");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateUser")
                        .HasColumnType("text");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifyUser")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Client", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithOne("Client")
                        .HasForeignKey("Domain.Entities.Client", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Client");
                });
#pragma warning restore 612, 618
        }
    }
}
