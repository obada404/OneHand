﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OneHandTraining.Models;

#nullable disable

namespace OneHandTraining.Data
{
    [DbContext(typeof(oneHandContext))]
    [Migration("20220926151312_addCreatedon")]
    partial class addCreatedon
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("OneHandTraining.model.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("authorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("favorited")
                        .HasColumnType("INTEGER");

                    b.Property<int>("favoritesCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("slug")
                        .HasColumnType("TEXT");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("authorId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("OneHandTraining.model.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ArticleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("authorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("authorId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OneHandTraining.model.UserOld", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserOldDBs");
                });

            modelBuilder.Entity("UserOldUserOld", b =>
                {
                    b.Property<int>("followersId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("followingId")
                        .HasColumnType("INTEGER");

                    b.HasKey("followersId", "followingId");

                    b.HasIndex("followingId");

                    b.ToTable("UserOldUserOld");
                });

            modelBuilder.Entity("OneHandTraining.model.Article", b =>
                {
                    b.HasOne("OneHandTraining.model.UserOld", "author")
                        .WithMany()
                        .HasForeignKey("authorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("author");
                });

            modelBuilder.Entity("OneHandTraining.model.Comment", b =>
                {
                    b.HasOne("OneHandTraining.model.Article", null)
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId");

                    b.HasOne("OneHandTraining.model.UserOld", "author")
                        .WithMany()
                        .HasForeignKey("authorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("author");
                });

            modelBuilder.Entity("UserOldUserOld", b =>
                {
                    b.HasOne("OneHandTraining.model.UserOld", null)
                        .WithMany()
                        .HasForeignKey("followersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OneHandTraining.model.UserOld", null)
                        .WithMany()
                        .HasForeignKey("followingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OneHandTraining.model.Article", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
