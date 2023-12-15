# PriceNegotiationAPI
This web application serves as a platform for price negotiations between customers and employees of an online store. Customers can propose a price for a product, and employees have the ability to accept or reject the proposal. Built using CLEAN architecture and containerized using Docker for ease of use.

## Technologies
- .NET 7
- MSSQL Server
- Dapper
- MediatR
- Xunit
- Fluent Validations
- Serilog
- Clean Architecture

## Architecture
The application follows the principles of Clean Architecture, emphasizing separation of concerns and maintainability. It's organized into distinct layers:

![image](https://github.com/zoneel/PriceNegotiationAPI/assets/40122657/89ebfdcd-9a4b-4de0-8bf6-5d1bf0d88281)

### Layers:
- **Presentation Layer**: Responsible for handling API requests and responses.
- **Infrastructure Layer**: Manages external concerns like database access, adhering to interfaces defined in the Domain layer.
- **Application Layer**: Contains the business logic, including negotiation processes and product catalog management.
- **Domain Layer**: Holds the core business entities and logic, ensuring independence from external concerns.

## Database Overview
The application utilizes MSSQL Server Docker image for data storage. The database schema comprises tables for product catalog, negotiation details, and any necessary additional entities.

![image](https://github.com/zoneel/PriceNegotiationAPI/assets/40122657/deda16be-09f9-46d0-93de-2de8116beca5)


## API Endpoints
### Products Endpoint:
![image](https://github.com/zoneel/PriceNegotiationAPI/assets/40122657/639bda51-22c2-4aeb-9912-e87b357a44dc)

### Negotiation Endpoint:
![image](https://github.com/zoneel/PriceNegotiationAPI/assets/40122657/05be858f-08b9-49dd-9d4c-c7395c6fc886)

### User Endpoint:
![image](https://github.com/zoneel/PriceNegotiationAPI/assets/40122657/8c518b1e-a70d-441d-83f1-fb915e4e004d)


## Launch Instructions
To run the application locally:
1. Clone the repository.
2. Create and run a container using docker-compose located in my project.
3. Set up the MSSQL Server database using command below:

   > ```docker exec -it pricenegotiationdb /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password123 -i /sql-scripts/init-script.sql ```
6. Access the API endpoints through a tool like Postman or via your browser.

**(default port is set to 5000 so you can access swagger by going to: localhost:5000/swagger)**
