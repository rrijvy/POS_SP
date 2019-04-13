using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace POS_SP.Data.Migrations
{
    public partial class downPayment_Sales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DownPayment",
                table: "Sales",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownPayment",
                table: "Sales");
        }
    }
}
