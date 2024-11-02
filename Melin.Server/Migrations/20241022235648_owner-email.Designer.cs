﻿// <auto-generated />
using System;
using Melin.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Melin.Server.Migrations
{
    [DbContext(typeof(ReferenceContext))]
    [Migration("20241022235648_owner-email")]
    partial class owneremail
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.1.24451.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.HasKey("Id");

                    b.HasIndex("ReferenceId");

                    b.ToTable("Creator");
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

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

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

                    b.HasDiscriminator().HasValue("Reference");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Melin.Server.Models.Artwork", b =>
                {
                    b.HasBaseType("Melin.Server.Models.Reference");

                    b.Property<string>("Dimensions")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("MapType")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Medium")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Scale")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.ToTable("Reference", t =>
                        {
                            t.Property("Medium")
                                .HasColumnName("Artwork_Medium");
                        });

                    b.HasDiscriminator().HasValue("Artwork");
                });

            modelBuilder.Entity("Melin.Server.Models.Book", b =>
                {
                    b.HasBaseType("Melin.Server.Models.Reference");

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Edition")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ISSN")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Issue")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("JournalAbbr")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("PageAmount")
                        .HasColumnType("integer");

                    b.Property<int>("Pages")
                        .HasColumnType("integer");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Publication")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Section")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Series")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("SeriesNumber")
                        .HasColumnType("integer");

                    b.Property<string>("SeriesTitle")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Volume")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("VolumeAmount")
                        .HasColumnType("integer");

                    b.ToTable("Reference", t =>
                        {
                            t.Property("Place")
                                .HasColumnName("Book_Place");

                            t.Property("Section")
                                .HasColumnName("Book_Section");
                        });

                    b.HasDiscriminator().HasValue("Book");
                });

            modelBuilder.Entity("Melin.Server.Models.LegalCases", b =>
                {
                    b.HasBaseType("Melin.Server.Models.Reference");

                    b.Property<string>("CaseName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Court")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime?>("DateDecided")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DocketNumber")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("FirstPage")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("History")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Reporter")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ReporterVolume")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.ToTable("Reference", t =>
                        {
                            t.Property("History")
                                .HasColumnName("LegalCases_History");
                        });

                    b.HasDiscriminator().HasValue("LegalCases");
                });

            modelBuilder.Entity("Melin.Server.Models.Legislation", b =>
                {
                    b.HasBaseType("Melin.Server.Models.Reference");

                    b.Property<string>("BillNumber")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("CodeNumber")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("CodePages")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("CodeVolume")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Committee")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime?>("DateEnacted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DocumentNumber")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("History")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("LegislativeBody")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NameOfAct")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PublicLawNumber")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Section")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Session")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue("Legislation");
                });

            modelBuilder.Entity("Melin.Server.Models.Patent", b =>
                {
                    b.HasBaseType("Melin.Server.Models.Reference");

                    b.Property<string>("ApplicationNumber")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Assignee")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime?>("FilingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("IssueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IssuingAuthority")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("LegalStatus")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PatentNumber")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PriorityNumber")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string[]>("References")
                        .HasColumnType("text[]");

                    b.HasDiscriminator().HasValue("Patent");
                });

            modelBuilder.Entity("Melin.Server.Models.Presentation", b =>
                {
                    b.HasBaseType("Melin.Server.Models.Reference");

                    b.Property<string>("ConferenceName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PresentationType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ProceedingTitle")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue("Presentation");
                });

            modelBuilder.Entity("Melin.Server.Models.PrimarySource", b =>
                {
                    b.HasBaseType("Melin.Server.Models.Reference");

                    b.Property<string>("Medium")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PrimarySourceType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue("PrimarySource");
                });

            modelBuilder.Entity("Melin.Server.Models.Recording", b =>
                {
                    b.HasBaseType("Melin.Server.Models.Reference");

                    b.Property<string>("Distributor")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int?>("EpisodeNumber")
                        .HasColumnType("integer");

                    b.Property<string>("FileFormat")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Network")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ProgramTitle")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("RunningTime")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Studio")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue("Recording");
                });

            modelBuilder.Entity("Melin.Server.Models.Report", b =>
                {
                    b.HasBaseType("Melin.Server.Models.Reference");

                    b.Property<string>("Institution")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("ReportNumber")
                        .HasColumnType("integer");

                    b.Property<string>("ReportType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue("Report");
                });

            modelBuilder.Entity("Melin.Server.Models.Software", b =>
                {
                    b.HasBaseType("Melin.Server.Models.Reference");

                    b.Property<string>("Company")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("ProgrammingLanguage")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("System")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue("Software");
                });

            modelBuilder.Entity("Melin.Server.Models.Website", b =>
                {
                    b.HasBaseType("Melin.Server.Models.Reference");

                    b.Property<string>("ForumTitle")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PostType")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("WebsiteTitle")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("WebsiteType")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasDiscriminator().HasValue("Website");
                });

            modelBuilder.Entity("Melin.Server.Models.Creator", b =>
                {
                    b.HasOne("Melin.Server.Models.Reference", null)
                        .WithMany("Creators")
                        .HasForeignKey("ReferenceId");
                });

            modelBuilder.Entity("Melin.Server.Models.Reference", b =>
                {
                    b.Navigation("Creators");
                });
#pragma warning restore 612, 618
        }
    }
}
