# InfoTrack.SearchSiteInfo

This Application can be used to scrape in Google, Bing or Yahoo with search keywords to get information on which position the web site is ranked. 

# Technology

ASP.NET Core, Angular.

## Versions

The main branch is using .NET 8, Angular 17

# Requirements

Make sure you have SQL Express and .Net 8.0 installed on your machine.
Make sure you have npm and Node.js installed on your machine.

# Getting Started

1. Clone repository. 
2. Open InfoTrack.SearchSiteInfo.sln solution file in visual studio. 
3. Set InfoTrack.SearchSiteInfo.WebApi project as Startup project.
4. Build project
5. Use 'http' as the Launch Profile 

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

The project implements CQRS patern and other dependencis.

## The WebApi Project

The entry point of the application is the ASP.NET Core web api project.

## The Test Projects

Test projects have unit, functional, integration tests projects. The projects are using xunit - unit testing tool - [xunit](https://www.nuget.org/packages/xunit) 




