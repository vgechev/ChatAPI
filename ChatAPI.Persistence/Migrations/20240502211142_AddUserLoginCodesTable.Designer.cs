﻿// <auto-generated />
using System;
using ChatAPI.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatAPI.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240502211142_AddUserLoginCodesTable")]
    partial class AddUserLoginCodesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChatAPI.Domain.Entities.Friendship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SenderUserId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TargetUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SenderUserId");

                    b.HasIndex("TargetUserId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("ChatAPI.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ChatAPI.Domain.Entities.UserDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserDevices", (string)null);
                });

            modelBuilder.Entity("ChatAPI.Domain.Entities.UserLoginCode", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<Guid>("SecretLoginCode")
                        .HasMaxLength(36)
                        .HasColumnType("uuid");

                    b.HasKey("UserId");

                    b.ToTable("UserLoginCodes", (string)null);
                });

            modelBuilder.Entity("ChatAPI.Domain.Entities.Friendship", b =>
                {
                    b.HasOne("ChatAPI.Domain.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatAPI.Domain.Entities.User", "Target")
                        .WithMany()
                        .HasForeignKey("TargetUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("ChatAPI.Domain.Entities.UserDevice", b =>
                {
                    b.HasOne("ChatAPI.Domain.Entities.User", "User")
                        .WithMany("UserDevices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserDevices_UserId_Users_Id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatAPI.Domain.Entities.UserLoginCode", b =>
                {
                    b.HasOne("ChatAPI.Domain.Entities.User", "User")
                        .WithOne("UserLoginCode")
                        .HasForeignKey("ChatAPI.Domain.Entities.UserLoginCode", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserLoginCodes_UserId_Users_Id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatAPI.Domain.Entities.User", b =>
                {
                    b.Navigation("UserDevices");

                    b.Navigation("UserLoginCode");
                });
#pragma warning restore 612, 618
        }
    }
}
