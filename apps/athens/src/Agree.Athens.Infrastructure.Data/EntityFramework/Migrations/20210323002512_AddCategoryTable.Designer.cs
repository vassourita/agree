﻿// <auto-generated />
using System;
using Agree.Athens.Infrastructure.Data.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210323002512_AddCategoryTable")]
    partial class AddCategoryTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Agree.Athens.Application.Security.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ExpiryOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime?>("RevokedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(128)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.CategoryDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("CategoryDbModel");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.MessageDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("character varying(800)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("TextChannelId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TextChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.RoleDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("CanAddUsers")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanDeleteServer")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanRemoveUsers")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanUpdateServerName")
                        .HasColumnType("boolean");

                    b.Property<string>("ColorHex")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character(6)")
                        .IsFixedLength(true);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.ServerDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.TextChannelDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("TextChannels");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.UserDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("EmailVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character(4)")
                        .IsFixedLength(true);

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleDbModelUserDbModel", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("ServerDbModelUserDbModel", b =>
                {
                    b.Property<Guid>("ServersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("ServersId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserServers");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.CategoryDbModel", b =>
                {
                    b.HasOne("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.ServerDbModel", "Server")
                        .WithMany("Categories")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.MessageDbModel", b =>
                {
                    b.HasOne("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.TextChannelDbModel", "Channel")
                        .WithMany("Messages")
                        .HasForeignKey("TextChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.UserDbModel", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.RoleDbModel", b =>
                {
                    b.HasOne("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.ServerDbModel", "Server")
                        .WithMany("Roles")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.TextChannelDbModel", b =>
                {
                    b.HasOne("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.CategoryDbModel", "Category")
                        .WithMany("TextChannels")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RoleDbModelUserDbModel", b =>
                {
                    b.HasOne("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.RoleDbModel", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.UserDbModel", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerDbModelUserDbModel", b =>
                {
                    b.HasOne("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.ServerDbModel", null)
                        .WithMany()
                        .HasForeignKey("ServersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.UserDbModel", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.CategoryDbModel", b =>
                {
                    b.Navigation("TextChannels");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.ServerDbModel", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.TextChannelDbModel", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Agree.Athens.Infrastructure.Data.EntityFramework.DataModels.UserDbModel", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
