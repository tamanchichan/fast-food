using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fast_food.Migrations
{
    public partial class CartItem_N_OrderItem_Models_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartItem");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "CartItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ItemId",
                table: "CartItem",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Item_ItemId",
                table: "CartItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Item_ItemId",
                table: "OrderItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Item_ItemId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Item_ItemId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_ItemId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "CartItem");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CartItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CartItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CartItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
