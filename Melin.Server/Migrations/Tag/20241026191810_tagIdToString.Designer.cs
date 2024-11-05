﻿// <auto-generated />
using System;
using Melin.Server.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Melin.Server.Migrations.Tag
{
    [DbContext(typeof(TagContext))]
    [Migration("20241026191810_tagIdToString")]
    partial class tagIdToString
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.1.24451.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GroupReference", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("integer");

                    b.Property<int>("ReferencesId")
                        .HasColumnType("integer");

                    b.HasKey("GroupsId", "ReferencesId");

                    b.HasIndex("ReferencesId");

                    b.ToTable("GroupReference");
                });

            modelBuilder.Entity("Melin.Server.Models.Creator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<int?>("ReferenceId")
                        .HasColumnType("integer");

                    b.Property<int>("Types")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReferenceId");

                    b.ToTable("Creator");
                });

            modelBuilder.Entity("Melin.Server.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("Melin.Server.Models.Reference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DatePublished")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string[]>("ExtraFields")
                        .HasColumnType("text[]");

                    b.Property<int?>("Language")
                        .HasColumnType("integer");

                    b.Property<string>("OwnerEmail")
                        .HasColumnType("text");

                    b.Property<string[]>("Rights")
                        .HasColumnType("text[]");

                    b.Property<string>("ShortTitle")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Type")
                        .HasMaxLength(256)
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Reference");
                });

            modelBuilder.Entity("Melin.Server.Models.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ReferenceTag", b =>
                {
                    b.Property<int>("ReferencesId")
                        .HasColumnType("integer");

                    b.Property<string>("TagsId")
                        .HasColumnType("text");

                    b.HasKey("ReferencesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ReferenceTag");
                });

            modelBuilder.Entity("GroupReference", b =>
                {
                    b.HasOne("Melin.Server.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Melin.Server.Models.Reference", null)
                        .WithMany()
                        .HasForeignKey("ReferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Melin.Server.Models.Creator", b =>
                {
                    b.HasOne("Melin.Server.Models.Reference", null)
                        .WithMany("Creators")
                        .HasForeignKey("ReferenceId");
                });

            modelBuilder.Entity("ReferenceTag", b =>
                {
                    b.HasOne("Melin.Server.Models.Reference", null)
                        .WithMany()
                        .HasForeignKey("ReferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Melin.Server.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Melin.Server.Models.Reference", b =>
                {
                    b.Navigation("Creators");
                });
#pragma warning restore 612, 618
        }
    }
}