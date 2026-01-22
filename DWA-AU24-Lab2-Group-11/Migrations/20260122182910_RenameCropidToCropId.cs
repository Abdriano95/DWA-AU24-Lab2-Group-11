using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWA_AU24_Lab2_Group_11.Migrations
{
    /// <inheritdoc />
    public partial class RenameCropidToCropId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantingSchedule_Crop_Cropid",
                table: "PlantingSchedule");

            migrationBuilder.RenameColumn(
                name: "Cropid",
                table: "PlantingSchedule",
                newName: "CropId");

            migrationBuilder.RenameIndex(
                name: "IX_PlantingSchedule_Cropid",
                table: "PlantingSchedule",
                newName: "IX_PlantingSchedule_CropId");

            migrationBuilder.UpdateData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 1,
                column: "HarvestDate",
                value: new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 2,
                column: "HarvestDate",
                value: new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlantingDate",
                value: new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 2,
                column: "PlantingDate",
                value: new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_PlantingSchedule_Crop_CropId",
                table: "PlantingSchedule",
                column: "CropId",
                principalTable: "Crop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantingSchedule_Crop_CropId",
                table: "PlantingSchedule");

            migrationBuilder.RenameColumn(
                name: "CropId",
                table: "PlantingSchedule",
                newName: "Cropid");

            migrationBuilder.RenameIndex(
                name: "IX_PlantingSchedule_CropId",
                table: "PlantingSchedule",
                newName: "IX_PlantingSchedule_Cropid");

            migrationBuilder.UpdateData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 1,
                column: "HarvestDate",
                value: new DateTime(2024, 12, 9, 11, 18, 55, 236, DateTimeKind.Local).AddTicks(7048));

            migrationBuilder.UpdateData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 2,
                column: "HarvestDate",
                value: new DateTime(2024, 12, 9, 11, 18, 55, 236, DateTimeKind.Local).AddTicks(7053));

            migrationBuilder.UpdateData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlantingDate",
                value: new DateTime(2024, 10, 30, 11, 18, 55, 236, DateTimeKind.Local).AddTicks(6965));

            migrationBuilder.UpdateData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 2,
                column: "PlantingDate",
                value: new DateTime(2024, 9, 30, 11, 18, 55, 236, DateTimeKind.Local).AddTicks(7023));

            migrationBuilder.AddForeignKey(
                name: "FK_PlantingSchedule_Crop_Cropid",
                table: "PlantingSchedule",
                column: "Cropid",
                principalTable: "Crop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
