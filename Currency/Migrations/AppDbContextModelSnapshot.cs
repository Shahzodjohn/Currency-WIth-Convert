﻿// <auto-generated />
using System;
using Currency.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Currency.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Currency.ViewModels.ValCurs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Valcures");
                });

            modelBuilder.Entity("Currency.ViewModels.Valute", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CharCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nominal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ValCursId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ValCursId");

                    b.ToTable("Valutes");
                });

            modelBuilder.Entity("Currency.ViewModels.Valute", b =>
                {
                    b.HasOne("Currency.ViewModels.ValCurs", "ValCurs")
                        .WithMany("Valute")
                        .HasForeignKey("ValCursId");

                    b.Navigation("ValCurs");
                });

            modelBuilder.Entity("Currency.ViewModels.ValCurs", b =>
                {
                    b.Navigation("Valute");
                });
#pragma warning restore 612, 618
        }
    }
}
