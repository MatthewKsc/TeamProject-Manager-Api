﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamProject_Manager_Api.dao;

namespace TeamProject_Manager_Api.Migrations
{
    [DbContext(typeof(ProjectManagerDbContext))]
    [Migration("20210422101447_ProjectTableConfiguration")]
    partial class ProjectTableConfiguration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(85)
                        .HasColumnType("nvarchar(85)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SizeOfComapny")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndOfProject")
                        .HasColumnType("datetime2");

                    b.Property<int>("OwnerTeamId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartOfProject")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerTeamId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("NameOfTeam")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.Company", b =>
                {
                    b.HasOne("TeamProject_Manager_Api.dao.Entitys.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.Project", b =>
                {
                    b.HasOne("TeamProject_Manager_Api.dao.Entitys.Team", "OwnerTeam")
                        .WithMany("Projects")
                        .HasForeignKey("OwnerTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerTeam");
                });

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.Team", b =>
                {
                    b.HasOne("TeamProject_Manager_Api.dao.Entitys.Company", "Company")
                        .WithMany("Teams")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.User", b =>
                {
                    b.HasOne("TeamProject_Manager_Api.dao.Entitys.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamProject_Manager_Api.dao.Entitys.Project", null)
                        .WithMany("UsersAssigned")
                        .HasForeignKey("ProjectId");

                    b.HasOne("TeamProject_Manager_Api.dao.Entitys.Team", "Team")
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.Company", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.Project", b =>
                {
                    b.Navigation("UsersAssigned");
                });

            modelBuilder.Entity("TeamProject_Manager_Api.dao.Entitys.Team", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("TeamMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
