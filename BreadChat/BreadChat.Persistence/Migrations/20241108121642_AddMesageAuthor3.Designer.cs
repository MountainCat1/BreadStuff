﻿// <auto-generated />
using System;
using BreadChat.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BreadChat.Persistence.Migrations
{
    [DbContext(typeof(BreadChatDbContext))]
    [Migration("20241108121642_AddMesageAuthor3")]
    partial class AddMesageAuthor3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("BreadChat.Domain.Entities.Channel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Channels", (string)null);
                });

            modelBuilder.Entity("BreadChat.Domain.Entities.ChannelMembership", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ChannelId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "ChannelId");

                    b.HasIndex("ChannelId");

                    b.ToTable("ChannelMemberships", (string)null);
                });

            modelBuilder.Entity("BreadChat.Domain.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ChannelId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ChannelId");

                    b.ToTable("Messages", (string)null);
                });

            modelBuilder.Entity("BreadChat.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("BreadChat.Domain.Entities.ChannelMembership", b =>
                {
                    b.HasOne("BreadChat.Domain.Entities.Channel", "Channel")
                        .WithMany("Members")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreadChat.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BreadChat.Domain.Entities.Message", b =>
                {
                    b.HasOne("BreadChat.Domain.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreadChat.Domain.Entities.Channel", "Channel")
                        .WithMany("Messages")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Channel");
                });

            modelBuilder.Entity("BreadChat.Domain.Entities.Channel", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
