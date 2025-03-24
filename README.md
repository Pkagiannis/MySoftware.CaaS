# Cat Image Storage API

## Overview

This project is an ASP.NET Core Web API (.NET 8) that fetches cat images from the **Cats as a Service API** (The Cat API) and stores them in a **Microsoft SQL Server** database using **Entity Framework Core**.

---

## Features

- Fetch and store 25 cat images with related metadata and tags.
- Prevent duplicate entries when fetching images multiple times.
- Retrieve a single cat by ID.
- Retrieve a paginated list of cats.
- Filter cats by a specific tag with pagination.
- Swagger API documentation.
- Docker support with `docker-compose`.

---

## Technologies Used

- **ASP.NET Core Web API (.NET 8)**
- **Entity Framework Core**
- **Microsoft SQL Server**
- **Docker & Docker Compose**
- **Swagger** for API documentation
- **xUnit** for unit testing

---

## Prerequisites

- .NET 8 SDK
- Docker

---

## Setup and Installation

### Running the Application (Using Visual Studio 2022 Docker Profile)

1. Open the project in Visual Studio 2022.
2. Set the Docker Compose profile as the startup project.
3. Press F5 or click on "Run" to start the application.

### Running the Application (Using Docker Compose)

1. Open a terminal and navigate to the solution folder.
2. Run the following command:
   ```bash
   docker compose up -d --build

The API should now be running at https://localhost:8081/ and http://localhost:8080/.

### Database Migrations

Database migrations will be applied automatically when the application starts.

### API Endpoints

The API provides the following endpoints:

Fetch and Store Cat Images
- POST /api/cats/fetch
- Fetches 25 cat images from The Cat API and stores them in the database.
- Prevents duplicate entries.

Retrieve a single Cat Image
- GET /api/cats/{id}
- Retrieves a cat image by its ID.

Retrieve cats with paging support
- GET /api/cats?page={page}&pageSize={pageSize}

Retrieve cats with a specific tag with paging support
- GET /api/cats?page={page}&pageSize={pageSize}&tag={tag}
- Retrieves paginated cat images filtered by a specific tag.

### API Documentation

Swagger documentation is available at https://localhost:8081/swagger/index.html

### Unit Tests

1. You can run tests using the Test Explorer in Visual Studio.
2. Alternatively, you can run the following command:
   ```bash
   dotnet test <PathToTheSolutionFile\MySoftware.CaaS.sln>
_


