using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageOverlay.Migrations
{
    /// <inheritdoc />
    public partial class Image2AnnotationSetsCollectionNav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "AnnotationSet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "AnnotationSet");
        }
    }
}
