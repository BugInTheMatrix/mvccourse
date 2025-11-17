using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class AddingCommentsnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostComment_BlogPosts_BlogPostId",
                table: "BlogPostComment");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostLike_BlogPosts_BlogPostId",
                table: "BlogPostLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostLike",
                table: "BlogPostLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostComment",
                table: "BlogPostComment");

            migrationBuilder.RenameTable(
                name: "BlogPostLike",
                newName: "BlogPostLikes");

            migrationBuilder.RenameTable(
                name: "BlogPostComment",
                newName: "BlogPostComments");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostLike_BlogPostId",
                table: "BlogPostLikes",
                newName: "IX_BlogPostLikes_BlogPostId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostComment_BlogPostId",
                table: "BlogPostComments",
                newName: "IX_BlogPostComments_BlogPostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostLikes",
                table: "BlogPostLikes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostComments",
                table: "BlogPostComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostComments_BlogPosts_BlogPostId",
                table: "BlogPostComments",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostLikes_BlogPosts_BlogPostId",
                table: "BlogPostLikes",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostComments_BlogPosts_BlogPostId",
                table: "BlogPostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostLikes_BlogPosts_BlogPostId",
                table: "BlogPostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostLikes",
                table: "BlogPostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostComments",
                table: "BlogPostComments");

            migrationBuilder.RenameTable(
                name: "BlogPostLikes",
                newName: "BlogPostLike");

            migrationBuilder.RenameTable(
                name: "BlogPostComments",
                newName: "BlogPostComment");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostLikes_BlogPostId",
                table: "BlogPostLike",
                newName: "IX_BlogPostLike_BlogPostId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostComments_BlogPostId",
                table: "BlogPostComment",
                newName: "IX_BlogPostComment_BlogPostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostLike",
                table: "BlogPostLike",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostComment",
                table: "BlogPostComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostComment_BlogPosts_BlogPostId",
                table: "BlogPostComment",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostLike_BlogPosts_BlogPostId",
                table: "BlogPostLike",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
