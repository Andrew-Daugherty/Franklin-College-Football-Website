# Franklin College Football Website

This is my senior project for my Software Engineering degree. It is a full-stack web application built using ASP.NET Core Razor Pages and SQL Server. The website is designed to manage and display information about Franklin College's football program, including the team roster, schedule, statistics, and coaching staff.

## Technologies Used
- ASP.NET Core (Razor Pages)
- C#
- SQL Server
- Entity Framework Core
- HTML and CSS

## Features
- View full team roster
- View coaching staff 
- View full season schedule
- Manage (add, modify, delete) player roster (admin access only)
- Manage staff (admin access only)
- Manage schedule (admin access only)
- Manage statistics (admin access only)

## Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET 6.0 SDK or later
- SQL Server or LocalDB

### How to Run the Project
1. Clone this repository:
```bash
git clone https://github.com/Andrew-Daugherty/Franklin-College-Football-Website.git
```
2. Open the solution file `FCFootball.sln` in Visual Studio  
3. Restore the database using the provided `.bak` file in SQL Server Management Studio  
4. Update the database connection string in `appsettings.json` if needed  
5. Build and run the project using `Ctrl + F5` or the "Start Without Debugging" option

## My Role
This was an individual project. I was responsible for the complete design and development, including:
- Creating the database schema
- Developing the Razor Pages and back-end logic in C#
- Designing the front-end layout and styling using HTML and CSS
- Implementing administrator-specific features

## License
This project is licensed under the MIT License.
