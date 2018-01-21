using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CallsCRM.Migrations
{
    public partial class AddedUniqueness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Customers_Email",
                table: "Customers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Customers_PhoneNumber",
                table: "Customers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Callers_Login",
                table: "Callers");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber_Email",
                table: "Customers",
                columns: new[] { "PhoneNumber", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Callers_Login",
                table: "Callers",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_PhoneNumber_Email",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Callers_Login",
                table: "Callers");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Customers_Email",
                table: "Customers",
                column: "Email");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Customers_PhoneNumber",
                table: "Customers",
                column: "PhoneNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Callers_Login",
                table: "Callers",
                column: "Login");
        }
    }
}
