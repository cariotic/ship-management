# Ship Management API

A **RESTful ASP.NET Core Web API** for managing different types of ships, including passenger and tanker ships, along with fuel and passenger management functionality.

## Features

- Add/Get Passenger Ships
- Add/Get Tanker Ships
- Get all Ships
- Add/Remove Passengers to/from Passenger Ships
- Fill/Empty Fuel Tanks

## Run with Docker

```bash
cd ShipManagementApi
docker-compose up --build
```

The app will be accessible at **http://localhost:8080**

## Testing API endpoints

This project includes a **requests.http** file located in the root directory.
You can open it in VS Code and use the REST Client extension to test the API.
