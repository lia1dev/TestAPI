using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixParentNodeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreeNodes_TreeNodes_ParentId",
                table: "TreeNodes");

            migrationBuilder.DropIndex(
                name: "IX_TreeNodes_ParentId",
                table: "TreeNodes");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "TreeNodes");

            migrationBuilder.CreateIndex(
                name: "IX_TreeNodes_ParentNodeId",
                table: "TreeNodes",
                column: "ParentNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreeNodes_TreeNodes_ParentNodeId",
                table: "TreeNodes",
                column: "ParentNodeId",
                principalTable: "TreeNodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreeNodes_TreeNodes_ParentNodeId",
                table: "TreeNodes");

            migrationBuilder.DropIndex(
                name: "IX_TreeNodes_ParentNodeId",
                table: "TreeNodes");

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "TreeNodes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TreeNodes_ParentId",
                table: "TreeNodes",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreeNodes_TreeNodes_ParentId",
                table: "TreeNodes",
                column: "ParentId",
                principalTable: "TreeNodes",
                principalColumn: "Id");
        }
    }
}
