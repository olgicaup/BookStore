using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInOrders_Orders_OrderId",
                table: "BookInOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Authors_AuthorId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Publishers_PublisherId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AuthorId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PublisherId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInOrders_Orders_OrderId",
                table: "BookInOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInOrders_Orders_OrderId",
                table: "BookInOrders");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PublisherId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AuthorId",
                table: "Orders",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PublisherId",
                table: "Orders",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInOrders_Orders_OrderId",
                table: "BookInOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Authors_AuthorId",
                table: "Orders",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Publishers_PublisherId",
                table: "Orders",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
