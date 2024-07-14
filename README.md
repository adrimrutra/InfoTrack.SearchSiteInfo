# InfoTrack.SearchSiteInfo

This Application can be used to scrape in Google, Bing or Yahoo with search keywords to get information on which position the web site is ranked. 

# Technology

.Net Core 8.0, ASP.NET Core WebApi, EF Core, 'SQL Express', Angular.

## Versions

The main branch is using .NET 8, Angular 17

# Requirements

## Back End
 Make sure you have '.Net Core 8.0', 'SQL Express' installed on your machine.
## Front End
 Make sure you have 'npm', 'Node.js' and 'Angular 17' installed on your machine.

# Getting Started

To start using application first Clone Repository.

## Back End
1. Go to 'InfoTrack.SearchSiteInfo' folder and open InfoTrack.SearchSiteInfo.sln solution file in Visual studio 2022.
2. Set InfoTrack.SearchSiteInfo.WebApi project as Startup project.
3. Connection to DataBase can be located in appsettings.json file:
   Using SQL Express Server:
   ### Server=localhost\\SQLEXPRESS;Database=InfoTrackSearchSiteInfoDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False
   Or if needed use SQL Server: 
   ### Server=(local);Database=InfoTrackSearchSiteInfoDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False
   
5. Use 'InfoTrack.SearchSiteInfo.WebApi' as the Launch Profile. ![image](https://github.com/user-attachments/assets/17256981-bdc6-40fb-9f7d-33a27893bbbb)
6. Build project.
7. When application will start it automatically will create 'InfoTrackSearchSiteInfoDb' database and populate it with seed data.

## Front End
1. Go to 'InfoTrack.SearchSiteInfo\InfoTrack_SearchSiteInfoUI' and open project folder preferably in Visual Studio Code.
2. In terminal run command 'npm install' to install dependencies. 
3. Optionaly run 'ng test' to test apllication.
4. To start aplication run 'ng serve' command.
5. The application can be opened in browser using: 'localhost:4200'.

## Search page
![image](https://github.com/user-attachments/assets/7982d176-10de-4afd-a30b-b6a1309d5eda)

## History page
![image](https://github.com/user-attachments/assets/e9f80b43-83ee-4704-b8ad-574bde0dc442)




# Architecture

The application designed with Clean Architecture, with patterns: SQRS, Repository, Unit of work and few other paterns.

# Dependencies

## The Core Project

The Core project is the center of the Clean Architecture design, and all other project dependencies are pointing toward it.

- Entities
- Constants
- Exceptions
- Extensions
- Heandlers
- Interfaces
- Models
- Services

## The Infrastructure Project

The project implements interfaces defined in Core with paterns Repository, Unit of work.

## The UseCases Project

The project implements CQRS patern.

## The WebApi Project

The entry point of the application is the ASP.NET Core web api project.

## The Test Projects

Test projects have unit, functional, integration tests projects. The projects are using xunit - unit testing tool - [xunit](https://www.nuget.org/packages/xunit) 
The application is covered with unit, functional and integration tests.

## The UI Angular Project

The project uses Angular 17 framework with karma, jasmine, ng-mocks libraries.




