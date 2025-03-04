# Project Description

This project is a Blazor application built with .NET 9, consisting of multiple services including a MySQL database, a migration service, an API service, and a web frontend. The application is designed to provide a comprehensive utility management system with various statistical views and data management capabilities.

## Running the Project with Docker Compose

To run the project using Docker Compose, follow these steps:

1. **Ensure Docker and Docker Compose are installed** on your machine. You can download and install them from the [Docker website](https://www.docker.com/get-started).

2. **Set up environment variables**:
   - Create a `.env` file in the root directory of the project. Or reuse existing `.env` file in the repository and replace variables with your own.
   - Add the necessary environment variables required by the services. 

3. **Build and run the services**:
   - Open a terminal and navigate to the root directory of the project.
   - Run the following command to build and start all the services defined in the `docker-compose.yml` file:

```bash
docker compose up -d
```

4. **Access the application**:
   - Once the services are up and running, you can access the web frontend by navigating to `http://localhost:10002` in your web browser.

5. **Stopping the services**:
   - To stop the running services, use the following command:

```bash
docker compose down
```

By following these steps, you will be able to run the entire application stack using Docker Compose.