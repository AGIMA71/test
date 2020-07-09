﻿// <auto-generated />
using ABNLookup.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ABNLookup.Infrastructure.Migrations
{
    [DbContext(typeof(AbnContext))]
    partial class AbnContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("ABNLookup.Domain.Models.Abn", b =>
                {
                    b.Property<long>("ClientInternalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ABNidentifierValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("ACNidentifierValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("MainNameorganisationName")
                        .HasColumnType("TEXT");

                    b.HasKey("ClientInternalId");

                    b.HasIndex("ABNidentifierValue")
                        .IsUnique();

                    b.ToTable("Abn");
                });

            modelBuilder.Entity("ABNLookup.Domain.Models.MessageCode", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.ToTable("MessageCode");
                });
#pragma warning restore 612, 618
        }
    }
}