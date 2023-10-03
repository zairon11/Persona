﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persona_API.Dats;

#nullable disable

namespace PersonaAPI.Migrations
{
    [DbContext(typeof(AplicationDbContex))]
    [Migration("20231003140923_AlimentarTablaPersona")]
    partial class AlimentarTablaPersona
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Persona_API.Models.Persona", b =>
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

                    b.Property<DateTime>("Registrationdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Personas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "DESD",
                            Name = "Natural",
                            Registrationdate = new DateTime(2023, 10, 3, 9, 9, 23, 29, DateTimeKind.Local).AddTicks(3563),
                            State = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "DEX",
                            Name = "Juridica",
                            Registrationdate = new DateTime(2023, 10, 3, 9, 9, 23, 29, DateTimeKind.Local).AddTicks(3576),
                            State = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}