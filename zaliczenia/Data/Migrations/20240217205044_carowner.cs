﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zaliczenia.Data.Migrations
{
    /// <inheritdoc />
    public partial class carowner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Wlasciciel",
                table: "Auto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Wlasciciel",
                table: "Auto");
        }
    }
}
