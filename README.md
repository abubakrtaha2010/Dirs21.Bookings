# Dynamic Mapper Solution

## Introduction
This project provides a dynamic mapper system to handle bidirectional data mapping between internal `DIRS21` C# data models and external partner-specific data models. The solution is built using ASP.NET Core and follows the Onion architecture, ensuring scalability, maintainability, and separation of concerns. It provides a highly dynamic approach to adding new mappings with minimal modifications to the existing codebase. Leveraging the concept of dynamic C# execution, it offers comprehensive flexibility to validate, update, modify, and enhance mappings as needed.

---

## Features
- **Dynamic Mapping**:
  - Map data between internal and partner formats.
  - Store and execute dynamic C# code for mapping.
  
- **Caching and Database Integration**:
  - Uses `MemoryCache` for fast retrieval of mappings.
  - Integrates `LiteDB` for persistent storage of mappings.

- **Environment Support**:
  - Supports `Development` and `Production` environments using `appsettings.Development.json` and `appsettings.Production.json`.

- **Testing Framework**:
  - Includes unit tests in a dedicated test project (`Tests`) for validating functionality.

- **Developer Experience**:
  - Offers a Swagger UI for manual testing and exploration.
  - Provides clear and simple API endpoints.

---

## Architecture
The solution is designed using Onion architecture:
- **API Project**:
  - Entry point (`Program.cs`) handles service registration and application startup.
  - Contains the `MappingsController` with three main endpoints:
    1. **Get Mapping**: Retrieve existing mappings.
    2. **Save Mapping**: Store source type, target type, and dynamic mapping code.
    3. **Map Data**: Perform mapping using stored C# code.

- **Application Project**:
  - Houses business logic in the `MappingService`.
  - Manages interactions with the cache and database.

- **Domain Project**:
  - Defines core entities and interfaces.

- **Infrastructure Project**:
  - Handles external concerns such as database integration with `LiteDB`.

---

## How It Works
1. **Application Startup**:
   - The app starts in `Program.cs`, where services are registered and initialized.
   - Swagger UI is launched for testing endpoints.

2. **Workflow**:
   - Requests are handled by `MappingsController`, which delegates logic to `MappingService`.
   - `MappingService`:
     - Retrieves mappings from `MemoryCache` or `LiteDB`.
     - Executes dynamic C# mapping code stored in the database.

3. **Endpoints**:
   - **GET /Mappings/{key}**: Retrieve an existing mapping by key (e.g. Reservation), source type, and target type.
   - **PUT /Mappings/{key}**: Save a key, source type, target type, and dynamic mapping code.
   - **POST /Mappings/{key}/MapData**: Map a source data object to a target type using stored dynamic code.

---

## Testing
- The project includes a `Tests` project with example unit tests (not a 100% coverage):
  - Validates saving and retrieving mappings.
  - Ensures correct execution of dynamic mapping logic.

---

## Setup and Usage
1. **Prerequisites**:
   - .NET 8 SDK or later.
   - An IDE like Visual Studio for ASP.NET Core development.

2. **Running the Application**:
   - Clone the repository and build the solution.
   - Run the project to start the API server.
   - Use Swagger UI for manual API interaction or `MainTests.cs` for automated and ready use-case testing.

3. **Endpoints**:
   - Access Swagger UI for testing API endpoints.
   - Refer to `MainTests.cs` for example inputs and test cases.

---

## Future Enhancements
1. **Advanced Caching**:
   - Replace `MemoryCache` with distributed caching (e.g., Redis) for scalability.
   
2. **Database Optimization**:
   - Upgrade `LiteDB` to a robust solution like SQL Server or MongoDB for production.

3. **Enhanced Testing**:
   - Add integration tests and use mocking frameworks.
   - Include performance benchmarks for mapping operations.

---

## Conclusion
This project demonstrates a maintainable, scalable solution for dynamic data mapping. It adheres to software engineering best practices while allowing room for future growth and optimization. 
```