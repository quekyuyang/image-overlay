using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageOverlay.Migrations
{
    /// <inheritdoc />
    public partial class AnnotationSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnotationSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnotationSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnotationSet_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnotationSet_ImageId",
                table: "AnnotationSet",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnotationSet");
        }
    }
}
