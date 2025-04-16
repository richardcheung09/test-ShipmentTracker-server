# ShipmentTracker

ShipmentTracker is a web application designed to manage and track shipments and carriers. This project is built using ASP.NET Core and Entity Framework Core.

## Setup Instructions

1. **Clone the Repository**
   ```powershell
   git clone <repository-url>
   cd ShipmentTracker/server
   ```

2. **Install Dependencies**
   Ensure you have the following installed:
   - [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
   - [SQLite](https://www.sqlite.org/download.html) (optional, for database management)

3. **Database Setup**
   - Run the migrations to set up the database:
     ```powershell
     dotnet ef database update
     ```

4. **Run the Application**
   - Start the server:
     ```powershell
     dotnet run
     ```
   - The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Deployment Instructions

1. **Publish the Application**
   - Use the `dotnet publish` command to create a release build:
     ```powershell
     dotnet publish -c Release -o ./publish
     ```

2. **Deploy to a Web Server**
   - Copy the contents of the `./publish` directory to your web server.
   - Configure the server to use the `ShipmentTracker` executable.

3. **Database Configuration**
   - Ensure the `appsettings.json` file is correctly configured for your production database.

## Explanation of Design Choices

- **Separation of Concerns**: The project is divided into `API`, `Domain`, and `Infrastructure` layers to ensure a clean architecture.
- **Entity Framework Core**: Used for database management to simplify data access and migrations.
- **Unit Testing**: The `ShipmentTracker.Test` project includes unit tests for controllers and domain logic to ensure reliability.
- **SQLite**: Chosen as the database for its simplicity and ease of use during development. This will enable transaction ensuring integrity of the data
