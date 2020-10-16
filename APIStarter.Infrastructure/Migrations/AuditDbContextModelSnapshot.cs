﻿// <auto-generated />
using System;
using APIStarter.Infrastructure.Audit.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIStarter.Infrastructure.Migrations
{
    [DbContext(typeof(AuditDbContext))]
    partial class AuditDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("APIStarter.Domain.Audit.Aggregates.AuditCommand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Command")
                        .HasColumnType("text");

                    b.Property<string>("CommandName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ImpersonatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CommandName");

                    b.HasIndex("CorrelationId");

                    b.HasIndex("Date");

                    b.HasIndex("Id");

                    b.HasIndex("ImpersonatedUserId");

                    b.HasIndex("UserId");

                    b.ToTable("AuditCommands");
                });

            modelBuilder.Entity("APIStarter.Domain.Audit.Aggregates.AuditDatabaseChange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Changes")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("EntityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("ImpersonatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TableName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WriteAction")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("Changes");

                    b.HasIndex("CorrelationId");

                    b.HasIndex("Date");

                    b.HasIndex("EntityId");

                    b.HasIndex("Id");

                    b.HasIndex("ImpersonatedUserId");

                    b.HasIndex("TableName");

                    b.HasIndex("UserId");

                    b.HasIndex("WriteAction");

                    b.ToTable("AuditDatabaseChanges");
                });

            modelBuilder.Entity("APIStarter.Domain.Audit.Aggregates.AuditEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Event")
                        .HasColumnType("text");

                    b.Property<string>("EventName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("ImpersonatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CorrelationId");

                    b.HasIndex("Date");

                    b.HasIndex("EventName");

                    b.HasIndex("Id");

                    b.HasIndex("ImpersonatedUserId");

                    b.HasIndex("UserId");

                    b.ToTable("AuditEvents");
                });

            modelBuilder.Entity("APIStarter.Domain.Audit.Aggregates.AuditQuery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ImpersonatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Query")
                        .HasColumnType("text");

                    b.Property<string>("QueryName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("QueryResult")
                        .HasColumnType("text");

                    b.Property<string>("QueryResultName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CorrelationId");

                    b.HasIndex("Date");

                    b.HasIndex("Id");

                    b.HasIndex("ImpersonatedUserId");

                    b.HasIndex("QueryName");

                    b.HasIndex("QueryResultName");

                    b.HasIndex("UserId");

                    b.ToTable("AuditQueries");
                });

            modelBuilder.Entity("APIStarter.Domain.Audit.Aggregates.AuditRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("HttpMethod")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int>("HttpStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("ImpersonatedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RequestBody")
                        .HasColumnType("text");

                    b.Property<string>("RequestHeaders")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ResponseBody")
                        .HasColumnType("text");

                    b.Property<string>("Uri")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CorrelationId");

                    b.HasIndex("Date");

                    b.HasIndex("Duration");

                    b.HasIndex("HttpMethod");

                    b.HasIndex("Id");

                    b.HasIndex("ImpersonatedUserId");

                    b.HasIndex("RequestHeaders");

                    b.HasIndex("Uri");

                    b.HasIndex("UserId");

                    b.ToTable("AuditRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
