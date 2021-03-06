// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniBank.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiniBank.Data.Migrations
{
    [DbContext(typeof(MiniBankDbContext))]
    [Migration("20220409112945_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MiniBank.Data.Accounts.AccountDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("ClosingDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("closing_date");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("currency");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<DateTime>("OpeningDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("opening_date");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric")
                        .HasColumnName("sum");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_account");

                    b.ToTable("account", (string)null);
                });

            modelBuilder.Entity("MiniBank.Data.RemittanceHistories.RemittanceHistoryDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("currency");

                    b.Property<Guid>("FromAccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("from_account_id");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric")
                        .HasColumnName("sum");

                    b.Property<Guid>("ToAccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("to_account_id");

                    b.HasKey("Id")
                        .HasName("pk_remittance_history");

                    b.ToTable("remittanceHistory", (string)null);
                });

            modelBuilder.Entity("MiniBank.Data.Users.UserDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.ToTable("user", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
