﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(AuthServerContext))]
    [Migration("20230515071011_Update Genre")]
    partial class UpdateGenre
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnimeDescriptionGenre", b =>
                {
                    b.Property<int>("AnimeDescriptionsId")
                        .HasColumnType("integer");

                    b.Property<int>("GenresId")
                        .HasColumnType("integer");

                    b.HasKey("AnimeDescriptionsId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("AnimeDescriptionGenre");
                });

            modelBuilder.Entity("Core.DB.Anime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PostedBy")
                        .HasColumnType("integer");

                    b.Property<float?>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("Tags")
                        .HasColumnType("text");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Animes");
                });

            modelBuilder.Entity("Core.DB.AnimeDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimeId")
                        .HasColumnType("integer");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Placement")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId");

                    b.HasIndex("LanguageId");

                    b.ToTable("AnimeDescription");
                });

            modelBuilder.Entity("Core.DB.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Core.DB.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Core.DB.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnimeId")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PhotoType")
                        .HasColumnType("integer");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Core.DB.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Roles")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AnimeDescriptionGenre", b =>
                {
                    b.HasOne("Core.DB.AnimeDescription", null)
                        .WithMany()
                        .HasForeignKey("AnimeDescriptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.DB.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.DB.AnimeDescription", b =>
                {
                    b.HasOne("Core.DB.Anime", null)
                        .WithMany("AnimeDescriptions")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.DB.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Core.DB.Photo", b =>
                {
                    b.HasOne("Core.DB.Anime", null)
                        .WithMany("Photos")
                        .HasForeignKey("AnimeId");
                });

            modelBuilder.Entity("Core.DB.Anime", b =>
                {
                    b.Navigation("AnimeDescriptions");

                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
