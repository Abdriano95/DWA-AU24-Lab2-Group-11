using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DWA_AU24_Lab2_Group_11.Models;

namespace DWA_AU24_Lab2_Group_11.Data
{
    /// <summary>
    /// Entity Framework database context for the FarmTrack application.
    /// Manages database connections and entity configurations.
    /// </summary>
    public class FarmTrackContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the FarmTrackContext.
        /// </summary>
        /// <param name="options">Database context configuration options.</param>
        public FarmTrackContext(DbContextOptions<FarmTrackContext> options)
            : base(options)
        {
        }

        /// <summary>Gets or sets the Crops table.</summary>
        public DbSet<DWA_AU24_Lab2_Group_11.Models.Crop> Crop { get; set; } = default!;

        /// <summary>Gets or sets the GrowthHistory table.</summary>
        public DbSet<DWA_AU24_Lab2_Group_11.Models.GrowthHistory> GrowthHistory { get; set; } = default!;

        /// <summary>Gets or sets the HarvestTracking table.</summary>
        public DbSet<DWA_AU24_Lab2_Group_11.Models.HarvestTracking> HarvestTracking { get; set; } = default!;

        /// <summary>Gets or sets the Notifications table.</summary>
        public DbSet<DWA_AU24_Lab2_Group_11.Models.Notification> Notification { get; set; } = default!;

        /// <summary>Gets or sets the PlantingSchedules table.</summary>
        public DbSet<DWA_AU24_Lab2_Group_11.Models.PlantingSchedule> PlantingSchedule { get; set; } = default!;

        /// <summary>Gets or sets the Tasks table.</summary>
        public DbSet<DWA_AU24_Lab2_Group_11.Models.Task> Task { get; set; } = default!;

        /// <summary>Gets or sets the WeatherData table.</summary>
        public DbSet<DWA_AU24_Lab2_Group_11.Models.WeatherData> WeatherData { get; set; } = default!;

        /// <summary>
        /// Configures the entity models and seeds initial data.
        /// Uses fixed dates for seed data to ensure reproducible database state.
        /// </summary>
        /// <param name="modelBuilder">The model builder for entity configuration.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fixed reference date for seed data (ensures reproducible migrations)
            // Using January 1, 2026 as a baseline for demo data
            // This represents "today" in the demo scenario
            var seedDate = new DateTime(2026, 1, 1);

            // ============================================================
            // CROPS - A variety of crops for a Swedish farm scenario
            // ============================================================
            modelBuilder.Entity<Crop>().HasData(
                // Grains
                new Crop { Id = 1, Name = "Wheat", Type = CropType.Grain, GrowingDurationInDays = 90 },
                new Crop { Id = 2, Name = "Barley", Type = CropType.Cereal, GrowingDurationInDays = 85 },
                // Vegetables
                new Crop { Id = 3, Name = "Tomato", Type = CropType.Vegetable, GrowingDurationInDays = 80 },
                new Crop { Id = 4, Name = "Lettuce", Type = CropType.Vegetable, GrowingDurationInDays = 45 },
                new Crop { Id = 5, Name = "Cucumber", Type = CropType.Vegetable, GrowingDurationInDays = 55 },
                // Root vegetables
                new Crop { Id = 6, Name = "Carrot", Type = CropType.Root, GrowingDurationInDays = 70 },
                new Crop { Id = 7, Name = "Potato", Type = CropType.Tuber, GrowingDurationInDays = 90 },
                // Fruits
                new Crop { Id = 8, Name = "Strawberry", Type = CropType.Fruit, GrowingDurationInDays = 60 },
                // Herbs
                new Crop { Id = 9, Name = "Basil", Type = CropType.Herb, GrowingDurationInDays = 30 },
                new Crop { Id = 10, Name = "Dill", Type = CropType.Herb, GrowingDurationInDays = 40 }
            );

