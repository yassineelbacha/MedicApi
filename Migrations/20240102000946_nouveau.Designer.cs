﻿// <auto-generated />
using System;
using MedicApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicApi.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20240102000946_nouveau")]
    partial class nouveau
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MedicApi.Models.Appoin", b =>
                {
                    b.Property<int>("Rid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Rid"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Heure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonneId")
                        .HasColumnType("int");

                    b.Property<bool>("Urgence")
                        .HasColumnType("bit");

                    b.HasKey("Rid");

                    b.HasIndex("PersonneId");

                    b.ToTable("Appoins");
                });

            modelBuilder.Entity("MedicApi.Models.DossierMedical", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Certificats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Conclusion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descriptions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Medicaments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonneId");

                    b.ToTable("DossierMedicals");
                });

            modelBuilder.Entity("MedicApi.Models.Personne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateNaissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Personnes");
                });

            modelBuilder.Entity("MedicApi.Models.Travail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Conge")
                        .HasColumnType("bit");

                    b.Property<string>("Jours")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonneId")
                        .HasColumnType("int");

                    b.Property<int>("TrHeure")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonneId");

                    b.ToTable("Travails");
                });

            modelBuilder.Entity("MedicApi.Models.Appoin", b =>
                {
                    b.HasOne("MedicApi.Models.Personne", "Personne")
                        .WithMany()
                        .HasForeignKey("PersonneId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Personne");
                });

            modelBuilder.Entity("MedicApi.Models.DossierMedical", b =>
                {
                    b.HasOne("MedicApi.Models.Personne", "Personne")
                        .WithMany()
                        .HasForeignKey("PersonneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personne");
                });

            modelBuilder.Entity("MedicApi.Models.Travail", b =>
                {
                    b.HasOne("MedicApi.Models.Personne", "Personne")
                        .WithMany()
                        .HasForeignKey("PersonneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personne");
                });
#pragma warning restore 612, 618
        }
    }
}
