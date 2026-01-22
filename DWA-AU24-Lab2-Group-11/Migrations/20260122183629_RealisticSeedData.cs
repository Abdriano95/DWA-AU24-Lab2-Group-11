using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DWA_AU24_Lab2_Group_11.Migrations
{
    /// <inheritdoc />
    public partial class RealisticSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Crop",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GrowingDurationInDays", "Name", "Type" },
                values: new object[] { 85, "Barley", 89 });

            migrationBuilder.UpdateData(
                table: "Crop",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "GrowingDurationInDays", "Name", "Type" },
                values: new object[] { 80, "Tomato", 90 });

            migrationBuilder.InsertData(
                table: "Crop",
                columns: new[] { "Id", "GrowingDurationInDays", "Name", "Type" },
                values: new object[,]
                {
                    { 4, 45, "Lettuce", 90 },
                    { 5, 55, "Cucumber", 90 },
                    { 6, 70, "Carrot", 40 },
                    { 7, 90, "Potato", 23 },
                    { 8, 60, "Strawberry", 100 },
                    { 9, 30, "Basil", 80 },
                    { 10, 40, "Dill", 80 }
                });

            migrationBuilder.UpdateData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 1,
                column: "HarvestDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 2,
                column: "HarvestDate",
                value: null);

            migrationBuilder.InsertData(
                table: "Notification",
                columns: new[] { "Id", "IsRead", "Message", "NotificationDate", "PlantingScheduleId" },
                values: new object[,]
                {
                    { 1, false, "Your Wheat crop in North Field is ready for harvest in 5 days!", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, false, "Your Tomato crop in Greenhouse A is ready for harvest in 5 days!", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.UpdateData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Location", "PlantingDate" },
                values: new object[] { "North Field", new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CropId", "Location", "PlantingDate" },
                values: new object[] { 3, "Greenhouse A", new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "PlantingSchedule",
                columns: new[] { "Id", "CropId", "Location", "OptimalPlantingDate", "PlantingDate" },
                values: new object[] { 9, 2, "West Field", null, new DateTime(2025, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "IsCompleted", "PlantingScheduleId", "TaskDate", "TaskDescription", "TaskName" },
                values: new object[,]
                {
                    { 1, false, null, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Check all sprinklers and drip lines for damage before spring planting", "Inspect irrigation system" },
                    { 2, false, null, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Contact supplier for bulk NPK fertilizer delivery", "Order spring fertilizer" },
                    { 3, false, 2, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deep watering needed in Greenhouse A - check soil moisture first", "Water tomatoes" },
                    { 4, false, 1, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apply nitrogen fertilizer to North Field wheat crop", "Fertilize wheat field" },
                    { 6, false, null, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Verify heating system is maintaining optimal temperature for seedlings", "Check greenhouse temperature" },
                    { 7, true, null, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Insulate walls, check heating system, clean glass panels", "Prepare greenhouse for winter" },
                    { 8, true, null, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Placed order with Nordic Seeds AB for spring planting", "Order seeds for spring" }
                });

            migrationBuilder.InsertData(
                table: "GrowthHistory",
                columns: new[] { "Id", "CropName", "DaysBetween", "HarvestDate", "Notes", "PlantingDate", "PlantingScheduleId" },
                values: new object[,]
                {
                    { 1, "Barley", 85, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Excellent yield from West Field. Weather conditions were optimal throughout the growing season.", new DateTime(2025, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 3, "Spring Wheat", 90, new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Last year's spring wheat harvest. Stored in grain silo. Used for bread flour production.", new DateTime(2025, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 }
                });

            migrationBuilder.InsertData(
                table: "HarvestTracking",
                columns: new[] { "Id", "HarvestDate", "PlantingScheduleId" },
                values: new object[] { 9, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 });

            migrationBuilder.InsertData(
                table: "Notification",
                columns: new[] { "Id", "IsRead", "Message", "NotificationDate", "PlantingScheduleId" },
                values: new object[] { 4, true, "Your Barley crop in West Field was successfully harvested!", new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 });

            migrationBuilder.InsertData(
                table: "PlantingSchedule",
                columns: new[] { "Id", "CropId", "Location", "OptimalPlantingDate", "PlantingDate" },
                values: new object[,]
                {
                    { 3, 7, "South Field", null, new DateTime(2025, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 6, "East Garden", null, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 8, "Berry Patch", null, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 4, "Greenhouse B", null, new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 9, "Herb Garden", null, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 5, "Greenhouse A", null, new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 10, "Herb Garden", null, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "IsCompleted", "PlantingScheduleId", "TaskDate", "TaskDescription", "TaskName" },
                values: new object[] { 9, true, 9, new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Complete harvest from West Field - store in grain silo", "Harvest barley crop" });

            migrationBuilder.InsertData(
                table: "GrowthHistory",
                columns: new[] { "Id", "CropName", "DaysBetween", "HarvestDate", "Notes", "PlantingDate", "PlantingScheduleId" },
                values: new object[] { 2, "Dill", 40, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good quality herbs. Harvested before first frost. Dried and stored for winter use.", new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 });

            migrationBuilder.InsertData(
                table: "HarvestTracking",
                columns: new[] { "Id", "HarvestDate", "PlantingScheduleId" },
                values: new object[,]
                {
                    { 3, null, 3 },
                    { 4, null, 4 },
                    { 5, null, 5 },
                    { 6, null, 6 },
                    { 7, null, 7 },
                    { 8, null, 8 },
                    { 10, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 }
                });

            migrationBuilder.InsertData(
                table: "Notification",
                columns: new[] { "Id", "IsRead", "Message", "NotificationDate", "PlantingScheduleId" },
                values: new object[,]
                {
                    { 3, false, "Frost warning! Consider protecting sensitive crops in outdoor areas.", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 5, true, "Your Dill crop in Herb Garden was successfully harvested!", new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 6, true, "Potato planting in South Field is progressing well - 50% growth complete.", new DateTime(2025, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "IsCompleted", "PlantingScheduleId", "TaskDate", "TaskDescription", "TaskName" },
                values: new object[,]
                {
                    { 5, false, 5, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Remove excess runners to focus plant energy on fruit production", "Prune strawberry runners" },
                    { 10, true, 7, new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Transplant basil seedlings to herb garden raised beds", "Plant basil seedlings" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GrowthHistory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GrowthHistory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GrowthHistory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "HarvestTracking",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Notification",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notification",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notification",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Notification",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Notification",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Notification",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Crop",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Crop",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Crop",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Crop",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Crop",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Crop",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Crop",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Crop",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GrowingDurationInDays", "Name", "Type" },
                values: new object[] { 120, "Tomato", 90 });

            migrationBuilder.UpdateData(
                table: "Crop",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "GrowingDurationInDays", "Name", "Type" },
                values: new object[] { 110, "Corn", 120 });

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
                columns: new[] { "Location", "PlantingDate" },
                values: new object[] { "Field A", new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PlantingSchedule",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CropId", "Location", "PlantingDate" },
                values: new object[] { 2, "Greenhouse", new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