            // ============================================================
            // PLANTING SCHEDULES - Various growth stages
            // ============================================================
            modelBuilder.Entity<PlantingSchedule>().HasData(
                // Ready for harvest (planted long ago)
                new PlantingSchedule { Id = 1, CropId = 1, PlantingDate = seedDate.AddDays(-85), Location = "North Field" },    // Wheat - 5 days until harvest
                new PlantingSchedule { Id = 2, CropId = 3, PlantingDate = seedDate.AddDays(-75), Location = "Greenhouse A" },   // Tomato - 5 days until harvest
                
                // Mid-growth
                new PlantingSchedule { Id = 3, CropId = 7, PlantingDate = seedDate.AddDays(-45), Location = "South Field" },    // Potato - 45 days remaining
                new PlantingSchedule { Id = 4, CropId = 6, PlantingDate = seedDate.AddDays(-35), Location = "East Garden" },    // Carrot - 35 days remaining
                new PlantingSchedule { Id = 5, CropId = 8, PlantingDate = seedDate.AddDays(-30), Location = "Berry Patch" },    // Strawberry - 30 days remaining
                
                // Recently planted
                new PlantingSchedule { Id = 6, CropId = 4, PlantingDate = seedDate.AddDays(-10), Location = "Greenhouse B" },   // Lettuce - 35 days remaining
                new PlantingSchedule { Id = 7, CropId = 9, PlantingDate = seedDate.AddDays(-5), Location = "Herb Garden" },     // Basil - 25 days remaining
                new PlantingSchedule { Id = 8, CropId = 5, PlantingDate = seedDate.AddDays(-3), Location = "Greenhouse A" },    // Cucumber - 52 days remaining
                
                // Past harvest (for growth history)
                new PlantingSchedule { Id = 9, CropId = 2, PlantingDate = seedDate.AddDays(-120), Location = "West Field" },    // Barley - harvested
                new PlantingSchedule { Id = 10, CropId = 10, PlantingDate = seedDate.AddDays(-60), Location = "Herb Garden" }   // Dill - harvested
            );

            // ============================================================
            // TASKS - Mix of pending, completed, and overdue
            // ============================================================
            modelBuilder.Entity<Models.Task>().HasData(
                // Overdue tasks (before seedDate)
                new Models.Task 
                { 
                    Id = 1, 
                    TaskName = "Inspect irrigation system", 
                    TaskDescription = "Check all sprinklers and drip lines for damage before spring planting",
                    TaskDate = seedDate.AddDays(-5), 
                    IsCompleted = false, 
                    PlantingScheduleId = null 
                },
                new Models.Task 
                { 
                    Id = 2, 
                    TaskName = "Order spring fertilizer", 
                    TaskDescription = "Contact supplier for bulk NPK fertilizer delivery",
                    TaskDate = seedDate.AddDays(-3), 
                    IsCompleted = false, 
                    PlantingScheduleId = null 
                },
                
                // Upcoming tasks (pending)
                new Models.Task 
                { 
                    Id = 3, 
                    TaskName = "Water tomatoes", 
                    TaskDescription = "Deep watering needed in Greenhouse A - check soil moisture first",
                    TaskDate = seedDate.AddDays(1), 
                    IsCompleted = false, 
                    PlantingScheduleId = 2 
                },
                new Models.Task 
                { 
                    Id = 4, 
                    TaskName = "Fertilize wheat field", 
                    TaskDescription = "Apply nitrogen fertilizer to North Field wheat crop",
                    TaskDate = seedDate.AddDays(2), 
                    IsCompleted = false, 
                    PlantingScheduleId = 1 
                },
                new Models.Task 
                { 
                    Id = 5, 
                    TaskName = "Prune strawberry runners", 
                    TaskDescription = "Remove excess runners to focus plant energy on fruit production",
                    TaskDate = seedDate.AddDays(3), 
                    IsCompleted = false, 
                    PlantingScheduleId = 5 
                },
                new Models.Task 
                { 
                    Id = 6, 
                    TaskName = "Check greenhouse temperature", 
                    TaskDescription = "Verify heating system is maintaining optimal temperature for seedlings",
                    TaskDate = seedDate.AddDays(5), 
                    IsCompleted = false, 
                    PlantingScheduleId = null 
                },
                
                // Completed tasks
                new Models.Task 
                { 
                    Id = 7, 
                    TaskName = "Prepare greenhouse for winter", 
                    TaskDescription = "Insulate walls, check heating system, clean glass panels",
                    TaskDate = seedDate.AddDays(-30), 
                    IsCompleted = true, 
                    PlantingScheduleId = null 
                },
                new Models.Task 
                { 
                    Id = 8, 
                    TaskName = "Order seeds for spring", 
                    TaskDescription = "Placed order with Nordic Seeds AB for spring planting",
                    TaskDate = seedDate.AddDays(-20), 
                    IsCompleted = true, 
                    PlantingScheduleId = null 
                },
                new Models.Task 
                { 
                    Id = 9, 
                    TaskName = "Harvest barley crop", 
                    TaskDescription = "Complete harvest from West Field - store in grain silo",
                    TaskDate = seedDate.AddDays(-35), 
                    IsCompleted = true, 
                    PlantingScheduleId = 9 
                },
                new Models.Task 
                { 
                    Id = 10, 
                    TaskName = "Plant basil seedlings", 
                    TaskDescription = "Transplant basil seedlings to herb garden raised beds",
                    TaskDate = seedDate.AddDays(-5), 
                    IsCompleted = true, 
                    PlantingScheduleId = 7 
                }
            );

