﻿// <auto-generated />
using System;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Channel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChannelId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("CanCreateNewRoles")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanDeleteRoles")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanDeleteServer")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanRemoveUsers")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanUpdateServerAvatar")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanUpdateServerDescription")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanUpdateServerName")
                        .HasColumnType("boolean");

                    b.Property<string>("ColorHex")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character(6)")
                        .IsFixedLength(true);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Server", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarFileName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.ServerUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.HasIndex("UserId");

                    b.ToTable("ServerUsers");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.ServerUserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServerUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("ServerUserId");

                    b.ToTable("ServerUserRole");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarFileName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Status")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character(4)")
                        .IsFixedLength(true);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Category", b =>
                {
                    b.HasOne("Agree.Athens.Domain.Entities.Server", "Server")
                        .WithMany("Categories")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Channel", b =>
                {
                    b.HasOne("Agree.Athens.Domain.Entities.Category", "Category")
                        .WithMany("Channels")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Message", b =>
                {
                    b.HasOne("Agree.Athens.Domain.Entities.Channel", "Channel")
                        .WithMany("Messages")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Agree.Athens.Domain.Entities.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Role", b =>
                {
                    b.HasOne("Agree.Athens.Domain.Entities.Server", "Server")
                        .WithMany("Roles")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.ServerUser", b =>
                {
                    b.HasOne("Agree.Athens.Domain.Entities.Server", "Server")
                        .WithMany("ServerUsers")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Agree.Athens.Domain.Entities.User", "User")
                        .WithMany("UserServers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.ServerUserRole", b =>
                {
                    b.HasOne("Agree.Athens.Domain.Entities.Role", "Role")
                        .WithMany("ServerUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Agree.Athens.Domain.Entities.ServerUser", "ServerUser")
                        .WithMany("ServerUserRoles")
                        .HasForeignKey("ServerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("ServerUser");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Category", b =>
                {
                    b.Navigation("Channels");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Channel", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Role", b =>
                {
                    b.Navigation("ServerUserRoles");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.Server", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Roles");

                    b.Navigation("ServerUsers");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.ServerUser", b =>
                {
                    b.Navigation("ServerUserRoles");
                });

            modelBuilder.Entity("Agree.Athens.Domain.Entities.User", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserServers");
                });
#pragma warning restore 612, 618
        }
    }
}
