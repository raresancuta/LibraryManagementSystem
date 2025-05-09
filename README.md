# Library Management System - Console Application

## Description
A console-based Library Management System built with C# and .NET using a layered architecture.  
It utilizes **Entity Framework Core** as the ORM and stores data in a **SQLite** database.

This application allows library staff to manage book inventory, track lending activity, and provide book recommendations.

---

## Architecture

This project follows a clean layered architecture:
- **Presentation Layer** (UI Project)
- **Business Logic Layer** (Service Project)
- **Data Access Layer** (Persistence Project)
- **Entity Layer** (Model Project)
- **Machine Learning Layer** (Machine Learning Project)

---

## Technologies Used

- .NET Console Application
- C#
- Entity Framework Core
- SQLite (Local Database)
- ML.NET (FastTree Regression)

---

## Data Models

- **Book**
- **Lending**
- **Lending Status**

Each model is managed via **EF Core**, and migrations are used to set up the database schema.

---

## Main Functionalities

### 1. CRUD Operations
- Enables the library administrator to **create**, **read**, **update**, and **delete** book records.
- Ensures consistent data management and supports efficient inventory maintenance.

### 2. Advanced Search Functionality
- Allows flexible book lookup using the following filters:
  - **Title**
  - **Author**
  - **ISBN**
- Designed for quick and intuitive access to book details.

### 3. Book Lending Management
- Supports the full lending lifecycle:
  - Lend a book to a client (**only if copies are available**).
  - Prevent lending when **no stock** is available.
  - Disallow return if **all copies are already marked as returned**.
- Tracks important lending data:
  - **Client name**
  - **Lending period**
  - **Lending status**

### 4. Recommendations & Predictions (The Innovative Functionality)

This feature provides data-driven suggestions to assist with library collection planning and enhance user engagement.

#### Features:
- **Top 5 Most Borrowed Books**: Identifies the five most frequently borrowed books of all time.
- **Popular Author Suggestions**: Recommends books written by the top 3 most borrowed authors.
- **Seasonal Trends**: Offers book recommendations based on borrowing patterns for the current month across past years.
- **Lending Predictions**:
  - Estimates how many times a specific book is likely to be borrowed in the current month.
  - Powered by a **machine learning model** trained on approximately **6 years** of historical data.
  - Utilizes **Fast Tree regression** from ML.NET to capture complex borrowing trends.
  - Helps guide decisions around **purchasing, restocking**, and **inventory planning**.


---

## Installation

This application was developed using **Visual Studio 2022**.  
It is recommended to use Visual Studio 2022 or later for full compatibility with C#, .NET, and ML.NET tooling.

Before running the application, make sure the following NuGet packages are installed in the appropriate projects:

### NuGet Packages

#### MachineLearning Project
```bash
dotnet add package Microsoft.ML  
dotnet add package Microsoft.ML.FastTree
```
#### Persistence Project
```bash
dotnet add package Microsoft.EntityFrameworkCore  
dotnet add package Microsoft.EntityFrameworkCore.Sqlite  
dotnet add package Microsoft.Extensions.Configuration  
dotnet add package Microsoft.Extensions.Configuration.Json  
dotnet add package Microsoft.Extensions.DependencyInjection
```
You can run these commands from each project folder using the terminal or install them via the NuGet Package Manager in Visual Studio.

##  Running the Application

To run the application correctly, make sure the **UI project** is set as the startup project in Visual Studio:

1. Open the solution in **Visual Studio**.
2. In the **Solution Explorer**, locate the UI project.
3. Right-click the UI project and select **"Set as Startup Project"**.
4. Press **F5** or click **"Start"** to run the application.

This ensures the app launches from the correct user interface entry point.
## Usage

After starting the application, the following menu is displayed:

    Select an option:
    -----------------
    1. Show Books
    2. Search Book
    3. Add Book
    4. Delete Book
    5. Update Book
    6. Add Copies of Book
    7. Remove Copies of Book
    8. Lend Book
    9. Return Book
    10. Book Recommendations
    11. Exit

When choosing option 2 (Search Book), the user can search by:

    Search Book(s) By:
    ------------------
    1. Title
    2. Author
    3. ISBN

Option 10 displays various book recommendation features:

    Show Book Recommendations:
    --------------------------
    1. Get Top 5 Lent Books
    2. Get By Authors Popularity
    3. Get Top 5 Books For This Season
    4. Get Prediction of Lending For This Month

## Notes

- Make sure the SQLite file is correctly referenced in the connection string.
- Entity Framework migrations should be handled from the root project folder.

## Author
Rares Ancuta

