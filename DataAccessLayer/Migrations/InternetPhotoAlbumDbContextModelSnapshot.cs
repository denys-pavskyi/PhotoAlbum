﻿// <auto-generated />
using System;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(InternetPhotoAlbumDbContext))]
    partial class InternetPhotoAlbumDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Entities.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfPictures")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.AlbumPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AdditionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("PhotoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("PhotoId");

                    b.ToTable("AlbumPhotos");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<double>("TotalRating")
                        .HasColumnType("float");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PhotoRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Grade")
                        .HasColumnType("float");

                    b.Property<int>("PhotoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RatingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.HasIndex("UserId");

                    b.ToTable("PhotosRating");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PhotoTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PhotoId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.ToTable("PhotoTags");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Album", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.User", "User")
                        .WithMany("Albums")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.AlbumPhoto", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Album", "Album")
                        .WithMany("AlbumPhotos")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entities.Photo", "Photo")
                        .WithMany("AlbumPhotos")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Photo", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PhotoRating", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Photo", "Photo")
                        .WithMany("PhotoRatings")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entities.User", "User")
                        .WithMany("PhotoRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.PhotoTag", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Photo", "Photo")
                        .WithMany("PhotoTags")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entities.Tag", "Tag")
                        .WithMany("PhotoTags")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Photo");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Album", b =>
                {
                    b.Navigation("AlbumPhotos");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Photo", b =>
                {
                    b.Navigation("AlbumPhotos");

                    b.Navigation("PhotoRatings");

                    b.Navigation("PhotoTags");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Tag", b =>
                {
                    b.Navigation("PhotoTags");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.User", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("PhotoRatings");

                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}