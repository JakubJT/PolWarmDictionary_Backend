﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(DictionaryContext))]
    partial class DictionaryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DAL.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("DAL.Models.PartOfSpeech", b =>
                {
                    b.Property<int>("PartOfSpeechId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PartOfSpeechId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PartOfSpeechId");

                    b.ToTable("PartOfSpeeches");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DAL.Models.Word", b =>
                {
                    b.Property<int>("WordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WordId"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("InPolish")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("InWarmian")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int?>("PartOfSpeechId")
                        .HasColumnType("int");

                    b.HasKey("WordId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PartOfSpeechId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("DAL.Models.WordGroup", b =>
                {
                    b.Property<int>("WordGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WordGroupId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WordGroupId");

                    b.ToTable("WordGroup", (string)null);
                });

            modelBuilder.Entity("UserWordGroup", b =>
                {
                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.Property<int>("WordGroupsWordGroupId")
                        .HasColumnType("int");

                    b.HasKey("UsersUserId", "WordGroupsWordGroupId");

                    b.HasIndex("WordGroupsWordGroupId");

                    b.ToTable("UserWordGroup");
                });

            modelBuilder.Entity("WordWordGroup", b =>
                {
                    b.Property<int>("WordGroupsWordGroupId")
                        .HasColumnType("int");

                    b.Property<int>("WordsWordId")
                        .HasColumnType("int");

                    b.HasKey("WordGroupsWordGroupId", "WordsWordId");

                    b.HasIndex("WordsWordId");

                    b.ToTable("WordWordGroup");
                });

            modelBuilder.Entity("DAL.Models.Word", b =>
                {
                    b.HasOne("DAL.Models.Author", "Author")
                        .WithMany("Words")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.PartOfSpeech", "PartOfSpeech")
                        .WithMany("Words")
                        .HasForeignKey("PartOfSpeechId");

                    b.Navigation("Author");

                    b.Navigation("PartOfSpeech");
                });

            modelBuilder.Entity("UserWordGroup", b =>
                {
                    b.HasOne("DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.WordGroup", null)
                        .WithMany()
                        .HasForeignKey("WordGroupsWordGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WordWordGroup", b =>
                {
                    b.HasOne("DAL.Models.WordGroup", null)
                        .WithMany()
                        .HasForeignKey("WordGroupsWordGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Word", null)
                        .WithMany()
                        .HasForeignKey("WordsWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.Author", b =>
                {
                    b.Navigation("Words");
                });

            modelBuilder.Entity("DAL.Models.PartOfSpeech", b =>
                {
                    b.Navigation("Words");
                });
#pragma warning restore 612, 618
        }
    }
}
