using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace POS_SP.Data.Migrations
{
    public partial class Update_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstallmentPeriod",
                table: "Sales",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentPeriod",
                table: "Sales");
        }
    }
}
