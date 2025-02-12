using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class rosica1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "BookInShoppingCarts");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "BookInShoppingCarts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "BookInShoppingCarts");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "BookInShoppingCarts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }
    }
}
