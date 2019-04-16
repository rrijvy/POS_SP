using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace POS_SP.Data.Migrations
{
    public partial class IsPaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "InstallmentSchedulePayments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "InstallmentSchedulePayments");
        }
    }
}
