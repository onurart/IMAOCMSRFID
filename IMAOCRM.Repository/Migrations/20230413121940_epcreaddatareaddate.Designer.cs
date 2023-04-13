﻿// <auto-generated />
using System;
using IMAOCRM.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IMAOCRM.Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230413121940_epcreaddatareaddate")]
    partial class epcreaddatareaddate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IMAOCMS.Core.Entites.EPCReadTemp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte>("Ant")
                        .HasColumnType("tinyint");

                    b.Property<string>("Epc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EpcDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rssi")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("EPCReadTemps");
                });

            modelBuilder.Entity("IMAOCMS.Core.Entites.EpcReadData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte>("Ant")
                        .HasColumnType("tinyint");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterId")
                        .HasColumnType("int");

                    b.Property<string>("Epc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EpcDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("Rssi")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdaterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("DeleterId");

                    b.HasIndex("UpdaterId");

                    b.ToTable("EpcReadDatas");
                });

            modelBuilder.Entity("IMAOCMS.Core.Entites.RFIDDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Baud")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("Portnum")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdaterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("DeleterId");

                    b.HasIndex("UpdaterId");

                    b.ToTable("RFIDDevices");
                });

            modelBuilder.Entity("IMAOCMS.Core.Entites.RFIDDeviceAntenna", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Antenna")
                        .HasColumnType("int");

                    b.Property<int>("AntennaPower")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("RFIDDeviceId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdaterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("DeleterId");

                    b.HasIndex("RFIDDeviceId");

                    b.HasIndex("UpdaterId");

                    b.ToTable("RFIDDeviceAntennas");
                });

            modelBuilder.Entity("IMAOCMS.Core.Entites.RelayCardDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Baud")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Portnum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdaterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("DeleterId");

                    b.HasIndex("UpdaterId");

                    b.ToTable("RelayCardDevices");
                });

            modelBuilder.Entity("IMAOCMS.Core.Entites.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IMAOCMS.Core.Entites.EpcReadData", b =>
                {
                    b.HasOne("IMAOCMS.Core.Entites.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("IMAOCMS.Core.Entites.User", "Deleter")
                        .WithMany()
                        .HasForeignKey("DeleterId");

                    b.HasOne("IMAOCMS.Core.Entites.User", "Updater")
                        .WithMany()
                        .HasForeignKey("UpdaterId");

                    b.Navigation("Creator");

                    b.Navigation("Deleter");

                    b.Navigation("Updater");
                });

            modelBuilder.Entity("IMAOCMS.Core.Entites.RFIDDevice", b =>
                {
                    b.HasOne("IMAOCMS.Core.Entites.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("IMAOCMS.Core.Entites.User", "Deleter")
                        .WithMany()
                        .HasForeignKey("DeleterId");

                    b.HasOne("IMAOCMS.Core.Entites.User", "Updater")
                        .WithMany()
                        .HasForeignKey("UpdaterId");

                    b.Navigation("Creator");

                    b.Navigation("Deleter");

                    b.Navigation("Updater");
                });

            modelBuilder.Entity("IMAOCMS.Core.Entites.RFIDDeviceAntenna", b =>
                {
                    b.HasOne("IMAOCMS.Core.Entites.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("IMAOCMS.Core.Entites.User", "Deleter")
                        .WithMany()
                        .HasForeignKey("DeleterId");

                    b.HasOne("IMAOCMS.Core.Entites.RFIDDevice", "Fiche")
                        .WithMany()
                        .HasForeignKey("RFIDDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMAOCMS.Core.Entites.User", "Updater")
                        .WithMany()
                        .HasForeignKey("UpdaterId");

                    b.Navigation("Creator");

                    b.Navigation("Deleter");

                    b.Navigation("Fiche");

                    b.Navigation("Updater");
                });

            modelBuilder.Entity("IMAOCMS.Core.Entites.RelayCardDevice", b =>
                {
                    b.HasOne("IMAOCMS.Core.Entites.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("IMAOCMS.Core.Entites.User", "Deleter")
                        .WithMany()
                        .HasForeignKey("DeleterId");

                    b.HasOne("IMAOCMS.Core.Entites.User", "Updater")
                        .WithMany()
                        .HasForeignKey("UpdaterId");

                    b.Navigation("Creator");

                    b.Navigation("Deleter");

                    b.Navigation("Updater");
                });
#pragma warning restore 612, 618
        }
    }
}