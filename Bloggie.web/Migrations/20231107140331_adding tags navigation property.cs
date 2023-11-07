using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.web.Migrations
{
    /// <inheritdoc />
    public partial class addingtagsnavigationproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tag_BlogPostId",
                table: "Tag",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_BlogPosts_BlogPostId",
                table: "Tag",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_BlogPosts_BlogPostId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_BlogPostId",
                table: "Tag");
        }
    }
}
