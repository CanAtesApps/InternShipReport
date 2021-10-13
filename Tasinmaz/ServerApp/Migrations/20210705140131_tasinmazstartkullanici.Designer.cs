﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServerApp.Data;

namespace ServerApp.Migrations
{
    [DbContext(typeof(TasinmazContext))]
    [Migration("20210705140131_tasinmazstartkullanici")]
    partial class tasinmazstartkullanici
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ServerApp.Models.Il", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CityName")
                        .HasColumnType("text");

                    b.Property<int>("CountryID")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneCode")
                        .HasColumnType("text");

                    b.Property<string>("PlateNo")
                        .HasColumnType("text");

                    b.HasKey("CityID");

                    b.ToTable("Il");
                });

            modelBuilder.Entity("ServerApp.Models.Ilce", b =>
                {
                    b.Property<int>("CountyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CityID")
                        .HasColumnType("integer");

                    b.Property<string>("CountyName")
                        .HasColumnType("text");

                    b.HasKey("CountyID");

                    b.ToTable("Ilce");
                });

            modelBuilder.Entity("ServerApp.Models.Kullanici", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Ad")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<bool>("Rol")
                        .HasColumnType("boolean");

                    b.Property<string>("Soyad")
                        .HasColumnType("text");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("UserID");

                    b.ToTable("Kullanici");
                });

            modelBuilder.Entity("ServerApp.Models.Mahalle", b =>
                {
                    b.Property<int>("AreaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AreaName")
                        .HasColumnType("text");

                    b.Property<int>("CountyID")
                        .HasColumnType("integer");

                    b.HasKey("AreaID");

                    b.ToTable("Mahalle");
                });

            modelBuilder.Entity("ServerApp.Models.Tasinmaz", b =>
                {
                    b.Property<int>("TasinmazID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Ada")
                        .HasColumnType("integer");

                    b.Property<string>("Adres")
                        .HasColumnType("text");

                    b.Property<int>("CityID")
                        .HasColumnType("integer");

                    b.Property<int>("CountyID")
                        .HasColumnType("integer");

                    b.Property<int>("MahalleID")
                        .HasColumnType("integer");

                    b.Property<string>("Nitelik")
                        .HasColumnType("text");

                    b.Property<int>("Parsel")
                        .HasColumnType("integer");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("TasinmazID");

                    b.ToTable("Tasinmaz");
                });
#pragma warning restore 612, 618
        }
    }
}
