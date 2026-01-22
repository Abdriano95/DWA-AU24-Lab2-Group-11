# 🌾 FarmTrack

> A modern farm management application built with ASP.NET Core MVC

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?logo=dotnet)](https://docs.microsoft.com/aspnet/core)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%208-512BD4?logo=dotnet)](https://docs.microsoft.com/ef/core)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

FarmTrack is a comprehensive farm management system that helps farmers track crops, manage planting schedules, monitor harvests, and stay on top of daily tasks. Built with a modern, responsive UI and real-time weather integration.

![FarmTrack Dashboard](screenshots/dashboard.png)

## ✨ Features

### 📊 Dashboard
- Real-time weather widget powered by Open-Meteo API
- Notification center with unread indicators
- Task overview with quick completion actions
- Responsive card-based layout

### 🌱 Crop Management
- Track multiple crop types (Grains, Vegetables, Fruits, Herbs)
- Monitor growing duration and expected harvest dates
- Comprehensive CRUD operations

### 📅 Planting Schedules
- Plan planting dates with expected harvest calculations
- Assign crops to specific fields/locations
- Automatic harvest date computation based on crop growing days

### ✅ Task Management
- Create and assign farm tasks
- Track task status (Pending, In Progress, Completed)
- Overdue task notifications
- Quick completion from dashboard

### 📈 Growth History & Harvest Tracking
- Record harvest yields and quality
- Track growth history over time
- Analytics on crop performance

### 👤 User Management
- Secure authentication with ASP.NET Core Identity
- Role-based authorization (Admin/User)
- User-specific location settings for weather

## 🛠️ Tech Stack

| Layer | Technology |
|-------|------------|
| **Framework** | ASP.NET Core 8.0 MVC |
| **ORM** | Entity Framework Core 8.0 |
| **Database** | SQL Server (LocalDB) |
| **Authentication** | ASP.NET Core Identity |
| **UI Framework** | Bootstrap 5.3 + Bootswatch Minty |
| **Icons** | Bootstrap Icons |
| **Weather API** | Open-Meteo (no API key required) |
| **Configuration** | Options Pattern + User Secrets |

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server LocalDB (included with Visual Studio) or SQL Server Express
- (Optional) Visual Studio 2022 or VS Code with C# extension

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/DWA-AU24-Lab2-Group-11.git
   cd DWA-AU24-Lab2-Group-11
   ```

2. **Navigate to the project folder**
   ```bash
   cd DWA-AU24-Lab2-Group-11
   ```

3. **Configure Admin Credentials (User Secrets)**
   
   The admin account is configured via .NET User Secrets for security:
   ```bash
   dotnet user-secrets set "AdminSeed:Email" "admin@farmtrack.local"
   dotnet user-secrets set "AdminSeed:Password" "YourSecurePassword123!"
   dotnet user-secrets set "AdminSeed:FirstName" "Admin"
   dotnet user-secrets set "AdminSeed:LastName" "User"
   ```

4. **Apply Database Migrations**
   ```bash
   # Create the FarmTrack database (crops, schedules, tasks, etc.)
   dotnet ef database update --context FarmTrackContext
   
   # Create the Identity database (users, roles)
   dotnet ef database update --context DWA_AU24_Lab2_Group_11Context
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

6. **Open in Browser**
   
   Navigate to `https://localhost:5001` or `http://localhost:5000`

### Quick Start with Visual Studio

1. Open `DWA-AU24-Lab2-Group-11.sln`
2. Set `DWA-AU24-Lab2-Group-11` as the startup project
3. Open Package Manager Console and run:
   ```
   Update-Database -Context FarmTrackContext
   Update-Database -Context DWA_AU24_Lab2_Group_11Context
   ```
4. Press F5 to run

## 📸 Screenshots

### Dashboard
![Dashboard](screenshots/dashboard.png)

### Crop Management
![Crops](screenshots/crops.png)

### Mobile View
![Mobile](screenshots/mobile.png)

## 🏗️ Architecture

```
DWA-AU24-Lab2-Group-11/
├── Configuration/           # Options classes for strongly-typed config
│   ├── AdminSeedOptions.cs
│   └── WeatherApiOptions.cs
├── Controllers/             # MVC Controllers
│   ├── HomeController.cs    # Dashboard with weather, notifications, tasks
│   ├── CropsController.cs   # Crop CRUD operations
│   ├── TaskController.cs    # Task management
│   └── ...
├── Data/
│   └── FarmTrackContext.cs  # EF Core DbContext with seed data
├── Models/                  # Domain entities
│   ├── Crop.cs
│   ├── PlantingSchedule.cs
│   ├── HarvestTracking.cs
│   └── ...
├── Services/
│   ├── WeatherApiService.cs      # Open-Meteo API integration
│   └── NotificationService.cs    # Background notification service
├── Views/                   # Razor views organized by controller
└── wwwroot/
    └── css/site.css         # Custom CSS design system
```

### Design Patterns Used

- **MVC Pattern** - Clear separation of Models, Views, and Controllers
- **Repository Pattern** - Via Entity Framework Core DbContext
- **Options Pattern** - Strongly-typed configuration (WeatherApiOptions, AdminSeedOptions)
- **Background Services** - NotificationService for automated notifications
- **Dependency Injection** - Throughout the application

### Key Design Decisions

1. **Open-Meteo Weather API** - Chosen because it requires no API key, making the project easy to demo without configuration
2. **User Secrets** - Admin credentials stored securely outside of source control
3. **Bootswatch Minty Theme** - Fresh, modern look with green accents perfect for a farm application
4. **CSS Custom Properties** - Design system with `--ft-*` variables for consistent theming

## 🔧 Configuration

### Weather Settings

Weather is automatically fetched based on the user's location coordinates. Default location is Stockholm, Sweden.

```json
// appsettings.json
{
  "WeatherApi": {
    "BaseUrl": "https://api.open-meteo.com/v1",
    "CacheDurationMinutes": 30
  }
}
```

### Admin User Customization

Configure the admin user via User Secrets:

```bash
dotnet user-secrets set "AdminSeed:Email" "your@email.com"
dotnet user-secrets set "AdminSeed:Password" "YourPassword123!"
dotnet user-secrets set "AdminSeed:FirstName" "Your"
dotnet user-secrets set "AdminSeed:LastName" "Name"
dotnet user-secrets set "AdminSeed:Location" "Your City"
dotnet user-secrets set "AdminSeed:Latitude" "59.3293"
dotnet user-secrets set "AdminSeed:Longitude" "18.0686"
```

## 🧪 Database Reset

To reset the databases and start fresh:

```bash
cd DWA-AU24-Lab2-Group-11

# Drop and recreate FarmTrack database
dotnet ef database drop --context FarmTrackContext --force
dotnet ef database update --context FarmTrackContext

# Drop and recreate Identity database
dotnet ef database drop --context DWA_AU24_Lab2_Group_11Context --force
dotnet ef database update --context DWA_AU24_Lab2_Group_11Context
```

## 📝 Seed Data

The application comes pre-seeded with realistic demo data:

| Entity | Count | Description |
|--------|-------|-------------|
| Crops | 10 | Wheat, Barley, Tomato, Lettuce, Cucumber, Carrot, Potato, Strawberry, Basil, Dill |
| Planting Schedules | 10 | Various growth stages across different fields |
| Tasks | 10 | Mix of completed, pending, and overdue tasks |
| Notifications | 6 | Harvest alerts, weather warnings, progress updates |
| Harvest Records | 10 | Pending and completed harvests |
| Growth History | 3 | Historical harvest records |

## 👥 Authors

- **Abdulla Mehdi** - [GitHub](https://github.com/yourusername)
- **Joakim Olsson** - [GitHub](https://github.com/yourusername)

*Originally developed as part of the Development of Web Applications course (DWA-AU24) at [University Name]*

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

<p align="center">
  Made with 💚 and ASP.NET Core
</p>
