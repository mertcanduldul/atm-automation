﻿// <auto-generated />
using Automation.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Automation.Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Automation.Core.Entities.Money", b =>
                {
                    b.Property<int>("ID_MONEY")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_MONEY"), 1L, 1);

                    b.Property<int>("ID_TAPE")
                        .HasColumnType("int");

                    b.Property<string>("MONEY_NAME")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MONEY_TYPE_ID")
                        .HasColumnType("int");

                    b.Property<int>("MONEY_VALUE")
                        .HasColumnType("int");

                    b.HasKey("ID_MONEY");

                    b.ToTable("Money", (string)null);
                });

            modelBuilder.Entity("Automation.Core.Entities.Tape", b =>
                {
                    b.Property<int>("ID_TAPE")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_TAPE"), 1L, 1);

                    b.Property<int>("TAPE_MONEY_TYPE_ID")
                        .HasColumnType("int");

                    b.Property<int>("TAPE_STATE_TYPE_ID")
                        .HasColumnType("int");

                    b.HasKey("ID_TAPE");

                    b.ToTable("Tape", (string)null);

                    b.HasData(
                        new
                        {
                            ID_TAPE = 1,
                            TAPE_MONEY_TYPE_ID = 4,
                            TAPE_STATE_TYPE_ID = 1
                        },
                        new
                        {
                            ID_TAPE = 2,
                            TAPE_MONEY_TYPE_ID = 1,
                            TAPE_STATE_TYPE_ID = 3
                        },
                        new
                        {
                            ID_TAPE = 3,
                            TAPE_MONEY_TYPE_ID = 2,
                            TAPE_STATE_TYPE_ID = 3
                        },
                        new
                        {
                            ID_TAPE = 4,
                            TAPE_MONEY_TYPE_ID = 3,
                            TAPE_STATE_TYPE_ID = 3
                        },
                        new
                        {
                            ID_TAPE = 5,
                            TAPE_MONEY_TYPE_ID = 4,
                            TAPE_STATE_TYPE_ID = 3
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
