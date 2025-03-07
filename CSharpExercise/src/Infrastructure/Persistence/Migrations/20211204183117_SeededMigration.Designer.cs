﻿// <auto-generated />
using CSharpExercise.src.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CSharpExercise.src.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211204183117_SeededMigration")]
    partial class SeededMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CSharpExercise.src.Domain.Entities.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(320)
                        .HasColumnType("varchar(320)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Login")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("id");

                    b.ToTable("user_info", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "l.g@oplead.com",
                            FirstName = "Laurent",
                            LastName = "G.",
                            Login = "LGilly",
                            Password = "$2a$11$EyF9ThzYp2z4HPtf71/LVe2B0S9QCMCMSLdsNws6SsR8OQ2V7xOfS"
                        },
                        new
                        {
                            Id = 2,
                            Email = "maxime@andre.com",
                            FirstName = "Maxime",
                            LastName = "Andre",
                            Login = "MAndre",
                            Password = "$2a$11$XB2PBbfsLToRUBmcTvFZeekt42z8kNwJjpt0l495GTV0boe7cHoCS"
                        },
                        new
                        {
                            Id = 3,
                            Email = "emma@taume.com",
                            FirstName = "Emma",
                            LastName = "Taume",
                            Login = "ETaume",
                            Password = "$2a$11$huEOgHoMQVXPivLtJIsZ6eeCVW1EKZau9exMPPLcvLp5yacLi.1EC"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