            // ============================================================
            // NOTIFICATIONS - Harvest reminders
            // ============================================================
            modelBuilder.Entity<Notification>().HasData(
                // Unread notifications (important)
                new Notification 
                { 
                    Id = 1, 
                    Message = "Your Wheat crop in North Field is ready for harvest in 5 days!", 
                    NotificationDate = seedDate, 
                    IsRead = false, 
                    PlantingScheduleId = 1 
                },
                new Notification 
                { 
                    Id = 2, 
                    Message = "Your Tomato crop in Greenhouse A is ready for harvest in 5 days!", 
                    NotificationDate = seedDate, 
                    IsRead = false, 
                    PlantingScheduleId = 2 
                },
                new Notification 
                { 
                    Id = 3, 
                    Message = "Frost warning! Consider protecting sensitive crops in outdoor areas.", 
                    NotificationDate = seedDate.AddDays(-1), 
                    IsRead = false, 
                    PlantingScheduleId = 5 
                },
                
                // Read notifications (historical)
                new Notification 
                { 
                    Id = 4, 
                    Message = "Your Barley crop in West Field was successfully harvested!", 
                    NotificationDate = seedDate.AddDays(-35), 
                    IsRead = true, 
                    PlantingScheduleId = 9 
                },
                new Notification 
                { 
                    Id = 5, 
                    Message = "Your Dill crop in Herb Garden was successfully harvested!", 
                    NotificationDate = seedDate.AddDays(-20), 
                    IsRead = true, 
                    PlantingScheduleId = 10 
                },
                new Notification 
                { 
                    Id = 6, 
                    Message = "Potato planting in South Field is progressing well - 50% growth complete.", 
                    NotificationDate = seedDate.AddDays(-10), 
                    IsRead = true, 
                    PlantingScheduleId = 3 
                }
            );

            // ============================================================
            // HARVEST TRACKING - Mix of completed and pending harvests
            // ============================================================
            modelBuilder.Entity<HarvestTracking>().HasData(
                // Pending harvests (HarvestDate is null or future expected date)
                new HarvestTracking { Id = 1, PlantingScheduleId = 1, HarvestDate = null },      // Wheat - not yet harvested
                new HarvestTracking { Id = 2, PlantingScheduleId = 2, HarvestDate = null },      // Tomato - not yet harvested
                new HarvestTracking { Id = 3, PlantingScheduleId = 3, HarvestDate = null },      // Potato - not yet harvested
                new HarvestTracking { Id = 4, PlantingScheduleId = 4, HarvestDate = null },      // Carrot - not yet harvested
                new HarvestTracking { Id = 5, PlantingScheduleId = 5, HarvestDate = null },      // Strawberry - not yet harvested
                new HarvestTracking { Id = 6, PlantingScheduleId = 6, HarvestDate = null },      // Lettuce - not yet harvested
                new HarvestTracking { Id = 7, PlantingScheduleId = 7, HarvestDate = null },      // Basil - not yet harvested
                new HarvestTracking { Id = 8, PlantingScheduleId = 8, HarvestDate = null },      // Cucumber - not yet harvested
                
                // Completed harvests
                new HarvestTracking { Id = 9, PlantingScheduleId = 9, HarvestDate = seedDate.AddDays(-35) },   // Barley - harvested
                new HarvestTracking { Id = 10, PlantingScheduleId = 10, HarvestDate = seedDate.AddDays(-20) }  // Dill - harvested
            );

            // ============================================================
            // GROWTH HISTORY - Records of past successful harvests
            // ============================================================
            modelBuilder.Entity<GrowthHistory>().HasData(
                new GrowthHistory 
                { 
                    Id = 1, 
                    PlantingScheduleId = 9, 
                    CropName = "Barley", 
                    PlantingDate = seedDate.AddDays(-120), 
                    HarvestDate = seedDate.AddDays(-35), 
                    DaysBetween = 85, 
                    Notes = "Excellent yield from West Field. Weather conditions were optimal throughout the growing season." 
                },
                new GrowthHistory 
                { 
                    Id = 2, 
                    PlantingScheduleId = 10, 
                    CropName = "Dill", 
                    PlantingDate = seedDate.AddDays(-60), 
                    HarvestDate = seedDate.AddDays(-20), 
                    DaysBetween = 40, 
                    Notes = "Good quality herbs. Harvested before first frost. Dried and stored for winter use." 
                },
                new GrowthHistory 
                { 
                    Id = 3, 
                    PlantingScheduleId = 9,  // Reusing schedule ID for historical record
                    CropName = "Spring Wheat", 
                    PlantingDate = seedDate.AddDays(-300), 
                    HarvestDate = seedDate.AddDays(-210), 
                    DaysBetween = 90, 
                    Notes = "Last year's spring wheat harvest. Stored in grain silo. Used for bread flour production." 
                }
            );
        }
    }
}
